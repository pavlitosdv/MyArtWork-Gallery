﻿using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomLibrary;

namespace GroupProject.Repositories
{
    public class TagsRepository
    {
        #region GetTags
        public IEnumerable<Tag> GetTags()
        {
            IEnumerable<Tag> tags;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                tags = db.Tags.ToList();
            }

            return tags;
        }
        #endregion

        #region AddTag
        public void AddTag(string name)
        {
            Throw.IfNullOrWhiteSpace(name, "Name cannot be null or whitespace");

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Tags.Add(new Tag
                {
                    Name = name
                });

                db.SaveChanges();
            }
        }
        #endregion

        #region UpdateTag
        public bool UpdateTag(Tag tag)
        {
            Throw.IfNull(tag, nameof(tag));

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Tags.Attach(tag);
                db.Entry(tag).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return true;
        }
        #endregion

        #region DeleteTag
        public bool DeleteTag(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var tag = db.Tags.Find(id);
                db.Tags.Remove(tag);
                db.SaveChanges();
            }
            return true;
        }
        #endregion

        #region FindById
        public Tag FindById(int id)
        {
            Tag tag;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                tag = db.Tags.Find(id);
            }

            return tag;
        }
        #endregion
    }
}