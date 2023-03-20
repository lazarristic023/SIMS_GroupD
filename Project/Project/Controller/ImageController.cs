using Project.Model;
using Project.Repository;
using System;
<<<<<<< HEAD
using System.CodeDom;
=======
>>>>>>> development
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
<<<<<<< HEAD
            Image image = new Image(url,entityId,type);
            imageRepository.Add(image);
            
=======
            Image image = new Image(url, entityId, type);
            imageRepository.Add(image);

>>>>>>> development

        }

        public List<string> GetImageUrlByTourId(int id)
        {
            List<Image> imageList = imageRepository.GetImagesByEntityIdandType(id, PictureType.TOUR);
            List<string> urls = new List<string>();
<<<<<<< HEAD
            
=======

>>>>>>> development
            foreach (Image image in imageList)
            {
                urls.Add(image.Url);
            }

            return urls;
        }
    }
}
