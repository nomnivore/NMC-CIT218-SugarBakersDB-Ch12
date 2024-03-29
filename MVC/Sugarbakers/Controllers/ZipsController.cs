﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sugarbakers.Models;

namespace Sugarbakers.Controllers
{
    public class ZipsController : Controller
    {
        private readonly SugarbakersDbContext _context;

        public ZipsController(SugarbakersDbContext context)
        {
            _context = context;
        }

        // GET: Zips
        public async Task<IActionResult> Index()
        {
              return View(await _context.Zips.ToListAsync());
        }

        // GET: Zips/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Zips == null)
            {
                return NotFound();
            }

            var zip = await _context.Zips
                .FirstOrDefaultAsync(m => m.Zipcode == id);
            if (zip == null)
            {
                return NotFound();
            }

            return View(zip);
        }

        // GET: Zips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Zipcode,City,State")] Zip zip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zip);
        }

        // GET: Zips/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Zips == null)
            {
                return NotFound();
            }

            var zip = await _context.Zips.FindAsync(id);
            if (zip == null)
            {
                return NotFound();
            }
            return View(zip);
        }

        // POST: Zips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Zipcode,City,State")] Zip zip)
        {
            if (id != zip.Zipcode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZipExists(zip.Zipcode))
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
            return View(zip);
        }

        // GET: Zips/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Zips == null)
            {
                return NotFound();
            }

            var zip = await _context.Zips
                .FirstOrDefaultAsync(m => m.Zipcode == id);
            if (zip == null)
            {
                return NotFound();
            }

            return View(zip);
        }

        // POST: Zips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Zips == null)
            {
                return Problem("Entity set 'SugarbakersDbContext.Zips'  is null.");
            }
            var zip = await _context.Zips.FindAsync(id);
            if (zip != null)
            {
                _context.Zips.Remove(zip);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZipExists(string id)
        {
          return _context.Zips.Any(e => e.Zipcode == id);
        }
    }
}
