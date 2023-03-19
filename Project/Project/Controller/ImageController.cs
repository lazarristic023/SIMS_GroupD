using Project.Model;
using Project.Repository;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    public class ImageController
    {
        ImageRepository imageRepository { get; set; }

        public ImageController()
        {
            imageRepository = new ImageRepository();
        }

        public void Create(string url, int entityId, PictureType type)
        {
            Image image = new Image(url,entityId,type);
            imageRepository.Add(image);
            

        }
    }
}
