using System;
using techAPI.Models.Interfaces;

namespace techAPI.Models.Models
{
    public class Device : IEntity<Guid>
    {
        public Guid Id { get ; set ; }
        public bool IsDeleted { get ; set ; }
        public int Category { get; set; }
        public string Name { get; set; }
        public int  Count { get; set; }
        public string AccountingBook { get; set; }
        public string SerialNumber { get; set; }
        public string PreciousMetals { get; set; }
        public string Place { get; set; }
    }
}
