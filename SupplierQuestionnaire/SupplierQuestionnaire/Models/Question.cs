using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SupplierQuestionnaire.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public string Keyword { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int VerbId { get; set; }
        [ForeignKey("VerbId")]
        public Verb Verb { get; set; }
    }

   
}