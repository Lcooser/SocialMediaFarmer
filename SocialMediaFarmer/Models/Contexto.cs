using Microsoft.EntityFrameworkCore;

namespace SocialMediaFarmer.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<Resposta> Resposta { get; set; }
    }
}
