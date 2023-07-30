# Note to reviewers:

The solution works with mock data and it's covered by UTs, with a focus on math and extensibility.
I've added comments in code about certain parts that needs explaining the decision.

I'm referring to the biweek as "bracket". So we have 26 brackets per year.
I'm assuming the money has always two precision points. e.g. `$123.45`

## Utils directory

Probably not the best approach for all the classes here, but I'm trying to keep it simple and fast.
So I'm using extensions to keep the code small.

- `ContainerExtensions` : Registered all IoC dependencies here. (Only for API project)
- `ApiResponseUtils` : I assume we have something like this in our infrastructure for DRY
- `Clock` : I assumed we can't rely on local time in production (I don't know about paylocity's app specs)
- `DependentExtensions` : To calculate age -> we should have better utils for this
- `EmployeeExtensions` : To identify invalid data -> probably the ugliest part of my code!
- `MoneyHelpers` : basically math utils

## Strategies

I used strategy pattern for calculating salary costs. I think it's the right pattern.
The interface `ISalaryCostStrategy` requires an implementation of `CalculateAnnualCost`.
The interface calculates the costs for each bracket itself (It's C#11 feature I think).

All strategies are registered in IoC, we just pass an `IEnumerable<ISalaryCostStrategy>` to `PaychecksController`.

## /paychecks/id/idx

I've added a new endpoint called "paychecks" and it only creates a new paycheck for an employee id (returns 404 if employee didn't exist) and a given bracket index (which should be less than 26 but no validations here).

There is no model and no mock data behind paycheck. There is only `DTO` for creating it.

## CreatePaycheckDto

`CreatePaycheckDto` has:

- `PaycheckBaseSalary` = `AnnualSalary` divided by bracket count and adjusted evenly throughout the year.
- `PaycheckItems` = a collection of additional costs, each of which has a name and amount.
  - e.g. `{BaseCostSalaryStrategy, 600.00}`
- `FinalAmount` = `PaycheckBaseSalary` + sum of `PaycheckItems`.
