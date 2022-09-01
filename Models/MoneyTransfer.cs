namespace BankManagementDotnetApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class MoneyTransfer
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "Please Enter Amount")]
    [Display(Name = "Enter Amount")] 
    public int? Amount { get; set; }
    [Display(Name = "Select Debit Account")] 

    public int? SenderAccountNumber{get;set;}  
    [NotMapped]
    [Display(Name="Comments(Optional)")]
     public string? Comments{get;set;}  
     [Required]
     public int?RegisteredPayeeId{get;set;}
    public RegisteredPayee?RegisteredPayee{get;set;}
     
}