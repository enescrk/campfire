// using System.Threading;
// using camp_fire.Domain.ValueObjects;

// namespace camp_fire.Domain.SeedWork.Helpers
// {
//     public static class GetLanguageHelper
//     {
//         public static LanguageType GetLanguage()
//         {
//             /*  Farklı şekilde language alma türleri
//              var language = Request.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
//              var language2 = Request.GetTypedHeaders().AcceptLanguage?.FirstOrDefault().Value.Value; */

//             var language = Thread.CurrentThread.CurrentCulture.Name;

//             switch (language)
//             {
//                 case "de-DE":
//                     return LanguageType.DEU;
//                 case "tr-TR":
//                     return LanguageType.TUR;
//                 case "en-US":
//                     return LanguageType.GBR;
//                 case "fr-FR":
//                     return LanguageType.FRA;
//             }

//             return LanguageType.DEU;
//         }
//     }
// }