using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entites
{
    public class Appointment
    {

        public int Id { get; set; } 
        public DateTime Date_Time { get; set; }
        public string Session_Type { get; set; }
        public decimal Duration_Minutes { get; set; }
        public string Location_Mode { get; set; } 
        public string Status { get; set; }

       
        public int PatientId { get; set; } 
        public int DoctorId { get; set; } 

       
        public  Patient Patient { get; set; }
        public  Doctors Doctor { get; set; }
    }
}

