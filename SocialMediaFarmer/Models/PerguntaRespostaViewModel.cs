using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaFarmer.Models
{
    public class PerguntaRespostaViewModel
    {
        public int PerguntaId { get; set; }
        public string? Topico { get; set; }
        public string? Pergunta { get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime DataPublicacao { get; set; }
        public bool Resolvida { get; set; }
        public string? NomeUsuario { get; set; }

        public List<RespostaViewModel>? Respostas { get; set; }
    }

    public class RespostaViewModel
    {
        public int RespostaId { get; set; }
        public string? Conteudo { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime DataPublicacao { get; set; }
        public string? NomeUsuario { get; set; }
    }
}
