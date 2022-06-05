using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas_D.Data;
using Historias_Clinicas_D.Models;
using Historias_Clinicas_D.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Historias_Clinicas_D.Helpers;
using Historias_Clinicas_D.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Historias_Clinicas_D.Controllers
{
    public class MedicosController : Controller
    {
        private readonly HistoriasClinicasContext _context;
        private readonly UserManager<Persona> _userManager;

        public MedicosController(HistoriasClinicasContext context, UserManager<Persona> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicos.ToListAsync());
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Include(m => m.Telefonos)
                .Include(m => m.Direccion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        public IActionResult Create(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistroMedico viewModel)
        {
            if (ModelState.IsValid)
            {
                Medico medicoARegistrar = new Medico()
                {
                    Legajo = viewModel.Legajo,
                    Nombre = viewModel.Nombre,
                    Apellido = viewModel.Apellido,
                    DNI = viewModel.DNI,
                    TipoMatricula = viewModel.TipoMatricula,
                    Matricula = viewModel.Matricula,
                    Especialidad = viewModel.Especialidad,
                    Email = viewModel.Email,
                    UserName = viewModel.Email
                };

                var resultado = await _userManager.CreateAsync(medicoARegistrar, viewModel.Password);

                if (resultado.Succeeded)
                {
                    await _userManager.AddToRoleAsync(medicoARegistrar, Constantes.RolMedico);

                    string returnUrl = TempData["returnUrl"] as string;

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Medicos");
                }

                ModelState.AddModelError(string.Empty, "Contraseña invalida");
            }
            return View();
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        public async Task<IActionResult> Edit(int? id, string returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }

            TempData["returnUrl"] = returnUrl;
            return View(medico);
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoMatricula,Matricula,Especialidad,Legajo,Id,Nombre,Apellido,DNI,FechaAlta,Email")] Medico medico)
        {
            if (id != medico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Medico medEnDb = _context.Medicos.Find(medico.Id);

                    medEnDb.Email = medico.Email;
                    medEnDb.UserName = medico.Email;
                    medEnDb.NormalizedUserName = medico.Email;

                    _context.Update(medEnDb);
                    await _context.SaveChangesAsync();

                    string returnUrl = TempData["returnUrl"] as string;

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.Id))
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
            return View(medico);
        }

        public async Task<IActionResult> MiPerfil()
        {
            if (User.Identity.IsAuthenticated)
            {
                int idMedico = Generadores.GetId(User);

                if (!_context.Medicos.Any(p => p.Id == idMedico))
                {
                    return NotFound();
                }

                Medico medicoEnDb = _context.Medicos.Find(idMedico);

                medicoEnDb = await _context.Medicos
                    .Include(p => p.Telefonos)
                    .Include(p => p.Direccion)
                    .FirstOrDefaultAsync(m => m.Id == idMedico);

                return View(medicoEnDb);
            }
         
            return View("LogIn", "Account");
        }

        [HttpPost]
        public ActionResult IsMatriculaEnUso(string Matricula)
        {
            bool resultado;

            if (_context.Medicos.Any(m => m.Matricula.Equals(Matricula) && !m.Matricula.Equals("No Posee")))
            {
                resultado = false;
            }
            else
            {
                resultado = true;
            }

            return Json(resultado);
        }

        private bool MedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.Id == id);
        }
    }
}
