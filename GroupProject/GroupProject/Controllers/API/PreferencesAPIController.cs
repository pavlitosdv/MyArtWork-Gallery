using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GroupProject.Models;

namespace GroupProject.Controllers
{
    public class PreferencesAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PreferencesAPI
        public IQueryable<Preference> GetPreferences()
        {
            return db.Preferences;
        }

        // GET: api/PreferencesAPI/5
        [ResponseType(typeof(Preference))]
        public IHttpActionResult GetPreference(int id)
        {
            Preference preference = db.Preferences.Find(id);
            if (preference == null)
            {
                return NotFound();
            }

            return Ok(preference);
        }

        // PUT: api/PreferencesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPreference(int id, Preference preference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != preference.Id)
            {
                return BadRequest();
            }

            db.Entry(preference).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreferenceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PreferencesAPI
        [ResponseType(typeof(Preference))]
        public IHttpActionResult PostPreference(Preference preference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Preferences.Add(preference);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = preference.Id }, preference);
        }

        // DELETE: api/PreferencesAPI/5
        [ResponseType(typeof(Preference))]
        public IHttpActionResult DeletePreference(int id)
        {
            Preference preference = db.Preferences.Find(id);
            if (preference == null)
            {
                return NotFound();
            }

            db.Preferences.Remove(preference);
            db.SaveChanges();

            return Ok(preference);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PreferenceExists(int id)
        {
            return db.Preferences.Count(e => e.Id == id) > 0;
        }
    }
}