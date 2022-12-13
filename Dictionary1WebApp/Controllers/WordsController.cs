using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dictionary1WebApp.Data;
using Dictionary1WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace Dictionary1WebApp.Controllers
{
    public class WordsController : Controller {
        private readonly ApplicationDbContext _context;

        public WordsController(ApplicationDbContext context) {
            _context = context;
        }

         // GET: Words
        [Authorize]

        public async Task<IActionResult> Index()
        {
            return View(await _context.Word.ToListAsync());
        }

       
        // GET: Words/ShowSearchWords
        [Authorize]

        public async Task<IActionResult> ShowSearchWords() {
            return View();
        }

        // POST: Words/ShowSearchWords/ShowSearchResults1
        [Authorize]

        public async Task<IActionResult> ShowSearchResults(String SearchPhrase) {
            return View("Index", await _context.Word.Where(w => w.Addword.StartsWith(SearchPhrase)).ToListAsync());
        }

        //GET: Words/Language
        public IActionResult Language() {
            return View();
        }

        // GET: Words/Details/5
        [Authorize]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Word
                .FirstOrDefaultAsync(m => m.Id == id);
            if (word == null)
            {
                return NotFound();
            }

            return View(word);
        }

        [Authorize]
        // GET: Words/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Addword,WordDefinition")] Word word)
        {
            if (ModelState.IsValid)
            {
                _context.Add(word);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(word);
        }

        // GET: Words/Edit/5
        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Word.FindAsync(id);
            if (word == null)
            {
                return NotFound();
            }
            return View(word);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Addword,WordDefinition")] Word word)
        {
            if (id != word.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(word);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordExists(word.Id))
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
            return View(word);
        }



        // GET: Words/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Word
                .FirstOrDefaultAsync(m => m.Id == id);
            if (word == null)
            {
                return NotFound();
            }

            return View(word);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var word = await _context.Word.FindAsync(id);
            _context.Word.Remove(word);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordExists(int id)
        {
            return _context.Word.Any(e => e.Id == id);
        }
    }
}
