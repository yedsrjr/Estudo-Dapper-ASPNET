
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel;

public class AmbulatoriosViewModel
{
    [Display(Name = "Codigo")]
    public int nroa { get; set; }

    [Display(Name = "Andar")]
    [Required(ErrorMessage = "O andar é obrigatório")]
    public int andar { get; set; }

    [Display(Name = "Capacidade")]
    [Required(ErrorMessage = "A capacidade é obrigatório")]
    public int capacidade { get; set; }
}