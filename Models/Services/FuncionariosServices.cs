using Models.Data;
using Models.ViewModel;

namespace Models.Services
{
    public class FuncionariosServices
    {
        private readonly FuncionariosRepository repository;

        public FuncionariosServices(FuncionariosRepository _repository)
        {
            repository = _repository;
        }

        public List<FuncionariosViewModel> ListaFuncionarios()
        {
            var ListaFuncionarios = repository.BuscarTodos();
            var NovaListaFuncionarios = ListaFuncionarios.Select(model => new FuncionariosViewModel
            {
                codf = model.codf,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                salario = model.salario
            }).ToList();
            return NovaListaFuncionarios;
        }

        public FuncionariosViewModel Cadastro(int codf = 0)
        {
            if (codf == 0)
            {
                FuncionariosViewModel model = new FuncionariosViewModel { codf = codf };
                return model;
            }
            else
            {
                var model = repository.Buscar(codf);
                FuncionariosViewModel newModel = new FuncionariosViewModel
                {
                    codf = model.codf,
                    nome = model.nome,
                    idade = model.idade,
                    cidade = model.cidade,
                    CPF = model.CPF,
                    salario = model.salario
                };
                return newModel;
            }
        }

        public void Excluir(int codf)
        {
            repository.Excluir(new Funcionarios { codf = codf });
        }

        public void Salvar(FuncionariosViewModel model)
        {
            Funcionarios newmodel = new Funcionarios
            {
                codf = model.codf,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                salario = model.salario
            };

            if (model.codf == 0)
                repository.Salvar(newmodel);
            else
                repository.Atualizar(newmodel);
        }
    }
}