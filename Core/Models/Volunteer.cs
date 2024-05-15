using System.ComponentModel.DataAnnotations;
using Core.Validation;


namespace Core.Models;

public class Volunteer
{
    [Required(ErrorMessage = "Du skal skrive dit krævnr")]
    public int Kræwnr { get; set; }

    [Required(ErrorMessage = "Du skal skrive dit navn")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Du skal skrive din email")]
    public string Email { get; set; }
}