using System;
using System.Collections.Generic;
using System.Text;
using RWC2019.Entities;

namespace RCW2019.DAL.DTO
{
        internal class MatchDetail
        {
            public int Id { get; set; }
            public string Team1 { get; set; }
            public string Team2 { get; set; }
            public IEnumerable<Event> Events { get; set; }
            public DateTime StartTime { get; set; }
        }

        internal class Event
        {
            public int Min { get; set; }
            public int Team { get; set; }
            public string Player { get; set; }
            public string Type { get; set; }
    }
    
}
