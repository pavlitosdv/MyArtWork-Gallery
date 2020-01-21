using GroupProject.Models;
using GroupProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArtWorksRepository _artWorksRepository = new ArtWorksRepository();
        private readonly ArtistSearchRepository _artistSearchRepository = new ArtistSearchRepository();
        public ActionResult Index()
        {
            var artWorks = _artWorksRepository.GetArtWorks();
            return View(artWorks);
        }

        public ActionResult Search(string searchTerm, string category)
        {
            if (category == "artist")
            {
                return View("~/Views/Home/SearchArtists.cshtml", _artistSearchRepository.SearchArtist(searchTerm, category));
            }

            IEnumerable<ArtWork> artWorks = null;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                artWorks = _artWorksRepository.SearchArtWorks(searchTerm);

            }
            return View(artWorks);
        }

    public ActionResult SingleImage(int id)
        {
            return View(id);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}