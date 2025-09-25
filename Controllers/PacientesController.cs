using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;
using AspNet_MVC.Models.Mapping;

namespace AspNet_MVC.Controllers;

public class PacientesController : Controller
{
    private readonly PacientesRepository repository;
    private readonly IWebHostEnvironment env;
    public PacientesController(PacientesRepository _repository, IWebHostEnvironment _env)
    {
        repository = _repository;
        env = _env;
    }   

    public IActionResult Index()
    {
        var ListaPacientes = repository.BuscarTodos();
        var NovaListaPacientes = ListaPacientes.Select(model => new PacientesViewModel
        {
                codp = model.codp,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                doenca = model.doenca
        }).ToList();
        return View("Listar", NovaListaPacientes);
    }
    public IActionResult Cadastro(int codp = 0)
    {
        if (codp == 0)
        {
            PacientesViewModel model = new PacientesViewModel { codp = codp };
            return View(model);
        }
        else
        {
            var model = repository.Buscar(codp);
            PacientesViewModel newModel = new PacientesViewModel
            {
                codp = model.codp,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                doenca = model.doenca
            };
            return View(newModel);
        }
    }

    public IActionResult Excluir(int codp)
    {
        repository.Excluir(new Pacientes{codp = codp});
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Salvar(PacientesViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                var fileName = Path.GetFileName(model.Image.FileName);
                var folderPath = Path.Combine(env.WebRootPath, "Imagens");

                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                var filePath = Path.Combine(folderPath, fileName);
                model.imagePath = fileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            if (model.codp == 0)
                repository.Salvar(model.ToPaciente());
            else
                repository.Atualizar(model.ToPaciente());
        }
        return RedirectToAction("Index");
    }
}