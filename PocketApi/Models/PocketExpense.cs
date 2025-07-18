using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocketApi.Models
{
    [Table(nameof(PocketExpense))]
    public class PocketExpense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("expense_id")]
        public int ExpenseId { get; set; }

        [Required]
        [Column("expense_type")]
        public ExpenseType ExpenseType { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("expense_name")]
        public string ExpenseName { get; set; } = string.Empty;

        [Required]
        [Column("expense_date")]
        public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("expense_amount")]
        public double ExpenseAmount { get; set; }

        [Required]
        [Column("expense_category")]
        public CategoryExpense ExpenseCategory { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

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
}
