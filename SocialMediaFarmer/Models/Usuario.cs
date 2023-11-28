namespace SocialMediaFarmer.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public bool Ativo { get; set; }

        public List<Pergunta>? Perguntas { get; set; }
        public List<Resposta>? Respostas { get; set; }

    }
}
