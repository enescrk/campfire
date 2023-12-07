namespace camp_fire.Application.Helpers;

public class FileExtensionHandler
{
    public class FileContentBase64
    {
        public string? Extension { get; set; }
        public string? FriendlyName { get; set; }
        public string? MimeType { get; set; }
        public string? Key { get; set; }
    }

    public static List<FileContentBase64> GetFileContentList() =>
        new List<FileContentBase64>
        {
            new FileContentBase64
            {
                Extension = ".jpg",
                FriendlyName = "JPG",
                MimeType = "image/jpeg",
                Key = "/9j/4"
            },
            new FileContentBase64
            {
                Extension = ".png",
                FriendlyName = "PNG",
                MimeType = "image/png",
                Key = "iVBOR"
            }
        };
}
