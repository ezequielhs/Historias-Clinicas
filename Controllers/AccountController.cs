using Historias_Clinicas_D.Data;
using Historias_Clinicas_D.Helpers;
using Historias_Clinicas_D.Models;
using Historias_Clinicas_D.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Historias_Clinicas_D.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _rolManager;
        private readonly HistoriasClinicasContext _context;

        public AccountController(
            UserManager<Persona> userManager,
            SignInManager<Persona> signInManager,
            RoleManager<IdentityRole<int>> rolManager,
            HistoriasClinicasContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._rolManager = rolManager;
            this._context = context;
        }

        [HttpGet]
        public IActionResult RegistrarPaciente()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPaciente(RegistroPaciente viewModel)
        {
            if (ModelState.IsValid)
            {
                Paciente paciente = new Paciente()
                {
                    Nombre = viewModel.Nombre,
                    Apellido = viewModel.Apellido,
                    DNI = viewModel.DNI,
                    ObraSocial = viewModel.ObraSocial,
                    Email = viewModel.Email,
                    UserName = viewModel.Email
                };

                var resultado = await _userManager.CreateAsync(paciente, viewModel.Password);

                if (resultado.Succeeded)
                {
                    await _userManager.AddToRoleAsync(paciente, Constantes.RolPaciente);
                    await _signInManager.SignInAsync(paciente, isPersistent: false);

                    return RedirectToAction("Index", "Pacientes");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult LogIn(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogIn viewModel)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(
                    viewModel.Email,
                    viewModel.Password,
                    isPersistent: viewModel.RememberMe,
                    lockoutOnFailure: false
                );

                if (resultado.Succeeded)
                {
                    string returnUrl = TempData["returnUrl"] as string;
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, MensajesError.ErrLogin);

            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("IniciarSesion", "Account");
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
