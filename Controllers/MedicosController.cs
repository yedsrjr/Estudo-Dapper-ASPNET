using Microsoft.AspNetCore.Mvc;
using Models.Services;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class MedicosController : Controller
{
    private readonly MedicosServices repository;
    public MedicosController(MedicosServices _repository)
    {
        repository = _repository;
    }

    public IActionResult Index()
    {
        var listaMedicos = repository.ListaMedicos();
        return View("Listar", listaMedicos);
    }
    public IActionResult Cadastro(int codm = 0)
    {
        var model = repository.Cadastro(codm);
        
        return View(model);
    }

    public IActionResult Excluir(int codm)
    {
        repository.Excluir(codm);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(MedicosViewModel model)
    {
        if (ModelState.IsValid)
        {
            repository.Salvar(model);
        }

        return RedirectToAction("Index");
    }
}