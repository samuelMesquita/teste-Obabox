using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TESTE_TÉCNICO___DESENVOLVEDOR_TRAINE.Models
{
    public class ClienteViewModel
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string EstadoCivil { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public List<EnderecoViewModel> ListaEndedreco { get; set; }
        
    }
   
}
