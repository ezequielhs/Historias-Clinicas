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
    public class DireccionesController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public DireccionesController(HistoriasClinicasContext context)
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

            var direccion = await _context.Direcciones
                .Include(d => d.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        [Authorize]
        public IActionResult Create(int? personaId, string returnUrl)
        {
            Persona persona = _context.Personas.Where(p => p.Id == personaId).FirstOrDefault();

            if (persona != null)
            {
                ViewBag.Persona = persona.Id;
            }

            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "NombreCompleto");
            TempData["returnUrl"] = returnUrl;
            
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Calle,Numero,CodigoPostal,Localidad,Provincia,PersonaId")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(direccion);
                await _context.SaveChangesAsync();

                string returnUrl = TempData["returnUrl"] as string;

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "NombreCompleto", direccion.PersonaId);
            return View(direccion);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id, string returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }

            TempData["returnUrl"] = returnUrl;
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "NombreCompleto", direccion.PersonaId);
            return View(direccion);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Calle,Numero,CodigoPostal,Localidad,Provincia,PersonaId")] Direccion direccion)
        {
            if (id != direccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direccion);
                    await _context.SaveChangesAsync();
                    string returnUrl = TempData["returnUrl"] as string;

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionExists(direccion.Id))
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
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "NombreCompleto", direccion.PersonaId);
            return View(direccion);
        }

        private bool DireccionExists(int id)
        {
            return _context.Direcciones.Any(e => e.Id == id);
        }
    }
}
