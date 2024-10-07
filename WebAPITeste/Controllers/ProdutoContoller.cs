using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using WebAPITeste.Dto;
using WebAPITeste.Models;
using WebAPITeste.Service;

namespace WebAPITeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly IProdutoInterface _produtoInterface;
        public ProdutoController(IProdutoInterface produtoInterface)
        {
            _produtoInterface = produtoInterface;
        }

        [HttpGet]

        public async Task<IActionResult> BuscarProduto()
        {
            var produtos = await _produtoInterface.BuscarProdutos();

            if (produtos.Status == false)
            {
                return NotFound(produtos);
            }

            return Ok(produtos);
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> BuscarProdutoPorId(int Id)
        {
            var produto = await _produtoInterface.BuscarProdutoPorId(Id);

            if (produto.Status == false)
            {
                return NotFound(produto);
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> CriarProduto(ProdutoCriarDto produtoCriarDto)
        {
            var produto = await _produtoInterface.CriarProdutos(produtoCriarDto);

            if (produto.Status == false)
            {
                return BadRequest(produto);
            }

            return Ok(produto);
        }

        [HttpPut]
        public async Task<IActionResult> EditarProdutos(ProdutoEditarDto produtoEditarDto)
        {
            var produto = await _produtoInterface.EditarProdutos(produtoEditarDto);

            if (produto.Status == false)
            {
                return BadRequest(produto);
            }

            return Ok(produto);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverProdutos(int id)
        {
            var produto = await _produtoInterface.RemoverProduto(id);

            if (produto.Status == false)
            {
                return BadRequest(produto);
            }

            return Ok(produto);
        }
    }
}
