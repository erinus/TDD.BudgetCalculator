using System;

namespace TDD.BudgetCalculator
{
    internal class BudgetCalculator
    {
        private BudgetRepository _budgetRepository;

        public BudgetCalculator()
        {

        }

        public decimal Summarize(DateTime start, DateTime end)
        {
            var budgets = _budgetRepository.GetAll();

            var total = 0;

            var startYM = Int32.Parse($"{start:yyyyMM}");
            var endYM = Int32.Parse($"{end:yyyyMM}");

            var startMonthDays = DateTime.DaysInMonth(start.Year, start.Month);
            var endMonthDays = DateTime.DaysInMonth(end.Year, end.Month);

            foreach (var budget in budgets)
            {
                var budgetYM = Int32.Parse(budget.YearMonth);
                
                if (startYM <= budgetYM && budgetYM <= endYM)
                {
                    total += budget.Amount;
                }

                if (budgetYM == startYM)
                {
                    total -= (start.Day - 1) * budget.Amount / startMonthDays;
                }

                if (budgetYM == endYM)
                {
                    total -= (endMonthDays - end.Day) * budget.Amount / endMonthDays;
                }
            }

            return total;
        }

        public void SetRepository(BudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
    }
}