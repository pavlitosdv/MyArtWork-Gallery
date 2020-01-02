using CustomLibrary;
using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Repositories
{
    public class ArtWorksRepository
    {
        #region GetArtWorks
        public IEnumerable<ArtWork> GetArtWorks()
        {
            IEnumerable<ArtWork> artWorks;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                artWorks = db.ArtWorks.ToList();
            }


            return artWorks;
        }
        #endregion

        #region AddArtWork
        public void AddArtWork(string name)
        {
            Throw.IfNullOrWhiteSpace(name, "Name cannot be null or whitespace");

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.ArtWorks.Add(new ArtWork
                {
                    Name = name
                });

                db.SaveChanges();
            }
        }
        #endregion

        #region UpdateArtWork
        public void UpdateArtWork(ArtWork artwork)
        {
            Throw.IfNull(artwork, nameof(artwork));

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.ArtWorks.Attach(artwork);
                db.Entry(artwork).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        #endregion

        #region DeleteArtWork
        public void DeleteArtWork(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var artwork = db.ArtWorks.Find(id);
                db.ArtWorks.Remove(artwork);
                db.SaveChanges();
            }
        }
        #endregion

        #region FindById
        public ArtWork FindById(int id)
        {
            ArtWork artwork;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                artwork = db.ArtWorks.Find(id);
            }

            return artwork;
        }
        #endregion
    }
}