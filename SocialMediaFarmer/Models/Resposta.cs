namespace SocialMediaFarmer.Models
{
    public class Resposta
    {
        public int Id { get; set; }
        public string? Conteudo { get; set; }
        public int PerguntaId { get; set; }
        public virtual Pergunta? Pergunta { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
        
    }
}
