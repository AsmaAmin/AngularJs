using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Evidence_11CoreAPI_Angularjs.ViewModels
{
    public class VmGames
    {[Key]
        public int Id { get; set; }
        [Required]
       // [StringLength(10)]
        public string Name { get; set; }

    }
}
