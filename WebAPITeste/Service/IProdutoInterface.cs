using WebAPITeste.Dto;
using WebAPITeste.Models;

namespace WebAPITeste.Service
{
    public interface IProdutoInterface
    {
        Task<ResponseModel<List<ProdutoListarDto>>> BuscarProdutos();
        Task<ResponseModel<ProdutoListarDto>> BuscarProdutoPorId(int id);
        Task<ResponseModel<List<ProdutoListarDto>>> CriarProdutos(ProdutoCriarDto produtoCriarDto);
        Task<ResponseModel<List<ProdutoListarDto>>> EditarProdutos(ProdutoEditarDto produtoEditarDto);
        Task<ResponseModel<List<ProdutoListarDto>>> RemoverProduto(int id);
    }
}
