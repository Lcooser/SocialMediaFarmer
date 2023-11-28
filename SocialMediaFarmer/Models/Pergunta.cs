namespace SocialMediaFarmer.Models
{
    public class Pergunta
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Conteudo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public bool Resolvida { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }

        public List<Resposta>? Respostas { get; set; }


    }
}
