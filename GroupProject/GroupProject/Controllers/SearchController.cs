using GroupProject.Models;
using GroupProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class SearchController : Controller
    {
        private readonly ArtWorksRepository _artWorksRepository = new ArtWorksRepository();


        [HttpGet]
        public ActionResult SearchBar(string searchTerm)
        {

            IEnumerable<ArtWork> artWorks = null;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                artWorks = _artWorksRepository.SearchArtWorks(searchTerm);

            }
            return View(artWorks);
        }
    }
}