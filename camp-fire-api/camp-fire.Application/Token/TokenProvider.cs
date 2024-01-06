using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using camp_fire.Domain.SeedWork.Helpers;
using camp_fire.Infrastructure.Helpers;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Token;

public static class TokenProvider
{
    public static JwtTokenModel? GenerateJwtToken(IConfiguration configuration, JwtTokenModel user)
    {
        if (configuration is null || user is null)
            return null;

        var issuer = configuration["Authentication:Url"].Encrypt();
        var expiresIn = DateTime.Now.AddDays(Convert.ToDouble(configuration["Authentication:JwtExpireDays"]));
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email.Encrypt() ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString().Encrypt() ?? ""),
                new Claim(JwtRegisteredClaimNames.NameId, user.FullName?.Encrypt() ?? ""),
                new Claim("usermodel", user.ToJson().Encrypt()!)
            };

        var token = new JwtSecurityToken(issuer, issuer, claims, expires: expiresIn, signingCredentials: creds);

        var accesssToken = new JwtSecurityTokenHandler().WriteToken(token);

        var result = new JwtTokenModel
        {
            ExpiresIn = expiresIn.Date,
            AccessToken = accesssToken,
            FullName = user.FullName,
            Email = user.Email,
            Id = user.Id,
            IsManager = user.IsManager
        };

        return result;
    }

    public static int GetUserId(this IPrincipal user)
    {
        var result = GetTokenValue(user, "jti");

        return Convert.ToInt32(result);
    }

    public static string GetEmail(this IPrincipal user)
    {
        return GetTokenValue(user, "email");
    }

    public static string GetFullName(this IPrincipal user)
    {
        return GetTokenValue(user, "nameid");
    }

    public static JwtTokenModel GetLoggedInUser(this IPrincipal user)
    {
        var strModel = GetTokenValue(user, "usermodel");
        if (string.IsNullOrEmpty(strModel))
            throw new AuthException("Not Authorized!");

        return strModel.FromJson<JwtTokenModel>()!;
    }

    private static string GetTokenValue(IPrincipal user, string type)
    {
        var identity = user.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var claims = from c in identity.Claims
                         select new
                         {
                             subject = c.Subject?.Name,
                             type = c.Type,
                             value = c.Value
                         };
            var claim = claims.FirstOrDefault(a => a.type == type);

            if (claim != null)
                return claim.value.Decrypt()!;
        }

        return string.Empty;
    }

    /* TODO: Bumethod düzeltilecek.
    public static DateTime GetExpireIn(this IPrincipal user)
    {
       var exp = GetTokenValue(user, "exp");

       TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(exp));

       string result = t.ToString("MM/dd/yyyy hh:mm tt");

       return Convert.ToDateTime(result);
    }

    public static JwtSecurityToken Decode(string token)
    {
        var stream = "[encoded jwt]";
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(stream);
        var accesssToken = handler.ReadToken(token) as JwtSecurityToken;

        return accesssToken;
    }

    public static JwtTokenModel GetClaims(string accesssToken)
    {
        if (string.IsNullOrEmpty(accesssToken))
            return null;

        var decodedToken = Decode(accesssToken);

        var userId = decodedToken.Claims.First(claim => claim.Type == "Jti")?.Value?.Decrypt();
        var expiresIn = decodedToken.Claims.First(claim => claim.Type == "Exp")?.Value?.Decrypt();
        var email = decodedToken.Claims.First(claim => claim.Type == "Email")?.Value?.Decrypt();
        var fullName = decodedToken.Claims.First(claim => claim.Type == "NameId")?.Value?.Decrypt();

        return new JwtTokenModel
        {
            Id = Convert.ToInt32(userId),
            FullName = fullName,
            Email = email
        };
    }

    public static int[] GetRight(this IPrincipal user)
    {
        var result = GetTokenValue(user, "rightIds");

        return string.IsNullOrEmpty(result) ? null : Array.ConvertAll(result?.Split(','), x => (Convert.ToInt32(x)));
    }

    public static bool? GetUnlimitedRight(this IPrincipal user)
    {
        var result = GetTokenValue(user, "unlimitedRight");

        if (string.IsNullOrEmpty(result))
            return null;

        return Convert.ToBoolean(result);
    } */
}
