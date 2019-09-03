using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    public class Dept
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
