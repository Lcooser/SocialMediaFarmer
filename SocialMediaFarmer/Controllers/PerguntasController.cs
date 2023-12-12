using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaFarmer.Models;

namespace SocialMediaFarmer.Controllers
{
    [Authorize]
    public class PerguntasController : Controller
    {
        private readonly Contexto _context;

        public PerguntasController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var perguntasRespostas = _context.Pergunta
                .Include(p => p.Usuario)
                .Include(p => p.Respostas)
                .ThenInclude(r => r.Usuario)
                .Select(p => new PerguntaRespostaViewModel
                {
                    PerguntaId = p.Id,
                    Topico = p.Topico,
                    Pergunta = p.ConteudoPergunta,
                    DataPublicacao = p.DataPublicacao,
                    Resolvida = p.Resolvida,
                    NomeUsuario = p.Usuario.Nome,
                    Respostas = p.Respostas.Select(r => new RespostaViewModel
                    {
                        RespostaId = r.Id,
                        Conteudo = r.Conteudo,
                        DataPublicacao = r.DataPublicacao,
                        NomeUsuario = r.Usuario.Nome
                    }).ToList()
                })
                .ToList();

            return View(perguntasRespostas);
        }

       
        [HttpGet]
        public IActionResult FazerPergunta()
        {
            if (!User.Identity.IsAuthenticated)
            {
            
                return RedirectToAction("Login", "Home", new { returnUrl = Url.Action("FazerPergunta", "Perguntas") });
            }

            return View();
        }

        [HttpPost]
        public IActionResult FazerPergunta(Pergunta pergunta)
        {
            if (!User.Identity.IsAuthenticated)
            
                return RedirectToAction("Login", "Home", new { returnUrl = Url.Action("FazerPergunta", "Perguntas") });
            }

            if (ModelState.IsValid)
          
                var usuarioId = ObterIdUsuarioLogado();
                pergunta.UsuarioId = usuarioId;

           
                _context.Pergunta.Add(pergunta);
                _context.SaveChanges();

           
                return RedirectToAction("Index");
            }

            return View(pergunta);
        }



        private int ObterIdUsuarioLogado()
        {
           
            if (User.Identity.IsAuthenticated)
            {
           
                var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);

              
                if (idClaim != null && int.TryParse(idClaim.Value, out int userId))
                {
                    // Retorna o ID do usuário
                    return userId;
                }
            }

           
            return -1;
        }
        [HttpPost]
        public IActionResult Responder(int perguntaId, string resposta)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }

            var usuarioId = ObterIdUsuarioLogado(); 
            var respostaModel = new Resposta
            {
                Conteudo = resposta,
                DataPublicacao = DateTime.Now,
                UsuarioId = usuarioId,
                PerguntaId = perguntaId 
            };

            _context.Respostas.Add(respostaModel);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
