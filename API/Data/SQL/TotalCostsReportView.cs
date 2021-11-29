using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.SQL
{
    public class TotalCostsReportView
    {
		public const string createSql = @"CREATE OR ALTER VIEW TotalCostsReportView
											AS
											SELECT CASE WHEN t.[Month] = 1 THEN 'Styczeń'
													 WHEN t.[Month] = 2 THEN 'Luty'
													 WHEN t.[Month] = 3 THEN 'Marzec'
													 WHEN t.[Month] = 4 THEN 'Kwiecień'
													 WHEN t.[Month] = 5 THEN 'Maj'
													 WHEN t.[Month] = 6 THEN 'Czerwiec'
													 WHEN t.[Month] = 7 THEN 'Lipiec'
													 WHEN t.[Month] = 8 THEN 'Sierpień'
													 WHEN t.[Month] = 9 THEN 'Wrzesień'
													 WHEN t.[Month] = 10 THEN 'Październik'
													 WHEN t.[Month] = 11 THEN 'Listopad'
													 WHEN t.[Month] = 12 THEN 'Grudzień'
												END as [Month], 
												SUM(t.[CostPerMonth]) as [TotalCostPerMonth], 
												t.[UserId] as [UserId]
											FROM (
											SELECT
											    FORMAT(repairReport.RepairDate, 'MM') as [Month],
												CAST(repairReport.Cost as decimal(18,2)) as [CostPerMonth],
												repairReport.UserId as [UserId]
											FROM RepairReports repairReport
											UNION
											SELECT
												FORMAT(fuelReport.RefuelDate, 'MM') as [Month],
												CAST(fuelReport.Cost as decimal(18,2)) as [CostPerMonth],
												fuelReport.UserId as [UserId]
											FROM FuelReports fuelReport
											) t
											GROUP BY t.[Month], t.[UserId]";

	}
}
