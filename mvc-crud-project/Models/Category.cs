using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc_crud_project.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Required")]
        public string Name { get; set; }

        public IList<Product> Products { get; set; }
    }
}