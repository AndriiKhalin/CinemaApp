using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace CinemaApi.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public string PosterUrl { get; set; }


        public List<Session> Sessions { get; set; } = new();
    }
}
