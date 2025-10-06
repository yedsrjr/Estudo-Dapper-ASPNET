using AspNet_MVC.Models.Entidades;
using Microsoft.AspNetCore.Mvc;
using Models.Services;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class AmbulatoriosController : Controller
{
    private readonly AmbulatoriosServices repository;
    public AmbulatoriosController(AmbulatoriosServices _repository)
    {
        repository = _repository;
    }

    public IActionResult Index()
    {
        var listaAmbulatorios = repository.ListaAmbulatorios();

        return View("Listar", listaAmbulatorios);
    }
    public IActionResult Cadastro(int nroa = 0)
    {
        var model = repository.BuscarCadastro(nroa);
        return View(model);
    }

    public IActionResult Excluir(int nroa)
    {
        repository.Excluir(nroa);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(AmbulatoriosViewModel model)
    {
        if (ModelState.IsValid)
        {
            repository.SalvarAmbulatorios(model);
        }
        
        return RedirectToAction("Index");
    }
}