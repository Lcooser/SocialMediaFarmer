using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SocialMediaFarmer.Models;

namespace SocialMediaFarmer.Controllers
{
    public class RespostasController : Controller
    {
        private readonly Contexto _context;

        public RespostasController(Contexto context)
        {
            _context = context;
        }

        // GET: Respostas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Resposta.Include(r => r.Pergunta).Include(r => r.Usuario);
            return View(await contexto.ToListAsync());
        }

        // GET: Respostas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resposta == null)
            {
                return NotFound();
            }

            var resposta = await _context.Resposta
                .Include(r => r.Pergunta)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resposta == null)
            {
                return NotFound();
            }

            return View(resposta);
        }

        // GET: Respostas/Create
        public IActionResult Create()
        {
            ViewData["PerguntaId"] = new SelectList(_context.Pergunta, "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Respostas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Conteudo,DataPublicacao,Aceita,PerguntaId,UsuarioId")] Resposta resposta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resposta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PerguntaId"] = new SelectList(_context.Pergunta, "Id", "Id", resposta.PerguntaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", resposta.UsuarioId);
            return View(resposta);
        }

        // GET: Respostas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resposta == null)
            {
                return NotFound();
            }

            var resposta = await _context.Resposta.FindAsync(id);
            if (resposta == null)
            {
                return NotFound();
            }
            ViewData["PerguntaId"] = new SelectList(_context.Pergunta, "Id", "Id", resposta.PerguntaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", resposta.UsuarioId);
            return View(resposta);
        }

        // POST: Respostas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Conteudo,DataPublicacao,Aceita,PerguntaId,UsuarioId")] Resposta resposta)
        {
            if (id != resposta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resposta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespostaExists(resposta.Id))
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
            ViewData["PerguntaId"] = new SelectList(_context.Pergunta, "Id", "Id", resposta.PerguntaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", resposta.UsuarioId);
            return View(resposta);
        }

        // GET: Respostas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resposta == null)
            {
                return NotFound();
            }

            var resposta = await _context.Resposta
                .Include(r => r.Pergunta)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resposta == null)
            {
                return NotFound();
            }

            return View(resposta);
        }

        // POST: Respostas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resposta == null)
            {
                return Problem("Entity set 'Contexto.Resposta'  is null.");
            }
            var resposta = await _context.Resposta.FindAsync(id);
            if (resposta != null)
            {
                _context.Resposta.Remove(resposta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RespostaExists(int id)
        {
          return (_context.Resposta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
