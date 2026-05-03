using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Text;

namespace CinemaApi.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public int HallId { get; set; }

        public DateTime StartTime { get; set; }

        [Range(0, 100000)]
        public decimal TicketPrice { get; set; }

        public Movie? Movie { get; set; }
        public Hall? Hall { get; set; }

        public List<Ticket> Tickets { get; set; } = new();
    }
}
