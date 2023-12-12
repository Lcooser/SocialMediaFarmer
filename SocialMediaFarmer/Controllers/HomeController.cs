using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SocialMediaFarmer.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SocialMediaFarmer.Controllers
{
    public class HomeController : Controller
    {
        private readonly Contexto _contexto;

        public HomeController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var usuario = _contexto.Usuarios.FirstOrDefault(u => u.Email == User.Identity.Name);
                if (usuario != null)
                {
                    usuario.EstaAutenticado = true;

                 
                    var perguntasRespostas = ObterPerguntasRespostas(); 
                    return View(perguntasRespostas);
                }
            }

            return RedirectToAction("Login");
        }

        private List<PerguntaRespostaViewModel> ObterPerguntasRespostas()
        {
            var perguntasRespostas = _contexto.Pergunta
                .Include(p => p.Usuario) 
                .Include(p => p.Respostas) 
                .ThenInclude(r => r.Usuario) 
                .Where(p => !p.Resolvida) 
                .Select(p => new PerguntaRespostaViewModel
                {
                    PerguntaId = p.Id,
                    Topico = p.Topico, 
                    Pergunta = p.Conteudo, 
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

            return perguntasRespostas;
        }

        [HttpPost]
        public IActionResult FazerPergunta([Bind("Topico, ConteudoPergunta")] Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                
                pergunta.UsuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 
                _contexto.Pergunta.Add(pergunta);
                _contexto.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(pergunta);
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _contexto.Usuarios.Add(usuario);
                _contexto.SaveChanges();

                TempData["CadastroSucesso"] = "Cadastro realizado com sucesso. Faça login para continuar.";

                return RedirectToAction("Login");
            }

            return View("Cadastro", usuario);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entrar([Bind("Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioBD = _contexto.Usuarios.FirstOrDefault(u => u.Email == usuario.Email && u.Senha == usuario.Senha);

                if (usuarioBD != null)
                {
                    var identidade = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identidade.AddClaim(new Claim(ClaimTypes.Name, usuarioBD.Email));

                    var claimsPrincipal = new ClaimsPrincipal(identidade);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("BoasVindas", new { id = usuarioBD.Id });
                }
                else
                {
                    ModelState.AddModelError("ErroLogin", "Email ou senha incorretos");
                }
            }

            return View("Login", usuario);
        }

        public IActionResult BoasVindas(Usuario usuario)
        {
            return View(usuario);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
