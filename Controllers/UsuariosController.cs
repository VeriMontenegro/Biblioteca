using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar());
        }

        public IActionResult editarUsuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);

            return View(u);
        }

        [HttpPost]
        public IActionResult editarUsuario(Usuario userEditado)
        {
            UsuarioService us = new UsuarioService();
            us.editarUsuario(userEditado);

            return RedirectToAction("ListaDeUsuarios");
        }

        public IActionResult RegistrarUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            UsuarioService us = new UsuarioService();
            us.incluirUsuario(novoUser);

            return RedirectToAction("cadastroRealizado");
        }

        public IActionResult excluirUsuario(int id)
        {
            return View(new UsuarioService().Listar(id));
        }

        [HttpPost]
        public IActionResult excluirUsuario(string decisao,int id)
        {
            if(decisao=="Excluir")
            {
                ViewData["Mensagem"] = "Exclusão do usuário" +new UsuarioService().Listar(id).Nome+"Realizada com sucesso";
                new UsuarioService().excluirUsuario(id);
                return View("ListaDeUsuarios",new UsuarioService().Listar());
            }
            else
            {
                ViewData["Mensagem"] = "Exclusão cancelada";
                return View("ListaDeUsuarios",new UsuarioService().Listar());
            }

        }

        public IActionResult cadastroRealizado()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();
        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}