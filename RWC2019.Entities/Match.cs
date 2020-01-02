using System;
using System.Collections.Generic;

namespace RWC2019.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        /// <summary>
        /// punteggio attuale squadra 1
        /// </summary>
        public int Team1Points { get; set; }
        /// <summary>
        /// punteggio attuale squadra 2
        /// </summary>
        public int Team2Points { get; set; }
        public DateTime StartTime { get; set; }
        public int ActualMin { get; set; }
    }

    public class MatchDetail : Match
    {        
        public IEnumerable<Event> Events { get; set; }
    }

    public class Event
    {
        public int Min { get; set; }
        public int Team { get; set; }
        public string Player { get; set; }
        public string Type { get; set; }
        /// <summary>
        /// Punti realizzati con questo evento
        /// </summary>
        public int Points { 
            get
            {
                int points = 0;
                switch (Type)
                {
                    case "Try": points = 5; break;
                    case "Conv": points = 2; break;
                    case "Pen": points = 3; break;
                    default: break;
                }
                return points;
            }
        }
    }
}
