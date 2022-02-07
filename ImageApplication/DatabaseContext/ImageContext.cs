using ImageApplication.Modal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApplication.DatabaseContext
{
    public class ImageContext : DbContext
    {
        public DbSet<Image> Images { get; set; }



        public ImageContext(DbContextOptions<ImageContext> options) : base(options)
        {

        }

    }
}