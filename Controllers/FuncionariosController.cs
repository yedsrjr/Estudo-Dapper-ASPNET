using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class FuncionariosController : Controller
{
    private readonly FuncionariosRepository repository;
    public FuncionariosController(FuncionariosRepository _repository)
    {
        repository = _repository;
    }

    public IActionResult Index()
    {
        var ListaFuncionarios = repository.BuscarTodos();
        var NovaListaFuncionarios = ListaFuncionarios.Select(model => new FuncionariosViewModel
        {
                codf = model.codf,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                salario = model.salario
        }).ToList();
        return View("Listar", NovaListaFuncionarios);
    }
    public IActionResult Cadastro(int codf = 0)
    {
        if (codf == 0)
        {
            FuncionariosViewModel model = new FuncionariosViewModel { codf = codf };
            return View(model);
        }
        else
        {
            var model = repository.Buscar(codf);
            FuncionariosViewModel newModel = new FuncionariosViewModel
            {
                codf = model.codf,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                salario = model.salario
            };
            return View(newModel);
        }
    }

    public IActionResult Excluir(int codf)
    {
        repository.Excluir(new Funcionarios{codf = codf});
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(FuncionariosViewModel model)
    {
        if (ModelState.IsValid)
        {
            Funcionarios newModel = new Funcionarios
            {
                codf = model.codf,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                salario = model.salario
            };

            if (model.codf == 0)
                repository.Salvar(newModel);
            else
                repository.Atualizar(newModel);
        }
        return RedirectToAction("Index");
    }
}