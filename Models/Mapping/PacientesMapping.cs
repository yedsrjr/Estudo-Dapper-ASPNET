using Models.Entidades;
using Models.ViewModel;

namespace AspNet_MVC.Models.Mapping;

public static class PacientesMapping
{
    public static Pacientes ToPaciente(this PacientesViewModel paciente)
    {
        return new Pacientes
        {
            codp = paciente.codp,
            nome = paciente.nome,
            cidade = paciente.cidade,
            idade = paciente.idade,
            doenca = paciente.doenca,
            CPF = paciente.CPF,
            imagePath = paciente.imagePath
        };
    }

    public static PacientesViewModel ToModel(this Pacientes paciente)
    {
        return new PacientesViewModel
        {
            cidade = paciente.cidade,
            codp = paciente.codp,
            CPF = paciente.CPF,
            doenca = paciente.doenca,
            idade = paciente.idade,
            nome = paciente.nome,
            imagePath = paciente.imagePath
        };
    }
}