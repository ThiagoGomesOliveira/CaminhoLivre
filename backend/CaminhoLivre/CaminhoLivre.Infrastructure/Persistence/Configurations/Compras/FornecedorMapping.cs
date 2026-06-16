using CaminhoLivre.Modulo.Compras.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaminhoLivre.Infrastructure.Persistence.Configurations.Compras;

public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        // 1. Nome da tabela no banco de dados (Postgres padrão snake_case)
        builder.ToTable("fornecedores");

        // 2. Chave Primária
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id)
            .UseIdentityByDefaultColumn()
            .HasColumnName("id");

        // 3. Mapeamento das Propriedades
        builder.Property(f => f.RazaoSocial)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("varchar(150)")
            .HasColumnName("razao_social");

        builder.Property(f => f.NomeFantasia)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .HasColumnName("nome_fantasia")
            .IsRequired(false); // Opcional, como havíamos planejado

        // Usou CnpjCpf unificado, excelente para o caso de fornecedores autônomos (Pessoa Física)
        builder.Property(f => f.CnpjCpf)
            .IsRequired()
            .HasMaxLength(14)
            .HasColumnType("varchar(14)")
            .HasColumnName("cnpj_cpf");

        // Criando um índice único para o CNPJ/CPF para evitar que o mesmo fornecedor seja cadastrado duas vezes
        builder.HasIndex(f => f.CnpjCpf)
            .IsUnique()
            .HasDatabaseName("idx_fornecedores_cnpj_cpf");

        builder.Property(f => f.Email)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .HasColumnName("email")
            .IsRequired(false);

        builder.Property(f => f.Telefone)
            .HasMaxLength(20)
            .HasColumnType("varchar(20)")
            .HasColumnName("telefone")
            .IsRequired();

        builder.Property(f => f.Numero)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("varchar(20)")
            .HasColumnName("numero");

        builder.Property(f => f.Complemento)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .HasColumnName("complemento")
            .IsRequired(false);

        builder.Property(f => f.Ativo)
            .IsRequired()
            .HasColumnName("ativo");

        builder.Property(f => f.DataCadastro)
            .IsRequired()
            .HasColumnName("data_cadastro");

        // 4. Relacionamento: 1 Logradouro tem muitos Fornecedores (1:N)
        builder.Property(f => f.LogradouroId)
            .HasColumnName("logradouro_id");

        builder.HasOne(f => f.Logradouro)
            .WithMany() // Como o seu Logradouro não tem uma coleção de Fornecedores dentro dele, fica vazio
            .HasForeignKey(f => f.LogradouroId)
            .OnDelete(DeleteBehavior.Restrict) // Segurança: impede deletar um logradouro usado por um fornecedor
            .HasConstraintName("fk_fornecedores_logradouros_logradouro_id");

        // 5. Dizer ao EF para usar o construtor privado ou acesso por propriedades se necessário
        // O EF Core já consegue usar o construtor privado sem parâmetros por padrão, mas mapear os campos garante estabilidade
    }
}