using ImageApplication.DatabaseContext;
using ImageApplication.Modal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApplication.Repository
{
    public class ImageRepository : ImageIRepository
    {
        private readonly ImageContext _imageContext;
        public ImageRepository( ImageContext imageContext) 
        {
            _imageContext = imageContext;
        }
        public void DeleteImage(string Imagename)
        {
            var image = _imageContext.Images.Find(Imagename);
            _imageContext.Images.Remove(image);
            Save();
           
        }

        public Image GetImageById(int ImageId)
        {
            return _imageContext.Images.Find(ImageId);
        }

        public IEnumerable<Image> GetImages()
        {
            return _imageContext.Images.ToList();
        }

        public void InsertImage(Image image)
        {
            _imageContext.Images.Add(image);
            Save();
        }

        public void Save()
        {
            _imageContext.SaveChangesAsync();
        }

        public void UpdateImage(Image image)
        {
            _imageContext.Entry(image).State = EntityState.Modified;
            Save();
        }
    }
}
