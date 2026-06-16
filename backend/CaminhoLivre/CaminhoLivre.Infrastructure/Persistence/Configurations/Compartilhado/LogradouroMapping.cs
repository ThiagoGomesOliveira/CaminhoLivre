using CaminhoLivre.Compartilhado.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaminhoLivre.Infrastructure.Persistence.Configurations.Compartilhado;

public class LogradouroMapping : IEntityTypeConfiguration<Logradouro>
{
    public void Configure(EntityTypeBuilder<Logradouro> builder)
    {
        // Nome da Tabela
        builder.ToTable("Logradouros");

        // Chave Primária
        builder.HasKey(l => l.Id);

        // Configuração do CEP
        builder.Property(l => l.Cep)
            .IsRequired()
            .HasMaxLength(8)
            .HasColumnType("varchar(8)");

        // Criando o Índice Único para o CEP (Garante a performance e impede duplicados no cache)
        builder.HasIndex(l => l.Cep)
            .IsUnique();

        // Configuração da Rua / Logradouro
        builder.Property(l => l.Rua)
            .HasMaxLength(150)
            .HasColumnType("varchar(150)")
            .IsRequired(false); // Permite nulo/vazio para cidades com CEP único geral

        // Configuração do Bairro
        builder.Property(l => l.Bairro)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .IsRequired(false);

        // Relacionamento: 1 Município tem muitos Logradouros (1:N)
        builder.HasOne(l => l.Municipio)
            .WithMany() // Se você não tiver uma ICollection<Logradouro> na classe Municipio, deixe vazio assim
            .HasForeignKey(l => l.MunicipioId)
            .OnDelete(DeleteBehavior.Restrict); // Impede deletar um município se ele tiver logradouros vinculados
    }
}