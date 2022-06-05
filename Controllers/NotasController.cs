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
    public class NotasController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public NotasController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        public async Task<IActionResult> Index()
        {
            var historiasClinicasContext = _context.Notas.Include(n => n.Empleado).Include(n => n.Evolucion);
            return View(await historiasClinicasContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Empleado)
                .Include(n => n.Evolucion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "NombreCompleto");
            ViewData["EvolucionId"] = new SelectList(_context.Evoluciones, "Id", "DescripcionAtencion");
            return View();
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EvolucionId,EmpleadoId,Mensaje,FechaYHora")] Nota nota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "NombreCompleto", nota.EmpleadoId);
            ViewData["EvolucionId"] = new SelectList(_context.Evoluciones, "Id", "DescripcionAtencion", nota.EvolucionId);
            return View(nota);
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.Id == id);
        }
    }
}
