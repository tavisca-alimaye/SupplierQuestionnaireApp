using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SupplierQuestionnaire.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class SupplierQuestionnaireDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswerMapper> QuestionAnswerMappers { get; set; }
        public DbSet<Verb> Verbs { get; set; }
    }
}