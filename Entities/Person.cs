using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities
{
    [DataContract]
    public class Person : IEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [NotMapped]
        public string Name => $"{FirstName} {LastName}";

        [DataMember]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(150, ErrorMessage = "First name exceeds {1} chracters.")]
        public string FirstName { get; set; }

        [DataMember]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "last name is required.")]
        [StringLength(150, ErrorMessage = "Last name exceeds {1} chracters.")]
        public string LastName { get; set; }

        [DataMember]
        [Display(Name = "Created")]
        [Required(ErrorMessage = "Created is required.")]
        public DateTime Created { get; set; }

        [DataMember]
        [Display(Name = "Last Modified")]
        [Required(ErrorMessage = "Last modified is required.")]
        public DateTime LastModified { get; set; }
    }
}
