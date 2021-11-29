using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.SQL
{
    public class RepairReportView
    {
        public const string createSql = @"CREATE OR ALTER VIEW RepairReportView
											AS
											SELECT 
												CASE WHEN FORMAT(repairReport.RepairDate, 'MM') = 1 THEN 'Styczeń'
													 WHEN FORMAT(repairReport.RepairDate, 'MM') = 2 THEN 'Luty'
													 WHEN FORMAT(repairReport.RepairDate, 'MM') = 3 THEN 'Marzec'
													 WHEN FORMAT(repairReport.RepairDate, 'MM') = 4 THEN 'Kwiecień'
													 WHEN FORMAT(repairReport.RepairDate, 'MM') = 5 THEN 'Maj'
													 WHEN FORMAT(repairReport.RepairDate, 'MM') = 6 THEN 'Czerwiec'
													 WHEN FORMAT(repairReport.RepairDate, 'MM') = 7 THEN 'Lipiec'
													 WHEN FORMAT(repairReport.RepairDate, 'MM') = 8 THEN 'Sierpień'
													 WHEN FORMAT(repairReport.RepairDate, 'MM') = 9 THEN 'Wrzesień'
													 WHEN FORMAT(repairReport.RepairDate, 'MM') = 10 THEN 'Październik'
													 WHEN FORMAT(repairReport.RepairDate, 'MM') = 11 THEN 'Listopad'
													 WHEN FORMAT(repairReport.RepairDate, 'MM') = 12 THEN 'Grudzień'
												END as [Month],
												CAST(SUM(repairReport.Cost) as decimal(18,2)) as [CostPerMonth],
												repairReport.UserId as [UserId]
											FROM RepairReports repairReport
											GROUP BY FORMAT(repairReport.RepairDate, 'MM'), repairReport.UserId";
    }
}
