using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Threading;

namespace GroupProject.Repositories
{
    public class CommissionRepository
    {
        public void AddCommissionToUser(string userId, IEnumerable<int> commissionIds)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //double total = 0;
                foreach (var item in commissionIds)
                {

                    var artworks = db.ArtWorks.Find(item);
                    var artist = db.Users.SingleOrDefault(i => i.Id == artworks.Artist.Id);
                    db.Commissions.Add(new Commission
                    {
                        UserId = userId,
                        Price=artworks.Price,
                        ArtistId = artist.Id,
                        DateOfCommission = DateTime.Now.Date
                    });
                    //total += artworks.Price;
                }
                db.SaveChanges();

            }
        }
    }
}