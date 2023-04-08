using System.ComponentModel;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace camp_fire.Infrastructure.Helpers;

public static class SharedTools
{
    public static string ToJson(this object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public static T? FromJson<T>(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return default;

        return JsonSerializer.Deserialize<T?>(str);
    }

    public static string EncryptPassword(this string password)
    {
        var byteData = Encoding.Unicode.GetBytes(password);

        HashAlgorithm sha = SHA256.Create();

        var byteEncryptSha1 = sha.ComputeHash(byteData);

        return Convert.ToBase64String(byteEncryptSha1);
    }

    public static string GetDescription(this Enum @enum)
    {
        var type = @enum.GetType();
        if (!type.IsEnum) return string.Empty;

        var memberInfo = type.GetMember(@enum.ToString()).FirstOrDefault();

        var attr = memberInfo is null ? default :
            memberInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attr is null ? @enum.ToString() : attr.Description;
    }

    public static string GenerateCode(int lenght = 4, bool OnlyInt = false)
    {
        List<string> strArray = new List<string>
            {"0","1","2","3","4","5","6","7","8","9"};
        if (!OnlyInt)
        {
            List<string> stringList = new List<string>{
                    "A", "B", "C", "D", "E", "F", "G", "H",
                    "I", "J", "K", "L", "N", "O", "P", "Q",
                    "R", "S", "T", "U", "V", "X", "Y", "Z",
                    "a", "b", "c", "d", "e", "f", "g", "h",
                    "i", "j", "k", "l", "n", "o", "p", "q",
                    "r", "s", "t", "u", "v", "x", "y", "z"};

            strArray.AddRange(stringList);
        }
        Random random = new Random();
        string empty = string.Empty;
        for (int index = 0; index < lenght; ++index)
            empty += strArray[random.Next(strArray.Count - 1)];
        return empty;
    }

    /// <summary>
    /// Email doğruluğunu kontrol eder
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool EmailIsValid(this string email)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
                return false;

            MailAddress result = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}