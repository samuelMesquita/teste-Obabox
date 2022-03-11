using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TESTE_TÉCNICO___DESENVOLVEDOR_TRAINE.Models;

namespace TESTE_TÉCNICO___DESENVOLVEDOR_TRAINE.Controllers
{
    [Route("")]
    [Route("Home")]
    public class ClienteController : Controller
    {
        private readonly MyContext myContext;

        public ClienteController(MyContext context)
        {
            myContext = context;
        }


        [HttpGet]
        public ActionResult<List<ClienteViewModel>> Index()
        {
            var result = myContext.Banco(null, "select-cliente");
            return View(result.TClienteViewMode);
        }

        [HttpGet("editar/{idCliente}")]
        public ActionResult<ClienteViewModel> Edit(int idCliente)
        {
            var result = myContext.Banco(new ResponseResult { Cliente = new ClienteViewModel { IdCliente = idCliente } }, "select-cliente-id");
            var listaEndereco = myContext.Banco(new ResponseResult { Endereco = new EnderecoViewModel { FkCliente = idCliente } }, "select-endereco");
            if (!String.IsNullOrEmpty(result.Response))
                return View();
            result.Cliente.ListaEndedreco = listaEndereco.TEnderecoViewMode;
            return View(result.Cliente);
        }

        [HttpPost("editar/{idCliente}")]
        public IActionResult Edit(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = myContext.Banco(new ResponseResult { Cliente = model }, "update-cliente");


            return RedirectToAction("Index", "Cliente");
        }

        [HttpGet("criar-cliente")]
        public ActionResult<ClienteViewModel> Create()
        {
            var viewModel = new ClienteViewModel
            {
                DataNascimento = System.DateTime.Now
            };

            return View(viewModel);
        }

        [HttpPost("criar-cliente")]
        public ActionResult<ClienteViewModel> Create(ClienteViewModel model)
        {
            if (!ModelState.IsValid) return View();

            ResponseResult responseResult = new ResponseResult()
            {
                Cliente = model
            };

            var modelResult = myContext.Banco(responseResult, "create-cliente");

            if (modelResult.Response != "success")
                return View(modelResult.Response);

            return RedirectToAction("index");
        }

        [HttpGet("delete/{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
                return View("Index");

            myContext.Banco(new ResponseResult { Cliente = new ClienteViewModel { IdCliente = id } }, "delete-cliente");


            return RedirectToAction("Index", "Cliente");
        }

        [HttpPost("cliente-filtro")]
        public ActionResult<List<ClienteViewModel>> FiltroCliente(string pesquisa)
        {
            var result = myContext.Banco(null, "select-cliente");
            List<ClienteViewModel> clientes = new List<ClienteViewModel>();
            
            if (pesquisa == null)
                return Json(result.TClienteViewMode);

            Regex rx = new Regex(pesquisa.ToLower());

            foreach (var cliente in result.TClienteViewMode)
            {
                MatchCollection matches = rx.Matches("^"+cliente.CPF);
                if (matches.Count > 0)
                {
                    clientes.Add(cliente);
                    continue;
                }

                matches = rx.Matches("^"+cliente.Nome.ToLower());
                if (matches.Count > 0)
                {
                    clientes.Add(cliente);
                    continue;
                }

                matches = rx.Matches(cliente.Sexo.ToLower());
                if (matches.Count > 0)
                {
                    clientes.Add(cliente);
                    continue;
                }  

                matches = rx.Matches(cliente.EstadoCivil.ToLower());
                if (matches.Count > 0)
                {
                    clientes.Add(cliente);
                    continue;
                }
            }

            return Json(clientes);
        }
    }
}
