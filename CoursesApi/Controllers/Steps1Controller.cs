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
using CoursesApi.Models;

namespace CoursesApi.Controllers
{
    public class Steps1Controller : ApiController
    {
        private ApiContext db = new ApiContext();

        // GET: api/Steps1
        public IQueryable<Step> GetSteps()
        {
            return db.Steps;
        }

        // GET: api/Steps1/5
        [ResponseType(typeof(Step))]
        public IHttpActionResult GetStep(int id)
        {
            Step step = db.Steps.Find(id);
            if (step == null)
            {
                return NotFound();
            }

            return Ok(step);
        }

        // PUT: api/Steps1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStep(int id, Step step)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != step.StepID)
            {
                return BadRequest();
            }

            db.Entry(step).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StepExists(id))
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

        // POST: api/Steps1
        [ResponseType(typeof(Step))]
        public IHttpActionResult PostStep(Step step)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Steps.Add(step);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = step.StepID }, step);
        }

        // DELETE: api/Steps1/5
        [ResponseType(typeof(Step))]
        public IHttpActionResult DeleteStep(int id)
        {
            Step step = db.Steps.Find(id);
            if (step == null)
            {
                return NotFound();
            }

            db.Steps.Remove(step);
            db.SaveChanges();

            return Ok(step);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StepExists(int id)
        {
            return db.Steps.Count(e => e.StepID == id) > 0;
        }
    }
}