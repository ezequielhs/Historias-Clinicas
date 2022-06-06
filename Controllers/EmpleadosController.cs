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
    public class EmpleadosController : Controller
    {
        private readonly HistoriasClinicasContext _context;
        private readonly UserManager<Persona> _userManager;

        public EmpleadosController(HistoriasClinicasContext context, UserManager<Persona> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empleados.ToListAsync());
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Direccion)
                .Include(e => e.Telefonos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
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
        public async Task<IActionResult> Create(RegistroEmpleado viewModel)
        {
            if (ModelState.IsValid)
            {
                Empleado empleadoARegistrar = new Empleado()
                {
                    Legajo = viewModel.Legajo,
                    Nombre = viewModel.Nombre,
                    Apellido = viewModel.Apellido,
                    DNI = viewModel.DNI,
                    Email = viewModel.Email,
                    UserName = viewModel.Email
                };

                var resultado = await _userManager.CreateAsync(empleadoARegistrar, viewModel.Password);

                if (resultado.Succeeded)
                {
                    await _userManager.AddToRoleAsync(empleadoARegistrar, Constantes.RolEmpleado);

                    string returnUrl = TempData["returnUrl"] as string;

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Empleados");
                }

                ModelState.AddModelError(string.Empty, "Contraseña invalida");
            }
            return View();
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        public async Task<IActionResult> Edit(int? id, string returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            TempData["returnUrl"] = returnUrl;
            return View(empleado);
        }

        [Authorize(Roles = Constantes.RolEmpleado)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Legajo,Id,Nombre,Apellido,DNI,FechaAlta,Email")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Empleado empleadoEnDb = _context.Empleados.Find(empleado.Id);

                    empleadoEnDb.Email = empleado.Email;
                    empleadoEnDb.UserName = empleado.Email;
                    empleadoEnDb.NormalizedUserName = empleado.Email;

                    _context.Update(empleadoEnDb);
                    await _context.SaveChangesAsync();

                    string returnUrl = TempData["returnUrl"] as string;

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            return View(empleado);
        }

        public async Task<IActionResult> MiPerfil()
        {
            if (User.Identity.IsAuthenticated)
            {
                int idEmpleado = Generadores.GetId(User);
                if (!_context.Empleados.Any(p => p.Id == idEmpleado))
                {
                    return NotFound();
                }

                Empleado empleadoEnDb = _context.Empleados.Find(idEmpleado);

                empleadoEnDb = await _context.Empleados
                    .Include(p => p.Telefonos)
                    .Include(p => p.Direccion)
                    .FirstOrDefaultAsync(m => m.Id == idEmpleado);

                return View(empleadoEnDb);

            }

            return RedirectToAction("LogIn", "Account");
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
