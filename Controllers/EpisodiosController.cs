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
using Historias_Clinicas_D.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Historias_Clinicas_D.Controllers
{
    public class EpisodiosController : Controller
    {
        private readonly HistoriasClinicasContext _context;
        private readonly UserManager<Persona> _userManager;

        public EpisodiosController(HistoriasClinicasContext context, UserManager<Persona> userManager)
        {
            _context = context;
            this._userManager = userManager;
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
        public IActionResult Create(int? pacienteId, int? empleadoRegistraId, string returnUrl)
        {
            Paciente paciente = _context.Pacientes.Where(e => e.Id == pacienteId).FirstOrDefault();
            Empleado empleadoRegistra = _context.Empleados.Where(e => e.Id == empleadoRegistraId).FirstOrDefault();

            if (paciente != null)
            {
                ViewBag.Paciente = paciente.Id;
            }

            if (empleadoRegistra != null)
            {
                ViewBag.Empleado = empleadoRegistra.Id;
            }

            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto");
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

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        [HttpGet]
        public async Task<IActionResult> CerrarEpisodio(int? id, string returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodios.Include(e => e.Evoluciones).FirstOrDefaultAsync(m => m.Id == id);

            if (episodio == null)
            {
                return NotFound();
            }

            TempData["returnUrl"] = returnUrl;
            
            return View(episodio);
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CerrarEpisodio(int id, Episodio episodio)
        {
            if (id != episodio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!this.TieneEvolucionesAbiertas(id))
                {
                    _context.Update(episodio);
                    await _context.SaveChangesAsync();

                    string returnUrl = TempData["returnUrl"] as string;

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Create", "Epicrisis", new { empleadoId = _userManager.GetUserId(this.User), episodioId = id, returnUrl });
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, MensajesError.ErrEvolucionesPendientes);
                }
            }

            return View();
        }

        private bool EpisodioExists(int id)
        {
            return _context.Episodios.Any(e => e.Id == id);
        }

        private bool TieneEvolucionesAbiertas(int id)
        {
            bool resultado = false;

            List<Evolucion> evoluciones = _context.Evoluciones.Where(e => e.EpisodioId == id).ToList();
            
            foreach(Evolucion evolucion in evoluciones)
            {
                if (evolucion.EstadoAbierto)
                {
                    resultado = true;
                }
            }

            return resultado;
        }
    }
}
