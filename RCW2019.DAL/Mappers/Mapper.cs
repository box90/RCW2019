using System;
using System.Collections.Generic;
using System.Text;
using RWC2019.Entities;

namespace RCW2019.DAL.Mappers
{
    internal static class Mapper
    {
        internal static Event ToEvent(this DTO.Event dto)
        {
            Event e = new Event();
            e.Min = dto.Min;
            e.Player = dto.Player;
            e.Team = dto.Team;
            e.Type = dto.Type;
            return e;
        }

        internal static int ActualMin(DateTime startDate, DateTime date)
        {
            if (startDate <= DateTime.MinValue || date <= DateTime.MinValue)
                return 0;

            var diffInSeconds = (date - startDate).TotalSeconds;

            if (diffInSeconds <= 0)
                return 0;

            return (int)Math.Floor(diffInSeconds * 2 / 60);
        }

        internal static bool IsPassedEvent(int min, DateTime startDate, DateTime date)
        {
            if (startDate <= DateTime.MinValue || date <= DateTime.MinValue)
                return false;

            var diffInSeconds = (date - startDate).TotalSeconds;
            
            if (diffInSeconds <= 0)
                return false;

            return min <= (diffInSeconds * 2 / 60);
        }
    }
}
