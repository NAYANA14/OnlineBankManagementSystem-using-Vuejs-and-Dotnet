namespace BankManagementDotnetApi.ViewModels;
using BankManagementDotnetApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class LoginViewModel
{
    [Required(ErrorMessage = "Please Enter Name")]  
    [Display(Name = "User Name")]  
    [StringLength(25)] 
    public string? UserName { get; set; }
    [Required(ErrorMessage = "Please Enter Password")] 
    [DataType(DataType.Password)]   
    [StringLength(25)] 
    public string? Password { get; set; }
}