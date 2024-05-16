using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TennisProjekt24.Models
{
    public class Event
    {
        [Required(ErrorMessage = "Vælg dato")]
        [FutureDate(ErrorMessage = "Dato må ikke ligge før nuværende tidspunkt")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Vælg titel")]
        [StringLength(50, ErrorMessage = "Titel må max være 50 karakterer")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Udfyld Beskrivelse")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Vælg Lokation")]
        [StringLength(50, ErrorMessage = "Lokation må max være 50 karakterer")]
        public string Place { get; set; }
        public Member Member { get; set; }
        public int EventId { get; set; }
        public string? Image { get; set; }
        public class FutureDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var date = (DateTime)value;

                if (date.Date < DateTime.Now.Date)
                {
                    return new ValidationResult(ErrorMessage);
                }

                return ValidationResult.Success;
            }
        }

    public Event()
        {
            
        }

        public Event(int eventId, DateTime date, string title, string description, string place, Member member, string? image)
        {
            EventId = eventId;
            Date = date;
            Title = title;
            Description = description;
            Place = place;
            Member = member;
            Image = image;
        }
    }
}
