﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class HorseModel
    {
        public int ID { get; set; }
        //hvis feltet er tomt
        [Required(ErrorMessage = "Name is required.")]
        //Laver en længde på string 50
        [StringLength(50, ErrorMessage = "Name is too long.")]
        public string? Name { get; set; }
        //hvis feltet er tomt
        [Required(ErrorMessage = "Horse race is required.")]
        //Laver en længde på string 50
        [StringLength(50, ErrorMessage = "Horse race is too long.")]
        public string? HorseRace { get; set; }
        //Laver en range for Int 1 - max int value
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a SamuraiID. Cant be 0.")]
        public int SamuraiID { get; set; }
    }
}
