// using System.Drawing;
// using System.Drawing.Imaging;
// using System.Reflection;
// using Mustache;

// namespace camp_fire.Infrastructure.Storage;

// public class StorageService : IStorageService
// {
//     /// <summary>
//     /// Parametreden gelen değere göre ilgili HTML dosyasını okur ve değişecek alanları günceller
//     /// </summary>
//     /// <param name="templateName"></param>
//     /// <param name="model"></param>
//     /// <typeparam name="TModel"></typeparam>
//     /// <returns>string</returns>
//     public async Task<string> ReadHtmlFromPath<TModel>(string templateName, TModel model)
//     {
//         string templatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, templateName);
//         var htmlStr = await File.ReadAllTextAsync(templatePath);
//         var result = ReplaceDocument(htmlStr, model);
//         return result;
//     }

//     #region Helpers

//     /// <summary>
//     /// Yardımcı method
//     /// Parametreden gelen değerlere göre ilgili kaydın güncellenmesi gereken yerleri değiştirir
//     /// </summary>
//     /// <param name="templatePath"></param>
//     /// <param name="model"></param>
//     /// <typeparam name="TModel"></typeparam>
//     /// <returns></returns>
//     private string ReplaceDocument<TModel>(string file, TModel model)
//     {
//         var compiler = new FormatCompiler();
//         var generator = compiler.Compile(file);
//         var result = generator.Render(model);

//         return result;
//     }

//     #endregion

//     public string SaveImage(string folderName, ImageType imageType, string image)
//     {
//         //* Gelen resmin Extension'ını al
//         var file = FileExtensionHandler.GetFileContentList().FirstOrDefault(y => image!.StartsWith(y.Key!));
//         var extension = Path.GetExtension(file?.Extension);

//         //* Gelen resim adı yerine standart isim vermek için tarih formatını al
//         var hashedKey = DateTime.Now.ToString("yyMMddHHmmss") + DateTime.Now.Ticks.ToString();

//         //* Image adını => ImageType_HashedKey.Extension olarak oluştur
//         string fileName = imageType.ToString().ToLower() + "_" + hashedKey + extension;

//         //* Path ile Image adını birleştir
//         var path = Path.Combine(folderName, fileName);

//         if (!Directory.Exists(folderName))
//         {
//             Directory.CreateDirectory(folderName);
//         }

//         //* Url oluştur
//         var url = "http://45.76.90.60/static/" + path;

//         //* Image'ı fiziksel olarak kaydet
//         byte[] imageBytes = Convert.FromBase64String(image!);

//         // var reducedImageBytes =  ReducingImageQuality(imageBytes);

//         System.IO.File.WriteAllBytes(path, imageBytes);

//         return url;
//     }

//     // private byte[] ReducingImageQuality(byte[] inputBytes)
//     // {
//     //     byte[] outputBytes;
//     //     using (var inputStream = new MemoryStream(inputBytes))
//     //     {
//     //         Bitmap newImage = (Bitmap)System.Drawing.Image.FromStream(inputStream);
//     //         var jpegEncoder = ImageCodecInfo.GetImageDecoders()
//     //           .First(c => c.FormatID == ImageFormat.Jpeg.Guid);

//     //         EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, 75L /*Resim kalitesinin düþürüleceði deðer min 0, max 100 arasýnda olmalý!*/);

//     //         var encoderParameters = new EncoderParameters(1);
//     //         encoderParameters.Param[0] = qualityParam;

//     //         using var outputStream = new MemoryStream();
//     //         newImage.Save(outputStream, jpegEncoder, encoderParameters);
//     //         outputBytes = outputStream.ToArray();
//     //     }

//     //     return outputBytes;
//     // }
// }
