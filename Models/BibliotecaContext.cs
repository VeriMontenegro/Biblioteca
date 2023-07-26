using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biblioteca.Models
{
    public class BibliotecaContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {                   
            optionsBuilder.UseMySql("Server=localhost; User ID=root; Password=; Database=Biblioteca");
        }

        public DbSet<Livro> Livros {get; set;}

        public DbSet<Emprestimo> Emprestimos {get; set;}
        
        public DbSet<Usuario> usuarios {get; set;}
    }
}
