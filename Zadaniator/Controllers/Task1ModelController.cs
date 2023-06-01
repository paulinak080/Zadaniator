using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zadaniator.Data;
using Zadaniator.Models;
using Microsoft.AspNetCore.Authorization;

namespace Zadaniator.Controllers
{
    [Authorize]
    public class Task1ModelController : Controller
    {
        private readonly ApplicationDbContext _context;


        private readonly UserManager<IdentityUser> _userManager;
        public Task1ModelController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

            // GET: Task1Model
            public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var applicationDbContext = _context.Task1Model.Include(t => t.User).Where(u => u.User == user);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Zadanie/Search
        public async Task<IActionResult> Search()
        {
            return _context.Task1Model != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.Task1Model'  is null.");
        }
        // POST: Zadanie/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return _context.Task1Model != null ?
                        View("Index", await _context.Task1Model.Where(j => j.Name.Contains(SearchPhrase)).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Task1Model'  is null.");
        }
            // GET: Task1Model/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Task1Model == null)
            {
                return NotFound();
            }

            var task1Model = await _context.Task1Model
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task1Model == null)
            {
                return NotFound();
            }

            return View(task1Model);
        }

        // GET: Task1Model/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Task1Model/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Data,IsCompleted,UserId")] Task1ModelDTO task1ModelDTO)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var task1Model = new Task1Model
            {
                Id = task1ModelDTO.Id,
                Name = task1ModelDTO.Name,
                Description = task1ModelDTO.Description,
                Data = task1ModelDTO.Data,
                IsCompleted = task1ModelDTO.IsCompleted,
                UserId = user.Id,
            };
            if (ModelState.IsValid)
            {
                _context.Add(task1Model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", task1Model.UserId);
            return View(task1Model);
        }

        // GET: Task1Model/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Task1Model == null)
            {
                return NotFound();
            }

            var task1Model = await _context.Task1Model.FindAsync(id);
            if (task1Model == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", task1Model.UserId);
            return View(task1Model);
        }

        // POST: Task1Model/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Data,IsCompleted,UserId")] Task1ModelDTO task1ModelDTO)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var task1Model = new Task1Model
            {
                Id = task1ModelDTO.Id,
                Name = task1ModelDTO.Name,
                Description = task1ModelDTO.Description,
                Data = task1ModelDTO.Data,
                IsCompleted = task1ModelDTO.IsCompleted,
                UserId = user.Id,
            };
            if (id != task1Model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task1Model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Task1ModelExists(task1Model.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", task1Model.UserId);
            return View(task1Model);
        }

        // GET: Task1Model/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Task1Model == null)
            {
                return NotFound();
            }

            var task1Model = await _context.Task1Model
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task1Model == null)
            {
                return NotFound();
            }

            return View(task1Model);
        }

        // POST: Task1Model/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Task1Model == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Task1Model'  is null.");
            }
            var task1Model = await _context.Task1Model.FindAsync(id);
            if (task1Model != null)
            {
                _context.Task1Model.Remove(task1Model);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Task1ModelExists(int id)
        {
          return (_context.Task1Model?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
