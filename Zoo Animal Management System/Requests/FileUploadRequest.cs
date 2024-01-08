using Zoo_Animal_Management_System.Attributes;

namespace Zoo_Animal_Management_System.Requests
{
    public class FileUploadRequest
    {
        [MaxFileSizeAttribute(10 * 1024)]
        [AllowedExtensions(".JSON")]
        public IFormFile File { get; set; }
    }
}
