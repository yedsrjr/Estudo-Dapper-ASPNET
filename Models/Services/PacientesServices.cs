using Microsoft.IdentityModel.Tokens;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;

namespace Models.Services
{
    public class PacientesServices
    {
        private readonly PacientesRepository repository;
        private readonly IWebHostEnvironment env;

        public PacientesServices(PacientesRepository _repository, IWebHostEnvironment _env)
        {
            repository = _repository;
            env = _env;
        }

        public List<PacientesViewModel> ListarPacientes()
        {
            var listaPacientes = repository.BuscarTodos();

            foreach (var item in listaPacientes)
            {
                if (item.imagePath.IsNullOrEmpty())
                {
                    item.imagePath = null;
                }
            }

            var novaListaPacientes = listaPacientes.Select(model => new PacientesViewModel
            {
                codp = model.codp,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                doenca = model.doenca,
                imagePath = model.imagePath
            }).ToList();

            return novaListaPacientes;
        }

        public PacientesViewModel Cadastro(int codp = 0)
        {
            if (codp == 0)
            {
                PacientesViewModel model = new PacientesViewModel { codp = codp };
                return model;
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
                    doenca = model.doenca,
                    imagePath = model.imagePath
                };
                return newModel;
            }
        }

        public void Excluir(int codp)
        {
            repository.Excluir(new Pacientes { codp = codp });
        }

        public async void Salvar(PacientesViewModel model)
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

            Pacientes novoPaciente = new Pacientes
            {
                    codp = model.codp,
                    nome = model.nome,
                    idade = model.idade,
                    cidade = model.cidade,
                    CPF = model.CPF,
                    doenca = model.doenca,
                    imagePath = model.imagePath
            };
            
            if (model.codp == 0)
            {
                repository.Salvar(novoPaciente);
            }
            else
            {
                repository.Atualizar(novoPaciente);
            }
            
        }
    }
}
