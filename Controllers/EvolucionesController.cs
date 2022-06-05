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

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones
                .Include(e => e.Medico)
                .Include(e => e.Episodio)
                .Include(e => e.Notas)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        [Authorize(Roles = Constantes.RolMedico)]
        public IActionResult Create(int? medicoId, int? episodioId, string returnUrl)
        {
            Medico medico = _context.Medicos.Where(e => e.Id == medicoId).FirstOrDefault();
            Episodio episodio = _context.Episodios.Where(e => e.Id == episodioId).FirstOrDefault();

            if (medico != null)
            {
                ViewBag.Medico = medico.Id;
            }

            if (episodio != null)
            {
                ViewBag.Episodio = episodio.Id;
            }

            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "NombreCompleto");
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Motivo");
            TempData["returnUrl"] = returnUrl;

            return View();
        }

        [Authorize(Roles = Constantes.RolMedico)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedicoId,EpisodioId,FechaYHoraAlta,FechaYHoraCierre,DescripcionAtencion")] Evolucion evolucion)
        {
            if (ModelState.IsValid)
            {
                _context.Evoluciones.Add(evolucion);
                await _context.SaveChangesAsync();

                string returnUrl = TempData["returnUrl"] as string;

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index","Evoluciones");
            }

            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "NombreCompleto", evolucion.MedicoId);
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Motivo");

            return View(evolucion);
        }

        [Authorize(Roles = Constantes.RolMedico)]
        [HttpGet]
        public async Task<IActionResult> CerrarEvolucion(int? id, string returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones.FirstOrDefaultAsync(e => e.Id == id);

            if (evolucion == null)
            {
                return NotFound();
            }

            TempData["returnUrl"] = returnUrl;

            return View(evolucion);
        }

        [Authorize(Roles = Constantes.RolMedico)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CerrarEvolucion(int id, Evolucion evolucion)
        {
            if (id != evolucion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(evolucion);
                await _context.SaveChangesAsync();

                string returnUrl = TempData["returnUrl"] as string;

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }

            return View();
        }

        private bool EvolucionExists(int id)
        {
            return _context.Evoluciones.Any(e => e.Id == id);
        }
    }
}
