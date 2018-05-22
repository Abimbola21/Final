using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Final.Models;

namespace Final.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Title,Description,CreditHours,Session,Status,Instructor")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Title,Description,CreditHours,Session,Status,Instructor")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        // GET: Courses matching the search  string
        public IActionResult CourseSearch(string searchFor)
        {
            if (searchFor.Equals(null))
            {
                return NotFound();
            }
            ViewBag.SearchTerm = searchFor;

            searchFor = searchFor.ToLower();
            string subSearch = searchFor.Substring(searchFor.IndexOf(":") + 1);


            List<Course> allCourses = _context.Courses.ToList();
            List<Course> courses;

            if (searchFor.Contains("title"))
            {
                courses = allCourses.FindAll(s => s.Title.ToLower().Contains(subSearch));
            }
            else if (searchFor.Contains("session"))
            {
                courses = allCourses.FindAll(s => s.Session.ToLower().Contains(subSearch));
            }
            else if (searchFor.Contains("code"))
            {
                courses = allCourses.FindAll(s => s.Code.ToLower().Contains(subSearch));
            }
            else if (searchFor.Contains("status"))
            {
                courses = allCourses.FindAll(s => s.Status.ToLower().Contains(subSearch));
            }
            else if (searchFor.Contains("instructor"))
            {
                courses = allCourses.FindAll(s => s.Instructor.ToLower().Contains(subSearch));
            }
            else
            {
                courses = allCourses.FindAll(s => s.Title.ToLower().Contains(subSearch)
                                            || s.Session.ToLower().Contains(subSearch)
                                            || s.Code.ToLower().Contains(subSearch)
                                            || s.Status.ToLower().Contains(subSearch)
                                            || s.Instructor.ToLower().Contains(subSearch));
            }
            if (courses == null)
            {
                return NotFound();
            }

            // return View("~/Views/Songs/Index.cshtml",songs);
            return View(courses);
        }
    }
}
