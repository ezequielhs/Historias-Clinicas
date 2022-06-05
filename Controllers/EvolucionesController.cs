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
     public class EvolucionesController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public EvolucionesController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        public async Task<IActionResult> Index()
        {
            var historiasClinicasContext = _context.Evoluciones
                .Include(e => e.Medico)
                .Include(e => e.Notas)
                .ToList();

            return View(historiasClinicasContext);
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones
                .Include(e => e.Medico)
                .Include(e => e.Notas)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        [Authorize(Roles = Constantes.RolMedico)]
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "NombreCompleto");
            return View();
        }

        [Authorize(Roles = Constantes.RolMedico)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedicoId,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,DescripcionAtencion,EstadoAbierto")] Evolucion evolucion)
        {
            if (ModelState.IsValid)
            {
                _context.Evoluciones.Add(evolucion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Evoluciones");
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "NombreCompleto", evolucion.MedicoId);
            return View(evolucion);
        }

        [Authorize(Roles = Constantes.RolMedico)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones.FindAsync(id);
            if (evolucion == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "NombreCompleto", evolucion.MedicoId);
            return View(evolucion);
        }

        [Authorize(Roles = Constantes.RolMedico)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicoId,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,DescripcionAtencion,EstadoAbierto")] Evolucion evolucion)
        {
            if (id != evolucion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evolucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvolucionExists(evolucion.Id))
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
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "NombreCompleto", evolucion.MedicoId);
            return View(evolucion);
        }

        private bool EvolucionExists(int id)
        {
            return _context.Evoluciones.Any(e => e.Id == id);
        }
    }
}
