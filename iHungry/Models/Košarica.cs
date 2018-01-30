using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iHungry.Models
{
    public class Košarica
    {
        public int Id { get; set; }
        public int JeloId { get; set; }
        public Jelo Jelo { get; set; }
        [Range(1, int.MaxValue)]
        public int Količina { get; set; }
        
    }
}
