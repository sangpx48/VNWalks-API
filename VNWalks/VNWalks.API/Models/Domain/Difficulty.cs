﻿using System.ComponentModel.DataAnnotations;

namespace VNWalks.API.Models.Domain
{
    public class Difficulty
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }


    }
}
