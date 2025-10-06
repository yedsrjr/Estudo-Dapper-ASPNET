using Models.ViewModel;
using AspNet_MVC.Models.Entidades;
using Models.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.Services
{
    public class ConsultasServices
    {
        private readonly ConsultasRepository repository;

        private readonly PacientesRepository repositoryPacientes;
        private readonly MedicosRepository repositoryMedicos;
        public ConsultasServices(ConsultasRepository _repository, PacientesRepository _pacientesRepository, MedicosRepository _medicosRepository)
        {
            repository = _repository;
            repositoryPacientes = _pacientesRepository;
            repositoryMedicos = _medicosRepository;
        }
        public List<ConsultasViewModel> ListaConsultas()
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
            return NovaListaConsultas;
        }
        public void SalvarConsulta(ConsultasViewModel model)
        {

            Consultas newModel = new Consultas
            {
                codm = model.codm,
                codp = model.codp,
                data = model.data,
                hora = model.hora
            };

            repository.Salvar(newModel);
        }

        public ConsultasViewModel BuscaCadastro(int codm = 0, DateTime? data = null, TimeSpan? hora = null)
        {
            ConsultasViewModel model;
            if (codm == 0)
            {

                model = new ConsultasViewModel { codm = codm, data = DateTime.Today};
                model.Pacientes = repositoryPacientes.BuscarTodos().Select(
                    p => new SelectListItem
                    {
                        Value = p.codp.ToString(),
                        Text = p.nome
                    }
                ).ToList();
                model.Medicos = repositoryMedicos.BuscarTodos().Select(
                    m => new SelectListItem
                    {
                        Value = m.codm.ToString(),
                        Text = m.nome
                    }
                ).ToList();
                return model;
            }
            else
            {
                var modelDb = repository.Buscar(codm, data.GetValueOrDefault(), hora.GetValueOrDefault());
                ConsultasViewModel newModel = new ConsultasViewModel
                {
                    codm = modelDb.codm,
                    codp = modelDb.codp,
                    data = modelDb.data,
                    hora = modelDb.hora
                };

                newModel.Pacientes = repositoryPacientes.BuscarTodos().Select(
                p => new SelectListItem
                {
                    Value = p.codp.ToString(),
                    Text = p.nome
                }
                ).ToList();
                newModel.Medicos = new List<SelectListItem>();
                return newModel;
            }
        }
        public void Excluir(int codm, DateTime data, TimeSpan hora)
        {
            repository.Excluir(new Consultas{codm = codm, data = data, hora = hora});
        }
    }
}