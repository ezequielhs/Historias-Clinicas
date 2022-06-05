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
    public class EpisodiosController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public EpisodiosController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        public async Task<IActionResult> Index()
        {
            var historiasClinicasContext = _context.Episodios.Include(e => e.EmpleadoRegistra).Include(e => e.Paciente);
            return View(await historiasClinicasContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodios
                .Include(e => e.Epicrisis)
                .Include(e => e.Evoluciones)
                .Include(e => e.EmpleadoRegistra)
                .Include(e => e.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        public IActionResult Create(int? id, string returnUrl)
        {
            if (id != null)
            {
                ViewBag.Paciente = _context.Pacientes.Find(id.Value);
            }
            else
            {
                ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto");
            }

            ViewData["EmpleadoRegistraId"] = new SelectList(_context.Empleados, "Id", "NombreCompleto");
            TempData["returnUrl"] = returnUrl;

            return View();
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacienteId,Motivo,Descripcion,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,EmpleadoRegistraId")] Episodio episodio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(episodio);
                await _context.SaveChangesAsync();

                string returnUrl = TempData["returnUrl"] as string;

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoRegistraId"] = Generadores.GetId(User);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", episodio.PacienteId);
            return View(episodio);
        }

        [Authorize(Roles = Constantes.RolMedico)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodios.FindAsync(id);
            if (episodio == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoRegistraId"] = new SelectList(_context.Empleados, "Id", "NombreCompleto", episodio.EmpleadoRegistraId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", episodio.PacienteId);
            return View(episodio);
        }

        [Authorize(Roles = Constantes.RolMedico)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PacienteId,Motivo,Descripcion,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,EmpleadoRegistraId")] Episodio episodio)
        {
            if (id != episodio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episodio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodioExists(episodio.Id))
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
            ViewData["EmpleadoRegistraId"] = new SelectList(_context.Empleados, "Id", "NombreCompleto", episodio.EmpleadoRegistraId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", episodio.PacienteId);
            return View(episodio);
        }

        private bool EpisodioExists(int id)
        {
            return _context.Episodios.Any(e => e.Id == id);
        }
    }
}
