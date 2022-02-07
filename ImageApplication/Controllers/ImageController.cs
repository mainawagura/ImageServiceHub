using ImageApplication.Modal;
using ImageApplication.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImageApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageIRepository _imageIRepository;

        private IWebHostEnvironment _webHostEnvironment;

        public ImageController(ImageIRepository imageIRepository,IWebHostEnvironment webHostEnvironment)
        {
         _webHostEnvironment = webHostEnvironment;
            _imageIRepository = imageIRepository;
        }

        // GET: api/<ImageController>
        [HttpGet]
        public IActionResult Get()
        {
          
            
            var images = _imageIRepository.GetImages();
            
            return new OkObjectResult(images);
        }

        // GET api/<ImageController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
          var image =  _imageIRepository.GetImageById(id);
            return new OkObjectResult(image);
        }

        // POST api/<ImageController>
        [HttpPost]
       
        public async Task <IActionResult> Post([FromForm] IFormFile file)
        {
            var filename = Guid.NewGuid()+Path.GetExtension(file.FileName);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/",filename);
      
            using var stream = new FileStream(path, FileMode.Create);

            var imageObject = new Image();
            imageObject.ImageName= filename;
           await file.CopyToAsync(stream);

            _imageIRepository.InsertImage(imageObject);
            return RedirectToAction("Get");
        }

        // PUT api/<ImageController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Image image)
        {
            if (image != null)
            {
                using(var scope = new TransactionScope())
                {
                    _imageIRepository.UpdateImage(image);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/<ImageController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string Imagename)
        {
            _imageIRepository.DeleteImage(Imagename);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/", Imagename);


            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                //ViewBag.deleteSuccess = "true";
            }
            return new OkResult();
        }

     /*  public FileResult Download(string ImageName)
        {

            string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory() + "/Images/"));
            var path = Path.Combine(filePath, ImageName);
            var stream = new FileStream(path, FileMode.Open);
            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, ImageName);

        }*/
    }
}
