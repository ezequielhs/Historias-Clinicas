using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas_D.Data;
using Historias_Clinicas_D.Models;
using Microsoft.AspNetCore.Authorization;
using Historias_Clinicas_D.Helpers;

namespace Historias_Clinicas_D.Controllers
{
    [Authorize]
    public class TelefonosController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public TelefonosController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .Include(t => t.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        [Authorize]
        public IActionResult Create(int? personaId, string returnUrl)
        {
            Persona persona = _context.Personas.Where(p => p.Id == personaId).FirstOrDefault();

            if (persona != null)
            {
                ViewBag.Persona = persona.Id;
            }

            TempData["returnUrl"] = returnUrl;
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "NombreCompleto");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Caracteristica,Numero,Tipo,PersonaId")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                _context.Add(telefono);
                await _context.SaveChangesAsync();

                string returnUrl = TempData["returnUrl"] as string;

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "NombreCompleto", telefono.PersonaId);
            return View(telefono);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id, string returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            TempData["returnUrl"] = returnUrl;
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "NombreCompleto", telefono.PersonaId);
            return View(telefono);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Caracteristica,Numero,Tipo,PersonaId")] Telefono telefono)
        {
            if (id != telefono.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(telefono);
                    await _context.SaveChangesAsync();

                    string returnUrl = TempData["returnUrl"] as string;

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelefonoExists(telefono.Id))
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

            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "NombreCompleto", telefono.PersonaId);
            return View(telefono);
        }

        private bool TelefonoExists(int id)
        {
            return _context.Telefonos.Any(e => e.Id == id);
        }
    }
}
