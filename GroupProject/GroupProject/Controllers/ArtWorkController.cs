using GroupProject.Models;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class ArtWorkController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        ArtWorksRepository _artWorksRepository = new ArtWorksRepository();

        // GET: ArtWork      
        public ActionResult Index()
        {
            var artWorks = _artWorksRepository.GetArtWorks();
            return View(artWorks);
        }

        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArtWork artwork)
        {
            string artistId = User.Identity.GetUserId();
            ApplicationUser artist = db.Users.SingleOrDefault(i => i.Id == artistId);

            if (artwork.ImageFile == null)
            {
                artwork.Thumbnail = "no_image.jpg";
            }
            else
            {
                string extension = Path.GetExtension(artwork.ImageFile.FileName);
                artwork.Thumbnail = Guid.NewGuid().ToString() + extension;
                string fileName = Path.Combine(Server.MapPath("~/ArtWorksImages/"), artwork.Thumbnail);
                artwork.ImageFile.SaveAs(fileName); 
            }

            if (!ModelState.IsValid)
            {
                return View(artwork);
            }
            _artWorksRepository.AddArtWork(artwork.Name, artwork.Length, artwork.Width, artwork.style, artwork.type,
                artwork.media, artwork.surface, artwork.Price, artwork.DatePublished, artwork.Thumbnail, artist);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var artwork = _artWorksRepository.FindById(id);
            return View(artwork);
        }

        public ActionResult Edit(int id)
        {
            var artwork = _artWorksRepository.FindById(id);
            return View(artwork);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArtWork artwork)
        {
            if (!ModelState.IsValid)
            {
                return View(artwork);
            }

            _artWorksRepository.UpdateArtWork(artwork);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var artwork = _artWorksRepository.FindById(id);
            return View(artwork);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            _artWorksRepository.DeleteArtWork(id);
            return RedirectToAction("Index");
        }


    }
}