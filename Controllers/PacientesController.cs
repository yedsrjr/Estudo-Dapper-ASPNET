using Microsoft.AspNetCore.Mvc;
using Models.Entidades;
using Models.ViewModel;
using Microsoft.IdentityModel.Tokens;
using Models.Services;
using Microsoft.VisualBasic;

namespace AspNet_MVC.Controllers;

public class PacientesController : Controller
{
    private readonly PacientesServices repository;
    public PacientesController(PacientesServices _repository)
    {
        repository = _repository;
    }   

    public IActionResult Index()
    {
        var listaPacientes = repository.ListarPacientes();

        return View("Listar", listaPacientes);
    }
    public IActionResult Cadastro(int codp = 0)
    {
        var model = repository.Cadastro(codp);

        return View(model);
        
    }

    public IActionResult Excluir(int codp)
    {
        repository.Excluir(codp);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Salvar(PacientesViewModel model)
    {
        if (ModelState.IsValid)
        {
            repository.Salvar(model);
        }
        
        return RedirectToAction("Index");
    }
}