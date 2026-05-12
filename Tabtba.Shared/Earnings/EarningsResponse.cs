using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Earnings;

public class EarningsResponse
{
    public decimal TotalAvailableEarnings { get; set; }
    public decimal PendingAmount { get; set; }
    public decimal MonthlyGrowthPercentage { get; set; }
    public List<MonthlyEarning> EarningsAnalysis { get; set; } = [];
    public List<TransactionDto> RecentTransactions { get; set; } = [];
}
