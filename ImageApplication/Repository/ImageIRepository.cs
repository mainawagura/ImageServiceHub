using ImageApplication.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApplication.Repository
{
   public interface ImageIRepository
    {

        IEnumerable<Image> GetImages();
        Image GetImageById(int ImageId);
        void InsertImage(Image image);
        void DeleteImage(string Imagename);
        void UpdateImage(Image image);
        void Save();
    }
}
