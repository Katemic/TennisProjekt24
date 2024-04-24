﻿using System.ComponentModel.DataAnnotations;

namespace TennisProjekt24.Models
{
    public enum CourtTypeEnum
    {
        Padel, Tennis, PickleBall
    }

    public class Court
    {

        public int CourtId { get; set; }
        public bool Outdoor { get; set; }
        [Required(ErrorMessage = "Bane nummer er påkrævet")]
        public int CourtNumber { get; set; }
        [Required(ErrorMessage = "Bane type er påkrævet")]
        public CourtTypeEnum CourtType { get; set; }
        public bool Availability { get; set; }


        public Court()
        {
            
        }

        public Court(int courtId, bool outdoor, int courtNumber, CourtTypeEnum courtType, bool availability)
        {
            CourtId = courtId;
            Outdoor = outdoor;
            CourtNumber = courtNumber;
            CourtType = courtType;
            Availability = availability;
        }

  

    }
}
