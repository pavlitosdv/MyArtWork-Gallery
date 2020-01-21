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

        public ActionResult ArtWorksByArtist()
        {
            string artistId = User.Identity.GetUserId();

            var artWorks = _artWorksRepository.GetArtWorksByArtist(artistId);

            return View(artWorks);
        }

        [Authorize(Roles = "Artist, Administrator")]
        public ActionResult Create()
        {
            List<Tag> tags = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                tags = db.Tags.ToList();
            }
            ViewBag.Tags = tags;
            return View();
        }

        [Authorize(Roles = "Artist, Administrator")]
        [HttpPost]
        public ActionResult Create(ArtWork artwork)
        {
            string artistId = User.Identity.GetUserId();
            ApplicationUser artist = db.Users.SingleOrDefault(i => i.Id == artistId);

            DateTime published = DateTime.Now.Date; //

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
                artwork.media, artwork.surface, artwork.Price, published, artwork.Thumbnail, artist, artwork.TagIds);
            
            return RedirectToAction("Index","Profile");
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