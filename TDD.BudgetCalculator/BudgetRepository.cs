using System.Collections.Generic;

namespace TDD.BudgetCalculator
{
    internal class BudgetRepository
    {
        private List<Budget> _budgets = new List<Budget>();

        public void SetBudgets(List<Budget> budgets)
        {
            _budgets.Clear();
            _budgets.AddRange(budgets);
        }

        public List<Budget> GetAll()
        {
            return _budgets;
        }
    }
}