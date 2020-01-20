using CustomLibrary;
using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using GroupProject.Models.Enums;

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

        public IEnumerable<ArtWork> GetArtWorks(IEnumerable<int> ids)
        {
            List<ArtWork> artWorks = new List<ArtWork>();

            using (var db = new ApplicationDbContext())
            {
                artWorks = db.ArtWorks.Where(gig => ids.Contains(gig.Id)).ToList();
            }

            return artWorks;
        }

        #region AddArtWork
        public void AddArtWork(string name, long length, long width, Style style, Models.Enums.Type type, Media media, 
                               Surface surface, double price, DateTime datePublished, string thumbnail, ApplicationUser artist)
        {
            Throw.IfNullOrWhiteSpace(name, "Name cannot be null or whitespace");

            var artwork = new ArtWork
            {
                Name = name,
                Length = length,
                Width = width,
                style = style,
                type = type,
                media = media,
                surface = surface,
                Price = price,
                DatePublished = datePublished,
                Thumbnail = thumbnail,
                Artist_Id = artist.Id
                //Artist = db.Users.SingleOrDefault(i => i.Id == User.Identity.GetUserId())
            };
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.ArtWorks.Add(artwork);

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

        #region Search Bar ArtWorks
        public IEnumerable<ArtWork> SearchArtWorks(string searchTerm)
        {
            IEnumerable<ArtWork> artWorks;

            using (var db = new ApplicationDbContext())
            {
                artWorks = db.ArtWorks.Include("Tags")
                            .Where(i => i.Name.Contains(searchTerm)).ToList();                                     
            }
            return artWorks;
        }
        #endregion
    }
}