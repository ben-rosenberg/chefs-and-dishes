using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChefsAndDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [PastDate("Date of birth must be in the past")]
        [MinAge(18)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class PastDateAttribute : ValidationAttribute
    {
        public PastDateAttribute() : base() { _Message = "Date must be in the past"; }
        public PastDateAttribute(string message) : base() { _Message = message; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int isPast = ((DateTime)value).CompareTo(DateTime.Now);

            if (isPast >= 0)
            {
                return new ValidationResult(_Message);
            }

            return ValidationResult.Success;
        }

        private string _Message;
    }

    // Custom validation attribute for checking if a user is older than a certain
    // age when a date of birth is submitted with a form. Note that while this
    // validates a DateTime input from a field rather than an integer age, an 
    // integer age IS specified when adding this validation to fields.
    public class MinAgeAttribute : ValidationAttribute
    {
        // Constructs a new MinAge validation object with the specified
        // minimum age and a default message. When an instance is constructed,
        // it converts the minAge integer into a TimeSpan object.
        public MinAgeAttribute(int minAge)
            : base()
        {
            _MinAge = new TimeSpan(minAge * 365, 0, 0, 0);
            _Message = "Must be older than " + minAge.ToString();
        }

        // Constructs a new MinAge validation object with the specified
        // minimum age and a custom message.
        public MinAgeAttribute(int minAge, string message)
            : base()
        {
            _MinAge = new TimeSpan(minAge * 365, 0, 0, 0);
            _Message = message;
        }

        // The method that is called by ASP.NET when validating a submitted form
        // that has an input with this validation attribute. Age is determined
        // by constructing a TimeSpan object equal to the current date-time minus
        // the date-time from the form's input. This is compared to the _MinAge
        // member, and an error message with the _Message is generated if the
        // age is less than _MinAge.
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TimeSpan age = DateTime.Now - ((DateTime)value);

            if (age.CompareTo(_MinAge) <= 0)
            {
                return new ValidationResult(_Message);
            }

            return ValidationResult.Success;
        }

        private string _Message;
        private TimeSpan _MinAge;
    }
}