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
    public class EpisodiosController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public EpisodiosController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: Episodios
        public async Task<IActionResult> Index()
        {
            var historiasClinicasContext = _context.Episodios.Include(e => e.EmpleadoRegistra).Include(e => e.Paciente);
            return View(await historiasClinicasContext.ToListAsync());
        }

        // GET: Episodios/Details/5
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

        // GET: Episodios/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoRegistraId"] = new SelectList(_context.Empleados, "Id", "Apellido");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Apellido");
            return View();
        }

        // POST: Episodios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacienteId,Motivo,Descripcion,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,EmpleadoRegistraId")] Episodio episodio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(episodio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoRegistraId"] = new SelectList(_context.Empleados, "Id", "Apellido", episodio.EmpleadoRegistraId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Apellido", episodio.PacienteId);
            return View(episodio);
        }

        // GET: Episodios/Edit/5
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
            ViewData["EmpleadoRegistraId"] = new SelectList(_context.Empleados, "Id", "Apellido", episodio.EmpleadoRegistraId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Apellido", episodio.PacienteId);
            return View(episodio);
        }

        // POST: Episodios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["EmpleadoRegistraId"] = new SelectList(_context.Empleados, "Id", "Apellido", episodio.EmpleadoRegistraId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Apellido", episodio.PacienteId);
            return View(episodio);
        }

        // GET: Episodios/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
                .FirstOrDefaultAsync(e => e.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        // POST: Episodios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var episodio = await _context.Episodios.FindAsync(id);
            _context.Episodios.Remove(episodio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodioExists(int id)
        {
            return _context.Episodios.Any(e => e.Id == id);
        }
    }
}
