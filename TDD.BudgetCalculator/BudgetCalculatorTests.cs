using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDD.BudgetCalculator
{
    // [Budgets]
    // YearMonth Amount
    //    201804    300
    //    201805    620
    //    201807    930

    // [Scenarios]
    //      Start        End Total
    // 2018-03-31 2018-03-31     0
    // 2018-04-01 2018-04-30   300
    // 2018-04-01 2018-05-31   920
    // 2018-04-01 2018-06-30   920
    // 2018-04-01 2018-04-01    10
    // 2018-04-30 2018-04-30    10
    // 2018-04-01 2018-05-01   320
    // 2018-04-30 2018-05-31   630
    // 2018-04-30 2018-07-01   660

    [TestClass]
    public class BudgetCalculatorTests
    {
        private BudgetRepository _budgetRepository;
        private BudgetCalculator _budgetCalculator;

        public BudgetCalculatorTests()
        {
            _budgetRepository = new BudgetRepository();
            _budgetCalculator = new BudgetCalculator();
        }

        [TestMethod]
        public void HasNoBudgetInDateRange()
        {
            _budgetRepository.SetBudgets(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201804",
                    Amount = 300
                }
            });

            _budgetCalculator.SetRepository(_budgetRepository);

            var exp = 0;
            var act = _budgetCalculator.Summarize(DateTime.Parse("2018-03-31"), DateTime.Parse("2018-03-31"));

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void OneMonth()
        {
            _budgetRepository.SetBudgets(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201804",
                    Amount = 300
                }
            });

            _budgetCalculator.SetRepository(_budgetRepository);

            var exp = 300;
            var act = _budgetCalculator.Summarize(DateTime.Parse("2018-04-01"), DateTime.Parse("2018-04-30"));

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TwoMonth()
        {
            _budgetRepository.SetBudgets(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201804",
                    Amount = 300
                },
                new Budget
                {
                    YearMonth = "201805",
                    Amount = 620
                }
            });

            _budgetCalculator.SetRepository(_budgetRepository);

            var exp = 920;
            var act = _budgetCalculator.Summarize(DateTime.Parse("2018-04-01"), DateTime.Parse("2018-05-31"));

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void ThreeMonthButEndMonthHasNoBudget()
        {
            _budgetRepository.SetBudgets(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201804",
                    Amount = 300
                },
                new Budget
                {
                    YearMonth = "201805",
                    Amount = 620
                }
            });

            _budgetCalculator.SetRepository(_budgetRepository);

            var exp = 920;
            var act = _budgetCalculator.Summarize(DateTime.Parse("2018-04-01"), DateTime.Parse("2018-06-30"));

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void OneDayIsMonthFirstDay()
        {
            _budgetRepository.SetBudgets(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201804",
                    Amount = 300
                }
            });

            _budgetCalculator.SetRepository(_budgetRepository);

            var exp = 10;
            var act = _budgetCalculator.Summarize(DateTime.Parse("2018-04-01"), DateTime.Parse("2018-04-01"));

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void OneDayIsMonthEndDay()
        {
            _budgetRepository.SetBudgets(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201804",
                    Amount = 300
                }
            });

            _budgetCalculator.SetRepository(_budgetRepository);

            var exp = 10;
            var act = _budgetCalculator.Summarize(DateTime.Parse("2018-04-30"), DateTime.Parse("2018-04-30"));

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void OneMonthAndOneDayIsMonthFirstDay()
        {
            _budgetRepository.SetBudgets(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201804",
                    Amount = 300
                },
                new Budget
                {
                    YearMonth = "201805",
                    Amount = 620
                }
            });

            _budgetCalculator.SetRepository(_budgetRepository);

            var exp = 320;
            var act = _budgetCalculator.Summarize(DateTime.Parse("2018-04-01"), DateTime.Parse("2018-05-01"));

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void OneMonthAndOneDayIsMonthEndDay()
        {
            _budgetRepository.SetBudgets(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201804",
                    Amount = 300
                },
                new Budget
                {
                    YearMonth = "201805",
                    Amount = 620
                }
            });

            _budgetCalculator.SetRepository(_budgetRepository);

            var exp = 630;
            var act = _budgetCalculator.Summarize(DateTime.Parse("2018-04-30"), DateTime.Parse("2018-05-31"));

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void OneMonthAndOneDayIsMonthEndDayAndOneDayIsMonthFirstDay()
        {
            _budgetRepository.SetBudgets(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201804",
                    Amount = 300
                },
                new Budget
                {
                    YearMonth = "201805",
                    Amount = 620
                },
                new Budget
                {
                    YearMonth = "201807",
                    Amount = 930
                }
            });

            _budgetCalculator.SetRepository(_budgetRepository);

            var exp = 660;
            var act = _budgetCalculator.Summarize(DateTime.Parse("2018-04-30"), DateTime.Parse("2018-07-01"));

            Assert.AreEqual(exp, act);
        }
    }
}
