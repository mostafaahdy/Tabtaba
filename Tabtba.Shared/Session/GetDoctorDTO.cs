using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Session
{
    public class GetDoctorDTO
    { public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public double Rating { get; set; }
        
        public List<string> DiagnosisNames { get; set; } = new();
    }
};
    

