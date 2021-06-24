using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Person : IEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public string Name { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(150, ErrorMessage = "First name exceeds {1} chracters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "last name is required.")]
        [StringLength(150, ErrorMessage = "Last name exceeds {1} chracters.")]
        public string LastName { get; set; }

        [Display(Name = "Created")]
        [Required(ErrorMessage = "Created is required.")]
        public DateTime Created { get; set; }

        [Display(Name = "Last Modified")]
        [Required(ErrorMessage = "Last modified is required.")]
        public DateTime LastModified { get; set; }
    }
}
