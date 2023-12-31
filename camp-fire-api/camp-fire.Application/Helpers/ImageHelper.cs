namespace camp_fire.Application.Helpers;

public class ImageHelper
{
    public string SaveImage(string folderName, string image)
    {
        var hashedKey = DateTime.Now.ToString("yyMMddHHmmss") + DateTime.Now.Ticks.ToString();
        var file = FileExtensionHandler.GetFileContentList().FirstOrDefault(y => image!.StartsWith(y.Key!));
        var extension = Path.GetExtension(file?.Extension);
        string fileName = hashedKey + extension;

        // Dizin yolu oluştur
        var directoryPath = Path.Combine("wwwroot", folderName);
        var path = Path.Combine(directoryPath, fileName);

        // Dizin yoksa oluştur
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var url = $"http://37.148.213.3/{folderName}/{fileName}";
        byte[] imageBytes = Convert.FromBase64String(image!);
        System.IO.File.WriteAllBytes(path, imageBytes);

        return url;
    }
}