using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.SQL
{
    public class FuelReportView
    {
        public const string createSql = @"CREATE OR ALTER VIEW FuelReportView
											AS
											SELECT 
												CASE WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 1 THEN 'Styczeń'
													 WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 2 THEN 'Luty'
													 WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 3 THEN 'Marzec'
													 WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 4 THEN 'Kwiecień'
													 WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 5 THEN 'Maj'
													 WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 6 THEN 'Czerwiec'
													 WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 7 THEN 'Lipiec'
													 WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 8 THEN 'Sierpień'
													 WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 9 THEN 'Wrzesień'
													 WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 10 THEN 'Październik'
													 WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 11 THEN 'Listopad'
													 WHEN FORMAT(fuelReport.RefuelDate, 'MM') = 12 THEN 'Grudzień'
												END as [Month],
												CAST(AVG((fuelReport.FuelAmount * 100) / fuelReport.TraveledDistance) as decimal(18,2)) as [AverageConsumption],
												CAST(AVG((fuelReport.Cost * 100) / fuelReport.TraveledDistance) as decimal(18,2)) as [AverageCost],
												fuelReport.UserId as [UserId]
											FROM FuelReports fuelReport
											GROUP BY FORMAT(fuelReport.RefuelDate, 'MM'), fuelReport.UserId";
    }
}
