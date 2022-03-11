using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TESTE_TÉCNICO___DESENVOLVEDOR_TRAINE.Models
{
    public class MyContext
    {
        public string ConnectionString { get; } = "Data Source=DESKTOP-A4VVOPE;Initial Catalog=ObaBox;Persist Security Info=False;User ID=sa;PWD=140420;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False";

        
        public ResponseResult Banco(ResponseResult responseResult, string tipo)
        { 
            ResponseResult result = new ResponseResult();
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    con.Open();

                    if (tipo == "create-cliente")
                        return Create(con, responseResult);

                    if (tipo == "select-cliente")
                        return SelectCliente(con);

                    if (tipo == "select-cliente-id")
                        return SelectClienteId(con, responseResult);

                    if (tipo == "delete-cliente")
                        deleteCliente(con, responseResult);

                    if (tipo == "update-cliente")
                        updateCliente(con, responseResult);

                    if(tipo == "create-endereco")
                        CreateEndereco(con, responseResult);

                    if (tipo == "select-endereco")
                        return SelectEndereco(con, responseResult);

                    if (tipo == "editar-endereco")
                        return UpdateEndereco(con, responseResult);

                    if (tipo == "select-endereco-id")
                        return SelectEnderecoId(con, responseResult);

                    if(tipo == "delete-endereco")
                        return DeleteEndereco(con, responseResult);

                    return result;
                    con.Close();
                }
                catch(Exception ex)
                {
                    return new ResponseResult { Response = ex.Message };
                }
            }
        }
      
        
        private ResponseResult SelectCliente(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("GetCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            List<ClienteViewModel> cliente = new List<ClienteViewModel>();
            while (reader.Read())
            {
                cliente.Add(new ClienteViewModel { 
                    IdCliente = int.Parse(reader["IdCliente"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Sexo = reader["Sexo"].ToString(),
                    DataNascimento = DateTime.Parse(reader["DataNascimento"].ToString()),
                    EstadoCivil = reader["EstadoCivil"].ToString(),
                    CPF = reader["CPF"].ToString(),
                    RG = reader["RG"].ToString()
                });
            }
            return new ResponseResult{ TClienteViewMode = cliente };
        }
        private ResponseResult SelectClienteId(SqlConnection con, ResponseResult response)
        {
            SqlCommand cmd = new SqlCommand("GetClienteId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdCliente", response.Cliente.IdCliente);
            SqlDataReader reader = cmd.ExecuteReader();
            ClienteViewModel cliente = new ClienteViewModel();
            while (reader.Read())
            {
                cliente = new ClienteViewModel()
                {
                    IdCliente = int.Parse(reader["IdCliente"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Sexo = reader["Sexo"].ToString(),
                    DataNascimento = DateTime.Parse(reader["DataNascimento"].ToString()),
                    EstadoCivil = reader["EstadoCivil"].ToString(),
                    CPF = reader["CPF"].ToString(),
                    RG = reader["RG"].ToString()
                };
            }

            return new ResponseResult { Cliente = cliente };
        } 

        private ResponseResult Create(SqlConnection con, ResponseResult cliente)
        {
            SqlCommand cmd = new SqlCommand("SetCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pNome", cliente.Cliente.Nome);
            cmd.Parameters.AddWithValue("@pSexo", Char.Parse(cliente.Cliente.Sexo));
            cmd.Parameters.AddWithValue("@pDataNascimento", cliente.Cliente.DataNascimento);
            cmd.Parameters.AddWithValue("@pEstadoCivil", Char.Parse(cliente.Cliente.EstadoCivil));
            cmd.Parameters.AddWithValue("@pCPF", cliente.Cliente.CPF);
            cmd.Parameters.AddWithValue("@pRG", cliente.Cliente.RG);
            cmd.Parameters.AddWithValue("@Retorno", SqlDbType.Int).Direction = ParameterDirection.Output;
            
            string responseBd = ValidarResponseBd(cmd.ExecuteNonQuery());
            return new ResponseResult { Response = responseBd };
        }

        private ResponseResult updateCliente(SqlConnection con, ResponseResult response)
        {
            SqlCommand cmd = new SqlCommand("UpdateCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdCliente", response.Cliente.IdCliente);
            cmd.Parameters.AddWithValue("@pNome", response.Cliente.Nome);
            cmd.Parameters.AddWithValue("@pSexo", Char.Parse(response.Cliente.Sexo));
            cmd.Parameters.AddWithValue("@pDataNascimento", response.Cliente.DataNascimento);
            cmd.Parameters.AddWithValue("@pEstadoCivil", Char.Parse(response.Cliente.EstadoCivil));
            cmd.Parameters.AddWithValue("@pCPF", response.Cliente.CPF);
            cmd.Parameters.AddWithValue("@pRG", response.Cliente.RG);
            cmd.Parameters.AddWithValue("@Retorno", SqlDbType.Int).Direction = ParameterDirection.Output;

            string responseBd = ValidarResponseBd(cmd.ExecuteNonQuery());
            return new ResponseResult { Response = responseBd };
        }

        private void deleteCliente(SqlConnection con, ResponseResult response)
        {
            SqlCommand cmd = new SqlCommand("DeleteCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdCliente", response.Cliente.IdCliente);
            cmd.ExecuteNonQuery();
        }

        private ResponseResult CreateEndereco(SqlConnection con, ResponseResult response)
        {
            SqlCommand cmd = new SqlCommand("SetEndereco", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pFkCliente", response.Endereco.FkCliente);
            cmd.Parameters.AddWithValue("@pIdentificadorTipo", response.Endereco.IdTipo);
            cmd.Parameters.AddWithValue("@pLogradouro", response.Endereco.Logradouro);
            cmd.Parameters.AddWithValue("@pNumero", int.Parse(response.Endereco.Numero));
            cmd.Parameters.AddWithValue("@pComplemento", String.IsNullOrEmpty(response.Endereco.Complemento) ? "": response.Endereco.Complemento);
            cmd.Parameters.AddWithValue("@pCidade", response.Endereco.Cidade);
            cmd.Parameters.AddWithValue("@pUF", response.Endereco.UF);
            cmd.Parameters.AddWithValue("@pCEP", response.Endereco.CEP);
            cmd.Parameters.AddWithValue("@Retorno", SqlDbType.Int).Direction = ParameterDirection.Output;

            string responseBd = ValidarResponseBd(cmd.ExecuteNonQuery());
            return new ResponseResult { Response = responseBd };
        }

        private ResponseResult SelectEndereco(SqlConnection con, ResponseResult response)
        {
            SqlCommand cmd = new SqlCommand("GetEndereco", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pFkCliente", response.Endereco.FkCliente);
            SqlDataReader reader = cmd.ExecuteReader();
            List<EnderecoViewModel> cliente = new List<EnderecoViewModel>();
            while (reader.Read())
            {
                cliente.Add(new EnderecoViewModel
                {
                    IdEndereco = int.Parse(reader["IdEndereco"].ToString()),
                    FkCliente = int.Parse(reader["FkCliente"].ToString()),
                    IdTipo = int.Parse(reader["IdentificadorTipo"].ToString()),
                    Logradouro = reader["Logradouro"].ToString(),
                    Numero = reader["Numero"].ToString(),
                    Complemento = reader["Complemento"].ToString(),
                    Cidade = reader["Cidade"].ToString(),
                    UF = reader["UF"].ToString(),
                    CEP = reader["CEP"].ToString()
                });
            }
            return new ResponseResult { TEnderecoViewMode = cliente };
        }

        private ResponseResult SelectEnderecoId(SqlConnection con, ResponseResult response)
        {
            SqlCommand cmd = new SqlCommand("GetEnderecoId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdEndereco", response.Endereco.IdEndereco);
            SqlDataReader reader = cmd.ExecuteReader();
            List<EnderecoViewModel> cliente = new List<EnderecoViewModel>();
            while (reader.Read())
            {
                cliente.Add(new EnderecoViewModel
                {
                    IdEndereco = int.Parse(reader["IdEndereco"].ToString()),
                    FkCliente = int.Parse(reader["FkCliente"].ToString()),
                    IdTipo = int.Parse(reader["IdentificadorTipo"].ToString()),
                    Logradouro = reader["Logradouro"].ToString(),
                    Numero = reader["Numero"].ToString(),
                    Complemento = reader["Complemento"].ToString(),
                    Cidade = reader["Cidade"].ToString(),
                    UF = reader["UF"].ToString(),
                    CEP = reader["CEP"].ToString()
                });
            }
            return new ResponseResult { TEnderecoViewMode = cliente };
        }

        private ResponseResult UpdateEndereco(SqlConnection con, ResponseResult response)
        {

            SqlCommand cmd = new SqlCommand("UpdateEndereco", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdEndereco", response.Endereco.IdEndereco);
            cmd.Parameters.AddWithValue("@pIdentificadorTipo", response.Endereco.IdTipo);
            cmd.Parameters.AddWithValue("@pLogradouro", response.Endereco.Logradouro);
            cmd.Parameters.AddWithValue("@pNumero", response.Endereco.Numero);
            cmd.Parameters.AddWithValue("@pComplemento", String.IsNullOrEmpty(response.Endereco.Complemento)? "" : response.Endereco.Complemento);
            cmd.Parameters.AddWithValue("@pCidade", response.Endereco.Cidade);
            cmd.Parameters.AddWithValue("@pUF", response.Endereco.UF);
            cmd.Parameters.AddWithValue("@pCEP", response.Endereco.CEP);
            cmd.Parameters.AddWithValue("@Retorno", SqlDbType.Int).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            return new ResponseResult { };
        }

        private ResponseResult DeleteEndereco(SqlConnection con, ResponseResult response)
        {
            SqlCommand cmd = new SqlCommand("DeleteEndereco", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdEndereco", response.Endereco.IdEndereco);
            cmd.ExecuteNonQuery();

            return new ResponseResult();
        }

        private string ValidarResponseBd(int rbd)
        {
            string responseBd = "success";

            if (rbd == -1)
                return responseBd = "CPF ou RG invalido, tente novamente";

            return responseBd;
        }
    }
}
