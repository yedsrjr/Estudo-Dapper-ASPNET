using Models.ViewModel;
using AspNet_MVC.Models.Entidades;
using Models.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.Services
{
    public class AmbulatoriosServices
    {
        private readonly AmbulatoriosRepository repository;
        public AmbulatoriosServices(AmbulatoriosRepository _repository)
        {
            repository = _repository;
        }
        public List<AmbulatoriosViewModel> ListaAmbulatorios()
        {
            var ListaAmbulatorios = repository.BuscarTodos();
            var NovaListaAmbulatorios = ListaAmbulatorios.Select(model => new AmbulatoriosViewModel
            {
                nroa = model.nroa,
                andar = model.andar,
                capacidade = model.capacidade
            }).ToList();

            return NovaListaAmbulatorios;
        }
        public void SalvarAmbulatorios(AmbulatoriosViewModel model)
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
            }
            else
            {
                repository.Atualizar(newModel);
            }
        }
        public void Excluir(int nroa)
        {
            repository.Excluir(new Ambulatorios { nroa = nroa });
        }
        public AmbulatoriosViewModel BuscarCadastro(int nroa = 0)
        {
            if (nroa == 0)
            {
                AmbulatoriosViewModel model = new AmbulatoriosViewModel { nroa = nroa };
                return model;
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
                return newModel;
            }
        }
    }
}