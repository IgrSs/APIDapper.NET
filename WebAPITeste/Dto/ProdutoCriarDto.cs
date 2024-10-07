namespace WebAPITeste.Dto
{
    public class ProdutoCriarDto
    {
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataValidade { get; set; }
        public bool Situacao { get; set; } // 1- Ativo ; 0- Inativo
    }
}
