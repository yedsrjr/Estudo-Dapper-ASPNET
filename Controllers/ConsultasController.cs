using Microsoft.AspNetCore.Mvc;
using Models.Services;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class ConsultasController : Controller
{
    private readonly ConsultasServices consultasServices;

    public ConsultasController(
        ConsultasServices _consultasServices
    )
    {
        consultasServices = _consultasServices;
    }

    public IActionResult Index()
    {
        var novaListaConsultas = consultasServices.ListaConsultas();
        return View("Listar", novaListaConsultas);
    }
    public IActionResult Cadastro(int codm = 0, DateTime? data = null, TimeSpan? hora = null)
    {
        var model = consultasServices.BuscaCadastro(codm, data, hora);
        return View(model);
    }
    
    public IActionResult Excluir(int codm, DateTime data, TimeSpan hora)
    {
        consultasServices.Excluir(codm, data, hora);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(ConsultasViewModel model)
    {
        if (ModelState.IsValid)
        {
            consultasServices.SalvarConsulta(model);
        }
        return RedirectToAction("Index");
    }
}