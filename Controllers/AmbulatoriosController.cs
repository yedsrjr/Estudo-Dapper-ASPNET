using System.Runtime.InteropServices;
using AspNet_MVC.Models.Entidades;
using Microsoft.AspNetCore.Mvc;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class AmbulatoriosController : Controller
{
    private readonly AmbulatoriosRepository repository;
    public AmbulatoriosController(AmbulatoriosRepository _repository)
    {
        repository = _repository;
    }

    public IActionResult Index()
    {
        var ListaAmbulatorios = repository.BuscarTodos();
        var NovaListaAmbulatorios= ListaAmbulatorios.Select(model => new AmbulatoriosViewModel
        {
                nroa = model.nroa,
                andar = model.andar,
                capacidade = model.capacidade
        }).ToList();
        return View("Listar", NovaListaAmbulatorios);
    }
    public IActionResult Cadastro(int nroa = 0)
    {
        if (nroa == 0)
        {
            AmbulatoriosViewModel model = new AmbulatoriosViewModel { nroa = nroa };
            return View(model);
        }
        else
        {
            var model = repository.Buscar(nroa);
            AmbulatoriosViewModel newModel = new AmbulatoriosViewModel
            {
                nroa = model.nroa,
                andar = model.andar,
                capacidade = model.capacidade
            };
            return View(newModel);
        }
    }

    public IActionResult Excluir(int nroa)
    {
        repository.Excluir(new Ambulatorios{nroa = nroa});
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(AmbulatoriosViewModel model)
    {
        if (ModelState.IsValid)
        {
            Ambulatorios newModel = new Ambulatorios
            {
                nroa = model.nroa,
                andar = model.andar,
                capacidade = model.capacidade
            };

            if (model.nroa == 0)
            { 
                repository.Salvar(newModel);
                Console.WriteLine("Chegamos aqui");
            }
            else
                repository.Atualizar(newModel);
        }
        return RedirectToAction("Index");
    }
}