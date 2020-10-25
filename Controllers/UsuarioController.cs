using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreinandoDDLBackend.Models;

namespace TreinandoDDLBackend.Controllers
{
    public class UsuarioController: Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(Usuario nusuario)
        {
            UsuarioBanco us = new UsuarioBanco();
            us.Inserir(nusuario);
            ViewBag.Mensagem = "usuario Criado Com Sucesso!";
            return View();
        }
        public IActionResult Listar()
        {
            UsuarioBanco ub = new UsuarioBanco();
            List<Usuario> lista = ub.BuscarTodos();
            return View(lista);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            UsuarioBanco ub = new UsuarioBanco();
            Usuario u = ub.QueryLogin(usuario);
            if( usuario != null)
            {
                HttpContext.Session.SetInt32("idUsuario", usuario.Id);
                HttpContext.Session.SetString("idNome", usuario.Nome);
                return View();
            }else
            {
                ViewBag.Mensagem = "Falha No Login!";
                return View();
            }
           
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Usuario");
        }
    }
}