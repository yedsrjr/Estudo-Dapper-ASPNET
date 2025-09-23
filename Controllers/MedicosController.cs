using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class MedicosController : Controller
{
    private readonly MedicosRepository repository;
    public MedicosController(MedicosRepository _repository)
    {
        repository = _repository;
    }

    public IActionResult Index()
    {
        var ListaMedicos = repository.BuscarTodos();
        var NovaListaMedicos = ListaMedicos.Select(model => new MedicosViewModel
        {
                codm = model.codm,
                nome = model.nome,
                idade = model.idade,
                especialidade = model.especialidade,
                cidade = model.cidade,
                CPF = model.CPF,
                nroa = model.nroa
        }).ToList();
        return View("Listar", NovaListaMedicos);
    }
    public IActionResult Cadastro(int codm = 0)
    {
        if (codm == 0)
        {
            MedicosViewModel model = new MedicosViewModel { codm = codm };
            return View(model);
        }
        else
        {
            var model = repository.Buscar(codm);
            MedicosViewModel newModel = new MedicosViewModel
            {
                codm = model.codm,
                nome = model.nome,
                idade = model.idade,
                especialidade = model.especialidade,
                cidade = model.cidade,
                CPF = model.CPF,
                nroa = model.nroa
            };
            return View(newModel);
        }
    }

    public IActionResult Excluir(int codm)
    {
        repository.Excluir(new Medicos{codm = codm});
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(MedicosViewModel model)
    {
        if (ModelState.IsValid)
        {
            Medicos newModel = new Medicos
            {
                codm = model.codm,
                nome = model.nome,
                idade = model.idade,
                especialidade = model.especialidade,
                cidade = model.cidade,
                CPF = model.CPF,
                nroa = model.nroa
            };

            if (model.codm == 0)
                repository.Salvar(newModel);
            else
                repository.Atualizar(newModel);
        }
        return RedirectToAction("Index");
    }
}