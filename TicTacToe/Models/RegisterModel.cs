using System.ComponentModel.DataAnnotations;

public class RegisterModel
{
    [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "De gebruikersnaam moet tussen de 3 en 50 tekens zijn.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Wachtwoord is verplicht.")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Het wachtwoord moet minimaal 8 tekens lang zijn.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$", ErrorMessage = "Het wachtwoord moet minstens één hoofdletter, één cijfer en één speciaal teken bevatten.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Bevestig je wachtwoord.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Het wachtwoord en de bevestiging komen niet overeen.")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "E-mail is verplicht.")]
    [EmailAddress(ErrorMessage = "Voer een geldig e-mailadres in.")]
    public string Email { get; set; }
}
