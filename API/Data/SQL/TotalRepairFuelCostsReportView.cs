using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.SQL
{
    public class TotalRepairFuelCostsReportView
    {
        public const string createSql = @"CREATE OR ALTER VIEW TotalRepairFuelCostsReportView
                                            AS
                                            SELECT 'Wydatki na paliwo' as [Text]
	                                              ,SUM(fuel.Cost) as [Value]
                                            FROM FuelReports fuel
                                            UNION
                                            SELECT 'Wydatki na naprawy i koszty eksploatacyjne' as [Text]
	                                              ,SUM(repair.Cost) as [Value]
                                            FROM RepairReports repair";
    }
}
