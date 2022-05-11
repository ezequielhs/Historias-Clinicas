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
    public class CartillaController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public CartillaController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Medicos.Include(m => m.Direccion).ToList());
        }

        [HttpPost]
        public IActionResult Index(Especialidad especialidad)
        {
            var medicos = _context.Medicos.Where(m => m.Especialidad == especialidad).Include(m => m.Direccion).ToList();
            return View(medicos);
        }

        private bool MedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.Id == id);
        }
    }
}
