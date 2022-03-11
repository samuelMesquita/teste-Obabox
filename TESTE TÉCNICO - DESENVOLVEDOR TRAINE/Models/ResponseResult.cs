using System.Collections.Generic;

namespace TESTE_TÉCNICO___DESENVOLVEDOR_TRAINE.Models
{
    public class ResponseResult
    {
       public List<ClienteViewModel> TClienteViewMode { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public List<EnderecoViewModel> TEnderecoViewMode { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public string Response { get; set; }
    }
}
