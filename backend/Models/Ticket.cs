using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApi.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }

        public int Row { get; set; }
        public int SeatNumber { get; set; }

        public bool IsPaid { get; set; }


        public Session Session { get; set; }
    }
}
