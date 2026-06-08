using CaminhoLivre.Compartilhado.Exceptions;
using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Services;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;
using Moq;

namespace CaminhoLivre.Modulo.Catalago.Tests.Services
{
    [TestClass]
    public class ProdutoServiceTest
    {
        private Mock<IProdutoRepository> _produtoRepositoryMock;
        private ProdutoService _produtoServiceMock;

        [TestInitialize]
        public void Init()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _produtoServiceMock = new ProdutoService(_produtoRepositoryMock.Object);
        }

        [TestMethod]
        public async Task CriarAsync_QuandoDadosForemValidosESalvarComSucesso_DeveRetornarOIdDoProduto()
        {
            // --- ARRANGE
            var produtoDto = new ProdutoDto
            {
                Nome = "Camiseta Polo Algodão Premium",
                Descricao = "A Camiseta Básica Premium é a escolha perfeita para quem busca unir conforto e durabilidade.",
                Sku = "VES-CAM-PM", 
                PrecoCusto = 49.90m,
                PrecoVenda = 89.90m, 
                CategoriaId = 1,
                Ativo = true
            };

            _produtoRepositoryMock
                .Setup(repo => repo.SalvarAlteracoesAsync())
                .ReturnsAsync(true);

            // --- ACT 
            var resultadoId = await _produtoServiceMock.CriarAsync(produtoDto);

            // --- ASSERT
            Assert.IsTrue(resultadoId >= 0);

            _produtoRepositoryMock.Verify(
                repo => repo.AdicionarAsync(It.IsAny<Produto>()),
                Times.Once
            );

            // 3. Verifica se o SalvarAlteracoesAsync foi chamado
            _produtoRepositoryMock.Verify(
                repo => repo.SalvarAlteracoesAsync(),
                Times.Once
            );
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))] // O MSTest espera que essa exceção aconteça
        public async Task CriarAsync_QuandoBancoFalharAoSalvar_DeveLanciarBusinessRuleException()
        {
            // --- ARRANGE ---
            var produtoDto = new ProdutoDto
            {
                Nome = "Camiseta Polo Algodão Premium",
                Descricao = "A Camiseta Básica Premium é a escolha perfeita para quem busca unir conforto e durabilidade.",
                Sku = "VES-CAM-PM",
                PrecoCusto = 49.90m,
                PrecoVenda = 89.90m,
                CategoriaId = 1,
                Ativo = true
            };

            // Configura o mock para simular uma falha no banco (retornando false)
            _produtoRepositoryMock
                .Setup(repo => repo.SalvarAlteracoesAsync())
                .ReturnsAsync(false);

            // --- ACT ---
            await _produtoServiceMock.CriarAsync(produtoDto);

            // --- ASSERT ---
            // O assert é feito automaticamente pelo atributo [ExpectedException] no topo do método.
        }

        [TestMethod]
        public async Task DesativarAsync_QuandoProdutoExistir_DeveChamarDesativarEAtualizarNoBanco()
        {
            // --- ARRANGE (Preparação) ---
            long produtoId = 42;

            var produtoMock = Produto.Criar("Produto Teste", "VES-CAM-PM", "Descrição", 10m, 20m, 1);

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(produtoId))
                .ReturnsAsync(produtoMock);

            // --- ACT
            await _produtoServiceMock.DesativarAsync(produtoId);

            // --- ASSERT (Verificação) ---
            _produtoRepositoryMock.Verify(
                repo => repo.AtualizarAsync(produtoMock),
                Times.Once
            );

            _produtoRepositoryMock.Verify(
                repo => repo.SalvarAlteracoesAsync(),
                Times.Once
            );
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task DesativarAsync_QuandoProdutoNaoExistir_DeveLanciarNotFoundException()
        {
            // --- ARRANGE ---
            long idInexistente = 999;

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(idInexistente))
                .ReturnsAsync((Produto)null);

            // --- ACT ---
            await _produtoServiceMock.DesativarAsync(idInexistente);

            // --- ASSERT ---
            // Validado automaticamente pelo [ExpectedException] no topo
        }

        [TestMethod]
        public async Task AtivarAsync_QuandoProdutoExistir_DeveChamarAtivarEAtualizarNoBanco()
        {
            // --- ARRANGE
            long produtoId = 100;

            var produtoMock = Produto.Criar("Produto Inativo", "VES-CAM-PM", "Descrição", 10m, 20m, 1);

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(produtoId))
                .ReturnsAsync(produtoMock);

            // --- ACT
            await _produtoServiceMock.AtivarAsync(produtoId);

            // --- ASSERT 
            _produtoRepositoryMock.Verify(
                repo => repo.AtualizarAsync(produtoMock),
                Times.Once
            );

            _produtoRepositoryMock.Verify(
                repo => repo.SalvarAlteracoesAsync(),
                Times.Once
            );
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task AtivarAsync_QuandoProdutoNaoExistir_DeveLanciarNotFoundException()
        {
            // --- ARRANGE 
            long idInexistente = 999;

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(idInexistente))
                .ReturnsAsync((Produto)null);

            // --- ACT ---
            await _produtoServiceMock.AtivarAsync(idInexistente);

            // --- ASSERT ---
            // Validado automaticamente pelo [ExpectedException] do MSTest
        }

        [TestMethod]
        public async Task AtualizarAsync_QuandoProdutoExistir_DeveAtualizarTodasAsPropriedadesECommitar()
        {
            // --- ARRANGE
            long produtoId = 10;

            var produtoOriginal = Produto.Criar("Nome Antigo", "VES-CAM-PM", "Desc Antiga", 10m, 20m, 1);

            var dtoNovo = new ProdutoDto
            {
                Nome = "Nome Atualizado",
                Sku = "VES-CAM-PM",
                Descricao = "Nova Descrição",
                PrecoCusto = 15.00m,
                PrecoVenda = 30.00m,
                CategoriaId = 2,
                Ativo = true
            };

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(produtoId))
                .ReturnsAsync(produtoOriginal);

            // --- ACT 
            await _produtoServiceMock.AtualizarAsync(produtoId, dtoNovo);

            // --- ASSERT
            Assert.AreEqual(dtoNovo.Nome, produtoOriginal.Nome);
            Assert.AreEqual(dtoNovo.Sku, produtoOriginal.Sku);
            Assert.AreEqual(dtoNovo.Descricao, produtoOriginal.Descricao);
            Assert.AreEqual(dtoNovo.PrecoCusto, produtoOriginal.PrecoCusto);
            Assert.AreEqual(dtoNovo.PrecoVenda, produtoOriginal.PrecoVenda);
            Assert.AreEqual(dtoNovo.CategoriaId, produtoOriginal.CategoriaId);
            Assert.AreEqual(dtoNovo.Ativo, produtoOriginal.Ativo);

            _produtoRepositoryMock.Verify(
                repo => repo.AtualizarAsync(produtoOriginal),
                Times.Once
            );

            _produtoRepositoryMock.Verify(
                repo => repo.SalvarAlteracoesAsync(),
                Times.Once
            );
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task UpdateAsync_QuandoProdutoNaoExistir_DeveLanciarNotFoundException()
        {
            // --- ARRANGE
            long idInexistente = 999;
            var dto = new ProdutoDto
            {
                Nome = "Teste",
                Sku = "VES-TST-01",
                Descricao = "Desc",
                PrecoCusto = 10m,
                PrecoVenda = 20m,
                CategoriaId = 1
            };

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(idInexistente))
                .ReturnsAsync((Produto)null);

            // --- ACT ---
            await _produtoServiceMock.AtualizarAsync(idInexistente, dto);

            // --- ASSERT ---
            // Validado automaticamente pelo [ExpectedException] do MSTest
        }

        [TestMethod]
        public async Task ObterTodasAsync_QuandoExistiremProdutos_DeveRetornarResultadoPaginadoEMapeado()
        {
            // --- ARRANGE (Preparação) ---
            int pagina = 1;
            int quantidadePorPagina = 10;
            int totalProdutosNoBanco = 15;

           
            var categoriaMock = Categoria.Criar("Logística", "Categoria de paletes");

            // Injeta o ID 5 na categoria via Reflection (simulando a chave gerada pelo banco)
            typeof(Categoria).GetProperty("Id")?.SetValue(categoriaMock, 5);

            
            var produtoMock = Produto.Criar(
                "Palete PBR",
                "VES-CAM-PM",
                "Palete de madeira",
                45m,
                90m,
                1
            );

            typeof(Produto).GetProperty("Id")?.SetValue(produtoMock, 1);

            // Preenche o objeto de navegação 'Categoria' do produto que o EF traria preenchido pelo .Include()
            typeof(Produto).GetProperty("Categoria")?.SetValue(produtoMock, categoriaMock);

            // 3. Cria a lista e monta a tupla de retorno que o repositório espera
            var listaProdutosMock = new List<Produto> { produtoMock };
            var retornoRepositorioMock = (listaProdutosMock, totalProdutosNoBanco);

            _produtoRepositoryMock
                .Setup(repo => repo.ObterTodasPaginadasAsync(pagina, quantidadePorPagina))
                .ReturnsAsync(retornoRepositorioMock);

            // --- ACT 
            var resultado = await _produtoServiceMock.ObterTodasAsync(pagina, quantidadePorPagina);

            // --- ASSERT (Verificação) ---
           
            Assert.IsNotNull(resultado);
            Assert.AreEqual(totalProdutosNoBanco, resultado.TotalItens);
            Assert.AreEqual(pagina, resultado.PaginaAtual);
            Assert.AreEqual(1, resultado.Itens.Count());

            // 2. Validações do Produto Principal mapeado
            var produtoMapeado = resultado.Itens.First();
            Assert.AreEqual(1, produtoMapeado.Id);
            Assert.AreEqual("Palete PBR", produtoMapeado.Nome);
            Assert.AreEqual("VES-CAM-PM", produtoMapeado.Sku);
            Assert.IsTrue(produtoMapeado.Ativo);

            // 3. Validações do Objeto Aninhado (CategoriaDto)
            Assert.IsNotNull(produtoMapeado.Categoria);
            Assert.AreEqual("Logística", produtoMapeado.Categoria.Nome);
            Assert.AreEqual("Categoria de paletes", produtoMapeado.Categoria.Descricao);

            Assert.AreEqual(1, produtoMapeado.Categoria.Id);

            _produtoRepositoryMock.Verify(
                repo => repo.ObterTodasPaginadasAsync(pagina, quantidadePorPagina),
                Times.Once
            );
        }
    }
}
