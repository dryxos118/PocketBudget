using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PocketWeb.Models;

public class PocketExpense
{
    public int ExpenseId { get; set; }

    public ExpenseType ExpenseType { get; set; } = ExpenseType.Expense;

    [Required(ErrorMessage = "Le nom de la dépense est obligatoire.")]
    public string ExpenseName { get; set; } = string.Empty;

    public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Le montant est obligatoire.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "La valeur doit être supérieure à zéro.")]
    public double ExpenseAmount { get; set; }

    public CategoryExpense ExpenseCategory { get; set; } = CategoryExpense.Miscellaneous;

    public int UserId { get; set; }

    // one-to-many
    public PocketUser? User { get; set; }
}

public enum CategoryExpense
{
    [Description(nameof(Miscellaneous))]
    Miscellaneous = 1,
    [Description(nameof(Food))]
    Food = 2,
    [Description(nameof(Bill))]
    Bill = 3,
    [Description(nameof(Transport))]
    Transport = 4,
    [Description(nameof(Leisure))]
    Leisure = 5,
    [Description(nameof(Housing))]
    Housing = 6,
    [Description(nameof(Health))]
    Health = 7,
    [Description(nameof(Education))]
    Education = 8
}

public enum ExpenseType
{
    [Description(nameof(Expense))]
    Expense = 1,
    [Description(nameof(Income))]
    Income = 2
}