using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas_D.Data;
using Historias_Clinicas_D.Models;

namespace Historias_Clinicas_D.Controllers
{
    public class PersonasController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public PersonasController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> IsUserNameEnUso(string UserName, int? Id)
        {
            return Json(IsUserNameUnico(UserName, Id));
        }

        [HttpPost]
        public async Task<IActionResult> IsEmailEnUso(string Email, int? Id)
        {
            return Json(IsEmailUnico(Email, Id));
        }

        private bool IsUserNameUnico(string UserName, int? Id)
        {
            if (!Id.HasValue)
            {
                return !_context.Personas.Any(p => p.UserName == UserName);
            }
            else
            {
                return !_context.Personas.Any(p => p.UserName == UserName && p.Id != Id);
            }
        }

        private bool IsEmailUnico(string Email, int? Id)
        {
            if (!Id.HasValue)
            {
                return !_context.Personas.Any(p => p.Email == Email);
            }
            else
            {
                return !_context.Personas.Any(p => p.Email == Email && p.Id != Id);
            }
        }
    }
}