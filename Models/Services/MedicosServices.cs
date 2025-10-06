using Models.Data;
using Models.Entidades;
using Models.ViewModel;

namespace Models.Services
{
    public class MedicosServices
    {
        private readonly MedicosRepository repository;

        public MedicosServices(MedicosRepository _repository)
        {
            repository = _repository;
        }

        public List<MedicosViewModel> ListaMedicos()
        {
            var listaMedicos = repository.BuscarTodos();

            var novaListaMedicos = listaMedicos.Select(model => new MedicosViewModel
            {
                codm = model.codm,
                nome = model.nome,
                idade = model.idade,
                especialidade = model.especialidade,
                cidade = model.cidade,
                CPF = model.CPF,
                nroa = model.nroa
            }).ToList();
            return novaListaMedicos;
        }

        public MedicosViewModel Cadastro(int codm = 0)
        {
            if (codm == 0)
            {
                MedicosViewModel model = new MedicosViewModel { codm = codm };
                return model;
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
                return newModel;
            }
        }
        public void Excluir(int codm)
        {
            repository.Excluir(new Medicos { codm = codm });
        }
        public void Salvar(MedicosViewModel model)
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
            {
                repository.Atualizar(newModel);
            }
        }
    }
}