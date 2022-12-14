using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SalonApiNew.Models
{
    public partial class Records
    {
        public int IdRecord { get; set; }
        public DateTime RecordDateTime { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ServiceName { get; set; }
    }
}
