using PocketApi.Models;
using PocketApi.Models.Dto.Expense;

namespace PocketApi.Mappers;

public static class ExpenseMapper
{
    public static ExpenseDto ToDto(this PocketExpense entity)
    {
        return new ExpenseDto
        {
            ExpenseId = entity.ExpenseId,
            ExpenseType = entity.ExpenseType,
            ExpenseName = entity.ExpenseName,
            ExpenseCategory = entity.ExpenseCategory,
            ExpenseAmount = entity.ExpenseAmount,
            ExpenseDate = entity.ExpenseDate,
        };
    }

    public static PocketExpense ToEntity(this ExpenseDto dto, int userId)
    {
        return new PocketExpense
        {
            ExpenseId = dto.ExpenseId ?? 0,
            ExpenseType = dto.ExpenseType,
            ExpenseName = dto.ExpenseName,
            ExpenseDate = dto.ExpenseDate,
            ExpenseAmount = dto.ExpenseAmount,
            ExpenseCategory = dto.ExpenseCategory,
            UserId = userId
        };
    }

    public static void UpdateEntity(this PocketExpense entity, ExpenseDto dto)
    {
        entity.ExpenseType = dto.ExpenseType;
        entity.ExpenseName = dto.ExpenseName;
        entity.ExpenseDate = dto.ExpenseDate;
        entity.ExpenseAmount = dto.ExpenseAmount;
        entity.ExpenseCategory = dto.ExpenseCategory;
    }

    public static List<ExpenseDto> ToDtoList(this IEnumerable<PocketExpense> entities)
    {
        return entities.Select(e => e.ToDto()).ToList();
    }
}