using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CinemaApi.Models;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace CinemaApi.Models
{
   public class Movie
    {
        public int Id { get; set; }

        [Required, MaxLength(300)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Genre { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int DurationMinutes { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(1000)]
        public string PosterUrl { get; set; } = string.Empty;

        public List<Session> Sessions { get; set; } = new();
    }
}
