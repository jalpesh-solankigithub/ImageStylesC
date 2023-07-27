using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ImageStyleCreator.Models;

namespace ImageStyleCreator
{
    public class ImageStyleCreatorContext : DbContext
    {
        public ImageStyleCreatorContext (DbContextOptions<ImageStyleCreatorContext> options)
            : base(options)
        {
        }

        public DbSet<ImageStyleCreator.Models.ImageDetailsModel> ImageDetailsModel { get; set; } = default!;
    }
}
