
using Microsoft.EntityFrameworkCore;
using SorteOnlineDesafio.Domain.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Pedido> Pedido { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuario");
            entity.HasKey(u => u.UsuarioId);
            entity.Property(u => u.UsuarioId).HasColumnName("usuario_id").IsRequired().HasColumnType("INT").UseIdentityColumn();
            entity.Property(u => u.Nome).HasColumnName("nome").IsRequired().HasMaxLength(100).HasColumnType("VARCHAR");
            entity.Property(u => u.Email).HasColumnName("email").IsRequired().HasMaxLength(100).HasColumnType("VARCHAR");
            entity.Property(u => u.Senha).HasColumnName("senha").IsRequired().HasColumnType("VARCHAR(MAX)");
            entity.Property(u => u.DataCriacao).HasColumnName("data_criacao").IsRequired().HasColumnType("DATETIME");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("cliente"); 
            entity.HasKey(c => c.ClienteId);
            entity.Property(c => c.ClienteId).HasColumnName("cliente_id").IsRequired().HasColumnType("INT").UseIdentityColumn();
            entity.Property(c => c.Nome).HasColumnName("nome").IsRequired().HasMaxLength(100).HasColumnType("VARCHAR");
            entity.Property(c => c.Email).HasColumnName("email").IsRequired().HasMaxLength(100).HasColumnType("VARCHAR");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.ToTable("pedido"); 
            entity.HasKey(p => p.PedidoId);
            entity.Property(p => p.PedidoId).HasColumnName("pedido_id").IsRequired().HasColumnType("INT").UseIdentityColumn();
            entity.Property(p => p.ClienteId).HasColumnName("cliente_id").IsRequired().HasColumnType("INT");
            entity.Property(p => p.DataPedido).HasColumnName("data_pedido").IsRequired().HasColumnType("DATETIME");
            entity.Property(p => p.ValorTotal).HasColumnName("valor_total").IsRequired().HasColumnType("DECIMAL(18, 2)");

            entity.HasOne(p => p.Cliente)
                  .WithMany(c => c.Pedidos)
                  .HasForeignKey(p => p.ClienteId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}

