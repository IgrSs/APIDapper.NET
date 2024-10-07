namespace WebAPITeste.Models
{
    public class Produto
    {

        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataValidade { get; set; }
        public bool Situacao { get; set; } // 1- Ativo ; 0- Inativo

    }
}

// Dado, mensagem, status
// Modelo de resposta para no front ser mais facil a tratativa