using GroupProject.Models;
using GroupProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class TagController : Controller
    {
        TagsRepository _tagsRepository = new TagsRepository();

        // GET: Tag
        public ActionResult Index()
        {
            var tags = _tagsRepository.GetTags();
            return View(tags);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View(tag);
            }
            _tagsRepository.AddTag(tag.Name);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var tag = _tagsRepository.FindById(id);
            return View(tag);
        }

        public ActionResult Edit(int id)
        {
            var tag = _tagsRepository.FindById(id);
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View(tag);
            }

            _tagsRepository.UpdateTag(tag);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var tag = _tagsRepository.FindById(id);
            return View(tag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            _tagsRepository.DeleteTag(id);

            return RedirectToAction("Index");
        }
    }
}