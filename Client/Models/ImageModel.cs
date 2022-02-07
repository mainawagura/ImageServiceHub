using Microsoft.AspNetCore.Http;

namespace Client.Models
{
    public class ImageModel
    {
        public string ImageName { get; set; }
        public IFormFile  file { get; set; }
    }
}