using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Earnings;

public class MonthlyEarning
{
    public string Month { get; set; } = default!;
    public decimal Amount { get; set; }
}