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
using Historias_Clinicas_D.Helpers;
using Microsoft.AspNetCore.Authorization;
using Historias_Clinicas_D.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Historias_Clinicas_D.Controllers
{
    public class PacientesController : Controller
    {
        private readonly HistoriasClinicasContext _context;
        private readonly UserManager<Persona> _userManager;

        public PacientesController(HistoriasClinicasContext context, UserManager<Persona> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        public async Task<IActionResult> Index(string nombrebuscado)
        {
            IEnumerable<Paciente> pacientes;
            if (!string.IsNullOrEmpty(nombrebuscado))
            {
                pacientes = _context.Pacientes.Where(p=> p.Nombre.Contains(nombrebuscado) || p.Apellido.Contains(nombrebuscado));
            }
            else
            {
                pacientes = _context.Pacientes;
            }

            return View(pacientes);
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolMedico)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.Telefonos)
                .Include(p => p.Direccion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        public IActionResult Create(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;

            return View();
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistroPaciente viewModel)
        {
            if (ModelState.IsValid)
            {
                Paciente pacienteARegistrar = new Paciente()
                {
                    Nombre = viewModel.Nombre,
                    Apellido = viewModel.Apellido,
                    DNI = viewModel.DNI,
                    ObraSocial = viewModel.ObraSocial,
                    Email = viewModel.Email,
                    UserName = viewModel.Email
                };

                var resultado = await _userManager.CreateAsync(pacienteARegistrar, viewModel.Password);

                if (resultado.Succeeded)
                {
                    await _userManager.AddToRoleAsync(pacienteARegistrar, Constantes.RolPaciente);

                    string returnUrl = TempData["returnUrl"] as string;

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Pacientes");
                }

                ModelState.AddModelError(string.Empty, "Contraseña invalida");
            }
            return View();
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolPaciente)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        [Authorize(Roles = Constantes.RolEmpleado + ", " + Constantes.RolPaciente)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObraSocial,Id,Nombre,Apellido,DNI,FechaAlta,Email")] Paciente paciente)
        {
            //Seria una mejor practica hacer un view model
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Paciente pntEnDb = _context.Pacientes.Find(paciente.Id);

                    pntEnDb.ObraSocial = paciente.ObraSocial;
                    pntEnDb.Nombre = paciente.Nombre;
                    pntEnDb.Apellido = paciente.Apellido;
                    pntEnDb.DNI = paciente.DNI;
                    pntEnDb.Email = paciente.Email;
                    pntEnDb.UserName = paciente.Email;
                    pntEnDb.NormalizedUserName = paciente.Email;

                    _context.Update(pntEnDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }

        public async Task<IActionResult> MiPerfil()
        {
            if (User.Identity.IsAuthenticated)
            {
                int idPaciente = Generadores.GetId(User);
                if(!_context.Pacientes.Any(p => p.Id == idPaciente))
                {
                    return NotFound();
                }

                Paciente pacienteEnDb = _context.Pacientes.Find(idPaciente);

                pacienteEnDb = await _context.Pacientes
                    .Include(p => p.Telefonos)
                    .Include(p => p.Direccion)
                    .FirstOrDefaultAsync(m => m.Id == idPaciente);

                return View(pacienteEnDb);

            }

            return RedirectToAction("LogIn", "Account");
        }

        [Authorize]
        public async Task<IActionResult> HistoriaClinica(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.Episodios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
