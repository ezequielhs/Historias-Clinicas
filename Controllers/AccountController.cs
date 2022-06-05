using Historias_Clinicas_D.Data;
using Historias_Clinicas_D.Helpers;
using Historias_Clinicas_D.Models;
using Historias_Clinicas_D.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Historias_Clinicas_D.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<Persona> _userManager;
		private readonly SignInManager<Persona> _signInManager;
		private readonly RoleManager<IdentityRole<int>> _rolManager;
		private readonly HistoriasClinicasContext _contexto;

		public AccountController(
			UserManager<Persona> userManager, 
			SignInManager<Persona> signInManager,
			RoleManager<IdentityRole<int>> rolManager,
			HistoriasClinicasContext contexto)
		{
			this._userManager = userManager;
			this._signInManager = signInManager;
			this._rolManager = rolManager;
			this._contexto = contexto;
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

				ModelState.AddModelError(string.Empty, MensajesError.ErrLogIn);
			}

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("LogIn", "Account");
		}

		[HttpGet]
		[Authorize]
		public IActionResult AccesoDenegado()
		{
			return View();
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

				if(resultado.Succeeded)
				{
					await _userManager.AddToRoleAsync(pacienteARegistrar, Constantes.RolPaciente);
					await _signInManager.SignInAsync(pacienteARegistrar, isPersistent: false);
					
					return RedirectToAction("MiPerfil", "Pacientes"); 
				}
				ModelState.AddModelError(string.Empty, "Contraseña invalida");
			}
			return View();
		}

		[HttpGet]
		[Authorize]
		public IActionResult CambiarPassword()
        {
			return View();
        }

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CambiarPassword(CambiarPassword viewModel)
        {
			if (ModelState.IsValid)
            {
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
                {
					return RedirectToAction("LogIn");
                }

				var result = await _userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);

				if (result.Succeeded)
                {
					await _signInManager.RefreshSignInAsync(user);

					return View("CambiarPasswordConfirmacion");
                }

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, MensajesError.ErrCurrentPassword);
				}
			}

			return View(viewModel);
        }
	}
}
