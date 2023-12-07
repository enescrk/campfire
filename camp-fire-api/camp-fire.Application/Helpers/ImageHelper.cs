namespace camp_fire.Application.Helpers;

public class ImageHelper
{
    public string SaveImage(string folderName, string image)
    {
        //* Gelen resmin Extension'ını al
        var file = FileExtensionHandler.GetFileContentList().FirstOrDefault(y => image!.StartsWith(y.Key!));
        var extension = Path.GetExtension(file?.Extension);

        //* Gelen resim adı yerine standart isim vermek için tarih formatını al
        var hashedKey = DateTime.Now.ToString("yyMMddHHmmss") + DateTime.Now.Ticks.ToString();

        //* Image adını => ImageType_HashedKey.Extension olarak oluştur
        string fileName = hashedKey + extension;

        //* Path ile Image adını birleştir
        var path = Path.Combine(folderName, fileName);

        if (!Directory.Exists(folderName))
        {
            Directory.CreateDirectory(folderName);
        }

        //* Url oluştur
        var url = "http://37.148.213.3/static/" + path; //TODO: domain eklenecek.

        //* Image'ı fiziksel olarak kaydet
        byte[] imageBytes = Convert.FromBase64String(image!);

        // var reducedImageBytes =  ReducingImageQuality(imageBytes);

        System.IO.File.WriteAllBytes(path, imageBytes);

        return url;
    }
}