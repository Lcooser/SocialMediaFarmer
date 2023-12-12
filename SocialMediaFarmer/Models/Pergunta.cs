using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaFarmer.Models
{
    public class Pergunta
    {
        public int Id { get; set; }
        public string? Topico { get; set; } 
        public string? ConteudoPergunta { get; set; }
        public string? Titulo { get; set; }
        public string? Conteudo { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime DataPublicacao { get; set; }
        public bool Resolvida { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }

        public List<Resposta>? Respostas { get; set; }
    }
}
