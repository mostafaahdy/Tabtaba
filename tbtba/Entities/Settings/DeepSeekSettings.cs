using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.Settings
{
    public class DeepSeekSettings
    {
        public string ApiKey { get; set; } = string.Empty;
        public string ModelUrl { get; set; } = string.Empty;
    }
}
