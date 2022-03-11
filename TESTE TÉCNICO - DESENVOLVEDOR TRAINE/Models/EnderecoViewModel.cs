namespace TESTE_TÉCNICO___DESENVOLVEDOR_TRAINE.Models
{
    public class EnderecoViewModel
    {
        public int IdEndereco { get; set; }
        public int FkCliente { get; set; }
        public int IdTipo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
    }
    public enum Tipo
    {
    Residencial = 1,
    Comercial = 2,
    Outros  = 3
    }
}
