using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TESTE_TÉCNICO___DESENVOLVEDOR_TRAINE.Models;

namespace TESTE_TÉCNICO___DESENVOLVEDOR_TRAINE.Controllers
{
    [Route("endereco")]
    public class EnderecoController : Controller
    {
        readonly MyContext myContext;
        public EnderecoController(MyContext context)
        {
            myContext = context;
        }

        [HttpGet("criar-endereco/{id}")]
        public ActionResult<EnderecoViewModel> Create(int id)
        {
            if(id == 0)
                return RedirectToAction("", "");

            return View(new EnderecoViewModel { FkCliente = id});
        }

        [HttpPost("criar-endereco/{id}")]
        public ActionResult Create(EnderecoViewModel model, int id)
        {
            model.FkCliente = id;

            myContext.Banco(new ResponseResult { Endereco = model }, "create-endereco");

            return RedirectToAction("editar", "", new { id = id });
        }

        [HttpGet("editar-endereco/{id}")]
        public ActionResult<EnderecoViewModel> Edit(int id)
        {
            var listaEndereco = myContext.Banco(new ResponseResult { Endereco = new EnderecoViewModel { IdEndereco = id } }, "select-endereco-id");
            return View(listaEndereco.Endereco = listaEndereco.TEnderecoViewMode[0]);
        }

        [HttpPost("editar-endereco/{id}")]
        public ActionResult Edit(EnderecoViewModel model, int id)
        {
            model.IdEndereco = id;
            myContext.Banco(new ResponseResult { Endereco = model }, "editar-endereco");
            var listaEndereco = myContext.Banco(new ResponseResult { Endereco = new EnderecoViewModel { IdEndereco = id } }, "select-endereco-id");
            listaEndereco.Endereco = listaEndereco.TEnderecoViewMode[0];
            return RedirectToAction("editar", "", new { id = listaEndereco.Endereco.FkCliente });
        }

        [HttpGet("delete{id}")]
        public ActionResult Delete(int id, int idCliente)
        {
            myContext.Banco(new ResponseResult { Endereco = new EnderecoViewModel { IdEndereco = id } }, "delete-endereco");
            return RedirectToAction("editar", "", new { id = idCliente });
        }
    }  
}
