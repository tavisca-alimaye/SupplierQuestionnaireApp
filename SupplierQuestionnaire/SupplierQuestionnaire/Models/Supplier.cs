using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SupplierQuestionnaire.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    
}