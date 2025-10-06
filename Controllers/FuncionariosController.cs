using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Models.Data;
using Models.Entidades;
using Models.Services;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class FuncionariosController : Controller
{
    private readonly FuncionariosServices repository;
    public FuncionariosController(FuncionariosServices _repository)
    {
        repository = _repository;
    }

    public IActionResult Index()
    {
        var ListaFuncionarios = repository.ListaFuncionarios();
        return View("Listar", ListaFuncionarios);
    }
    public IActionResult Cadastro(int codf = 0)
    {
        var model = repository.Cadastro(codf);
        return View(model);
    }

    public IActionResult Excluir(int codf)
    {
        repository.Excluir(codf);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(FuncionariosViewModel model)
    {
        if (ModelState.IsValid)
        {
            repository.Salvar(model);
        }    
        return RedirectToAction("Index");
    }
}