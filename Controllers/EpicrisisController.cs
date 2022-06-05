using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas_D.Data;
using Historias_Clinicas_D.Models;
using Historias_Clinicas_D.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Historias_Clinicas_D.Controllers
{
    public class EpicrisisController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public EpicrisisController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        public async Task<IActionResult> Index()
        {
            var historiasClinicasContext = _context.Epicrisis
                .Include(e => e.Episodio)
                .Include(e => e.Medico)
                .ToList();
            return View(historiasClinicasContext);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epicrisis = await _context.Epicrisis
                .Include(e => e.Episodio)
                .Include(e => e.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epicrisis == null)
            {
                return NotFound();
            }

            return View(epicrisis);
        }

        [Authorize(Roles = Constantes.RolMedico)]
        public IActionResult Create()
        {
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion");
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "NombreCompleto");
            return View();
        }

        [Authorize(Roles = Constantes.RolMedico)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EpisodioId,MedicoId,FechaYHora,Diagnostico,Recomendacion")] Epicrisis epicrisis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epicrisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", epicrisis.EpisodioId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "NombreCompleto", epicrisis.MedicoId);
            return View(epicrisis);
        }
        private bool EpicrisisExists(int id)
        {
            return _context.Epicrisis.Any(e => e.Id == id);
        }
    }
}
