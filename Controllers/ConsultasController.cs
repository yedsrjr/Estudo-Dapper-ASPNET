using System.Runtime.InteropServices;
using AspNet_MVC.Models.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class ConsultasController : Controller
{
    private readonly ConsultasRepository repository;
    private readonly PacientesRepository repositoryPacientes;
    private readonly MedicosRepository repositoryMedicos;

    public ConsultasController(
        ConsultasRepository _repository,
        PacientesRepository _repositoryPacientes,
        MedicosRepository _repositoryMedicos
    )
    {
        repository = _repository;
        repositoryPacientes = _repositoryPacientes;
        repositoryMedicos = _repositoryMedicos;
    }

    public IActionResult Index()
    {
        var ListaConsultas = repository.BuscarTodos();
        var NovaListaConsultas = ListaConsultas.Select(model => new ConsultasViewModel
        {
            codm = model.codm,
            codp = model.codp,
            data = model.data,
            hora = model.hora,
            nomeMedico = repositoryMedicos.Buscar(model.codm)?.nome,            
            nomePaciente = repositoryPacientes.Buscar(model.codp)?.nome                
        }).ToList();
        return View("Listar", NovaListaConsultas);
    }
    public IActionResult Cadastro(int codm = 0 , DateTime? data = null, TimeSpan? hora = null)
    {
        if (codm == 0)
        {
            ConsultasViewModel model = new ConsultasViewModel { codm = codm };
            model.Pacientes = repositoryPacientes.BuscarTodos().Select(
                p => new SelectListItem
                {
                    Value = p.codp.ToString(),
                    Text = p.nome
                }
            ).ToList();
            model.Medicos = new List<SelectListItem>();
            return View(model);
        }
        else
        {
            var model = repository.Buscar(codm, data.GetValueOrDefault(), hora.GetValueOrDefault());
            ConsultasViewModel newModel = new ConsultasViewModel
            {
                codm = model.codm,
                codp = model.codp,
                data = model.data,
                hora = model.hora   
            };
            
             newModel.Pacientes = repositoryPacientes.BuscarTodos().Select(
                p => new SelectListItem
                {
                    Value = p.codp.ToString(),
                    Text = p.nome
                }
            ).ToList();
            newModel.Medicos = new List<SelectListItem>();
            return View(newModel);
        }
    }

    public IActionResult Excluir(int codm)
    {
        repository.Excluir(new Consultas{codm = codm});
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(ConsultasViewModel model)
    {
        if (ModelState.IsValid)
        {
            Consultas newModel = new Consultas
            {
                codm = model.codm,
                codp = model.codp,
                data = model.data,
                hora = model.hora  
            };

            //if (model.codp == 0)
                repository.Salvar(newModel);
            //else
            //    repository.Atualizar(newModel);
        }
        return RedirectToAction("Index");
    }
}