using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApi.Models
{
    public class Hall
    {
        public int Id { get; set; }

        public int CinemaId { get; set; }

        public string Name { get; set; }
        public int TotalRows { get; set; }
        public int SeatsPerRow { get; set; }


        public Cinema Cinema { get; set; }
        public List<Session> Sessions { get; set; } = new();
    }
}
