using AutoMapper;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebAPITeste.Dto;
using WebAPITeste.Models;

namespace WebAPITeste.Service
{
    public class ProdutoService : IProdutoInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProdutoService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;

        }

        public async Task<ResponseModel<ProdutoListarDto>> BuscarProdutoPorId(int id)
        {
            ResponseModel<ProdutoListarDto> response = new ResponseModel<ProdutoListarDto>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var produtoBanco = await connection.QueryFirstOrDefaultAsync<Produto>
                    ("SELECT * FROM Produto where id = @Id", new { Id = id });

                if (produtoBanco == null)
                {

                    response.Status = false;
                    return response;

                }
                var produtoMapeado = _mapper.Map<ProdutoListarDto>(produtoBanco);
                response.Dados = produtoMapeado;
            }

            return response;
        }

        public async Task<ResponseModel<List<ProdutoListarDto>>> BuscarProdutos()
        {
            ResponseModel<List<ProdutoListarDto>> response =
                new ResponseModel<List<ProdutoListarDto>>();

            using (var connection = new SqlConnection
                (_configuration.GetConnectionString("DefaultConnection")))
            {
                var produtoBanco = await connection.QueryAsync<Produto>
                    ("SELECT * FROM Produto");

                if (produtoBanco.Count() == 0)
                {

                    response.Status = false;
                    return response;

                }

                var produtoMapeado = _mapper.Map<List<ProdutoListarDto>>
                    (produtoBanco);

                response.Dados = produtoMapeado;

            }
            return response;
        }

        public async Task<ResponseModel<List<ProdutoListarDto>>> CriarProdutos(ProdutoCriarDto produtoCriarDto)
        {
            ResponseModel<List<ProdutoListarDto>> response = new ResponseModel<List<ProdutoListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var produtoBanco = await connection.ExecuteAsync("INSERT INTO Produto(Descricao, Preco, DataValidade, Situacao) " +
                    "values(@Descricao, @Preco, @DataValidade, @Situacao)", produtoCriarDto);

                if (produtoBanco == 0)
                {
                    response.Status = false;
                    return response;
                }

                var produto = await ListarProduto(connection);

                var produtoMapeado = _mapper.Map <List<ProdutoListarDto>>(produto);

                response.Dados = produtoMapeado;
                response.Mensagem = "Produto listado com sucesso";

            }

            return response;
        }

        private static async Task<IEnumerable<Produto>> ListarProduto(SqlConnection connection)
        {
          return await connection.QueryAsync<Produto>("SELECT * FROM Produto");
        }

        public async Task<ResponseModel<List<ProdutoListarDto>>> EditarProdutos(ProdutoEditarDto produtoEditarDto)
        {
            ResponseModel<List<ProdutoListarDto>> response = new ResponseModel<List<ProdutoListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                var produtoBanco = await connection.ExecuteAsync("UPDATE Produto set Descricao = @Descricao," +
                    "Preco = @Preco, DataValidade = @DataValidade, Situacao = @Situacao WHERE Id = @Id", produtoEditarDto);

                if (produtoBanco == 0)
                {

                    response.Mensagem = "Ocorreu um erro ao realizar a edicao!!";
                    response.Status = false;
                    return response;

                }

                var produto = await ListarProduto(connection);

                var produtoMapeado = _mapper.Map<List<ProdutoListarDto>>(produto);

                response.Dados = produtoMapeado;
                response.Mensagem = "Produto listado com sucesso!";

            }

            return response;
        }

        public async Task<ResponseModel<List<ProdutoListarDto>>> RemoverProduto(int id)
        {
            ResponseModel<List<ProdutoListarDto>> response = new ResponseModel<List<ProdutoListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var produtoBanco = await connection.ExecuteAsync("DELETE FROM Produto WHERE Id = @Id", new {Id = id});

                if (produtoBanco == 0)
                {

                    response.Mensagem = "Ocorreu um erro ao deletar!!";
                    response.Status = false;
                    return response;

                }

                var produto = await ListarProduto(connection);

                var produtoMapeado = _mapper.Map<List<ProdutoListarDto>>(produto);

                response.Dados = produtoMapeado;
                response.Mensagem = "Produto listado com sucesso!";
            }

            return response;
        }
    }
    }

