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
using Microsoft.AspNetCore.Identity;

namespace Historias_Clinicas_D.Controllers
{
    public class EpicrisisController : Controller
    {
        private readonly HistoriasClinicasContext _context;
        

        public EpicrisisController(HistoriasClinicasContext context)
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

            var epicrisis = await _context.Epicrisis
                .Include(e => e.Episodio)
                .Include(e => e.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epicrisis == null)
            {
                return NotFound();
            }

            return View(epicrisis);
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        public IActionResult Create(int? empleadoId, int? episodioId, string returnUrl)
        {
            Empleado empleado = _context.Empleados.Where(e => e.Id == empleadoId).FirstOrDefault();
            Episodio episodio = _context.Episodios.Where(e => e.Id == episodioId).FirstOrDefault();

            if (empleadoId == null && empleado == null)
            {
                return NotFound();
            }

            if (episodioId == null && episodio == null)
            {
                return NotFound();
            }

            ViewBag.EmpleadoId = empleado.Id;
            ViewBag.EpisodioId = episodio.Id;
            TempData["returnUrl"] = returnUrl;

            return View();
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EpisodioId,EmpleadoId,FechaYHora,Diagnostico,Recomendacion")] Epicrisis epicrisis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epicrisis);
                await _context.SaveChangesAsync();

                string returnUrl = TempData["returnUrl"] as string;

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }
            return View(epicrisis);
        }

        private bool EpicrisisExists(int id)
        {
            return _context.Epicrisis.Any(e => e.Id == id);
        }
    }
}
