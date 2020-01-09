using GroupProject.Models;
using GroupProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class ArtWorkController : Controller
    {
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
            if (!ModelState.IsValid)
            {
                return View(artwork);
            }
            _artWorksRepository.AddArtWork(artwork.Name);

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