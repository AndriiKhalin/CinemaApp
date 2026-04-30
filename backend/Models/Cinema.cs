using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApi.Models
{
    public class Cinema
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        public List<Hall> Halls { get; set; } = new();
    }
}
