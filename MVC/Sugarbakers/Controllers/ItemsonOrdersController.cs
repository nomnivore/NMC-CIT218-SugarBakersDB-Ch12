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
    public class ItemsonOrdersController : Controller
    {
        private readonly SugarbakersDbContext _context;

        public ItemsonOrdersController(SugarbakersDbContext context)
        {
            _context = context;
        }

        // GET: ItemsonOrders
        public async Task<IActionResult> Index()
        {
            var sugarbakersDbContext = _context.ItemsonOrders.Include(i => i.Orders).Include(i => i.Products);
            return View(await sugarbakersDbContext.ToListAsync());
        }

        // GET: ItemsonOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ItemsonOrders == null)
            {
                return NotFound();
            }

            var itemsonOrder = await _context.ItemsonOrders
                .Include(i => i.Orders)
                .Include(i => i.Products)
                .FirstOrDefaultAsync(m => m.OrdersId == id);
            if (itemsonOrder == null)
            {
                return NotFound();
            }

            return View(itemsonOrder);
        }

        // GET: ItemsonOrders/Create
        public IActionResult Create()
        {
            ViewData["OrdersId"] = new SelectList(_context.Orders, "OrdersId", "OrdersId");
            ViewData["ProductsId"] = new SelectList(_context.Products, "ProductsId", "ProductsId");
            return View();
        }

        // POST: ItemsonOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdersId,ProductsId,Quantity,UnitPrice,LineItemTotal,ShipDate")] ItemsonOrder itemsonOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemsonOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrdersId"] = new SelectList(_context.Orders, "OrdersId", "OrdersId", itemsonOrder.OrdersId);
            ViewData["ProductsId"] = new SelectList(_context.Products, "ProductsId", "ProductsId", itemsonOrder.ProductsId);
            return View(itemsonOrder);
        }

        // GET: ItemsonOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ItemsonOrders == null)
            {
                return NotFound();
            }

            var itemsonOrder = await _context.ItemsonOrders.FindAsync(id);
            if (itemsonOrder == null)
            {
                return NotFound();
            }
            ViewData["OrdersId"] = new SelectList(_context.Orders, "OrdersId", "OrdersId", itemsonOrder.OrdersId);
            ViewData["ProductsId"] = new SelectList(_context.Products, "ProductsId", "ProductsId", itemsonOrder.ProductsId);
            return View(itemsonOrder);
        }

        // POST: ItemsonOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdersId,ProductsId,Quantity,UnitPrice,LineItemTotal,ShipDate")] ItemsonOrder itemsonOrder)
        {
            if (id != itemsonOrder.OrdersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemsonOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsonOrderExists(itemsonOrder.OrdersId))
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
            ViewData["OrdersId"] = new SelectList(_context.Orders, "OrdersId", "OrdersId", itemsonOrder.OrdersId);
            ViewData["ProductsId"] = new SelectList(_context.Products, "ProductsId", "ProductsId", itemsonOrder.ProductsId);
            return View(itemsonOrder);
        }

        // GET: ItemsonOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ItemsonOrders == null)
            {
                return NotFound();
            }

            var itemsonOrder = await _context.ItemsonOrders
                .Include(i => i.Orders)
                .Include(i => i.Products)
                .FirstOrDefaultAsync(m => m.OrdersId == id);
            if (itemsonOrder == null)
            {
                return NotFound();
            }

            return View(itemsonOrder);
        }

        // POST: ItemsonOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ItemsonOrders == null)
            {
                return Problem("Entity set 'SugarbakersDbContext.ItemsonOrders'  is null.");
            }
            var itemsonOrder = await _context.ItemsonOrders.FindAsync(id);
            if (itemsonOrder != null)
            {
                _context.ItemsonOrders.Remove(itemsonOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsonOrderExists(int id)
        {
          return _context.ItemsonOrders.Any(e => e.OrdersId == id);
        }
    }
}
