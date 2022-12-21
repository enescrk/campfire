// using Newtonsoft.Json;

// namespace camp_fire.Domain.SeedWork.Helpers
// {
//     public static class JsonHelper
//     {
//         public static string ToJson(this object obj)
//         {
//             return JsonConvert.SerializeObject(obj);
//         }

//         public static T FromJson<T>(this string str)
//         {
//             if (string.IsNullOrWhiteSpace(str))
//                 return default;

//             return JsonConvert.DeserializeObject<T>(str);
//         }
//     }
// }