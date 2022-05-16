using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes_app.Models;
using Notes_app.Services;

namespace Notes_app.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _applicationDb;
        public NotesController(ApplicationDbContext dbContext)
        {
            _applicationDb = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allNotes = _applicationDb.Notes.ToList();
            return View(allNotes);
        }

        [HttpGet]
        public IActionResult AddNoteForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNote(Notes note)
        {
            DateTime dt = DateTime.Now;
            note.ModificationDate = dt;
            _applicationDb.Notes.Add(note);
            _applicationDb.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteNote(int id)
        {
           var noteToRemove = _applicationDb.Notes.Where(x => x.Id == id).FirstOrDefault();
            _applicationDb.Notes.Remove(noteToRemove);
            _applicationDb.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditNote(int id)
        {
            var noteToEdit = _applicationDb.Notes.Where(x => x.Id == id).FirstOrDefault();
            return View(noteToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateNote(Notes note)
        {
            note.ModificationDate = DateTime.Now;
            _applicationDb.Entry(note).State = EntityState.Modified;
            _applicationDb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
