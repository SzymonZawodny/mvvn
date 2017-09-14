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
    public class Courses1Controller : ApiController
    {
        private ApiContext db = new ApiContext();

        // GET: api/Courses1
        public IEnumerable<CourseDTO> GetCourses()
        {
            var courses = db.Courses;
            List<CourseDTO> courseDTO = new List<CourseDTO>();
            foreach (var c in courses)
            {
                CourseDTO course = new CourseDTO();
                List<StepDTO> steps = new List<StepDTO>();
                course.courseId = c.CourseID;
                course.name = c.name;
                course.completed = false;
                foreach (var s in c.steps)
                {
                    StepDTO stepDTO = new StepDTO();
                    stepDTO.stepId = s.StepID;
                    stepDTO.stepNumber = s.numberOfStep;
                    stepDTO.courseId = s.CourseID;
                    stepDTO.description = s.description;
                    stepDTO.completed = false;
                    steps.Add(stepDTO);
                }
                course.steps = steps;
                courseDTO.Add(course);
            }
                return courseDTO;
        }

        // GET: api/Courses1/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult GetCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/Courses1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourse(int id, Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.CourseID)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses1
        [ResponseType(typeof(Course))]
        public IHttpActionResult PostCourse(Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Courses.Add(course);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = course.CourseID }, course);
        }

        // DELETE: api/Courses1/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult DeleteCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            db.Courses.Remove(course);
            db.SaveChanges();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseExists(int id)
        {
            return db.Courses.Count(e => e.CourseID == id) > 0;
        }
    }
}