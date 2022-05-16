using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Notes_app.Models;
using Notes_app.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Notes_app.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly ILogger<NotesController> _logger;
        public NotesController(ApplicationDbContext dbContext, ILogger<NotesController> logger)
        {
            _applicationDb = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("action=index");
            var allNotes = _applicationDb.Notes.ToList();
            _logger.LogInformation($"action=index notesCount={allNotes.Count}");
            return View(allNotes);
        }

        [HttpGet]
        public IActionResult AddNoteForm()
        {
            _logger.LogInformation("action=addNoteForm");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNote(Notes note)
        {
            _logger.LogInformation($"action=addNote note={JsonSerializer.Serialize(note)}");
            DateTime dt = DateTime.Now;
            note.ModificationDate = dt;
            _applicationDb.Notes.Add(note);
            _applicationDb.SaveChanges();
            _logger.LogInformation("action=addNote msg='note saved'");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteNote(int id)
        {
            _logger.LogInformation($"action=deleteNote id={id}");
            var noteToRemove = _applicationDb.Notes.Where(x => x.Id == id).FirstOrDefault();
            _applicationDb.Notes.Remove(noteToRemove);
            _applicationDb.SaveChanges();
            _logger.LogInformation("action=deleteNote msg='note removed'");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditNote(int id)
        {
            _logger.LogInformation($"action=editNote id={id}");
            var noteToEdit = _applicationDb.Notes.Where(x => x.Id == id).FirstOrDefault();
            _logger.LogInformation("action=editNote msg='note to edit was found'");
            return View(noteToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateNote(Notes note)
        {
            _logger.LogInformation($"action=updateNote note={JsonSerializer.Serialize(note)}");
            note.ModificationDate = DateTime.Now;
            _applicationDb.Entry(note).State = EntityState.Modified;
            _applicationDb.SaveChanges();
            _logger.LogInformation($"action=updateNote msg='note was modified'");
            return RedirectToAction("Index");
        }
    }
}