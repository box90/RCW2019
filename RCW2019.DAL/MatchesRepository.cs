using Newtonsoft.Json;
using RWC2019.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RCW2019.DAL.Mappers;
using Newtonsoft.Json.Converters;

namespace RCW2019.DAL
{
    public class MatchesRepository
    {
        private HttpClient client;

        public MatchesRepository()
        {
            client = HttpClientFactory.Create();
        }

        /// <summary>
        /// iniza il match
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> StartMatch(int id)
        {
            var match = await GetMatch(id, true);
            match.StartTime = DateTime.Now;

            //post
            return await ExecuteCall<bool, MatchDetail>(
                HttpMethod.Put,
                $"http://localhost:3000/matches/{id}/",
                (output) => true,
                JsonConvert.SerializeObject(match));
        }

        /// <summary>
        /// ritorna l'elenco delle partite con il risultato al minuto attuale
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Match>> GetResults()
        {
            var now = DateTime.Now;

            //elenco dei match con risultato al minuto attuale
            return await ExecuteCall<List<Match>, IEnumerable<DTO.MatchDetail>>(
                HttpMethod.Get,
                $"http://localhost:3000/matches/",
                (dto) =>
                {
                    return dto?.Select(dtoMatch =>
                    {
                        var events = dtoMatch?.Events?.Where(ev => { return Mapper.IsPassedEvent(ev.Min, dtoMatch.StartTime, now); }).Select(ev => ev.ToEvent());

                        return new Match()
                        {
                            Id = dtoMatch.Id,
                            ActualMin = Mapper.ActualMin(dtoMatch.StartTime, now),
                            StartTime = dtoMatch.StartTime,
                            Team1 = dtoMatch.Team1,
                            Team2 = dtoMatch.Team2,
                            Team1Points = events.Where(ev => ev.Team == 1).Sum(ev => ev.Points),
                            Team2Points = events.Where(ev => ev.Team == 2).Sum(ev => ev.Points)
                        };
                    }).ToList();
                });
        }

        /// <summary>
        /// Ritorna i dati di un singolo match
        /// </summary>
        /// <param name="id">Id del match</param>
        /// <param name="allEvents">Se true torna tutti gli eventi come se la partita fosse finita</param>
        /// <returns></returns>
        public async Task<MatchDetail> GetMatch(int id, bool allEvents)
        {
            var now = DateTime.Now;
            //singolo match con eventi fino al minuto attuale
            return await ExecuteCall<MatchDetail, DTO.MatchDetail>(
                HttpMethod.Get,
                $"http://localhost:3000/matches/{id}",
                (dto) =>
                {
                    var det = new MatchDetail()
                    {
                        Id = dto.Id,
                        StartTime = dto.StartTime,
                        Team1 = dto.Team1,
                        Team2 = dto.Team2,
                        ActualMin = Mapper.ActualMin(dto.StartTime, now),
                        Events = dto.Events?.
                                    Where(ev => { return allEvents || Mapper.IsPassedEvent(ev.Min, dto.StartTime, now); }).
                                    Select(ev => ev.ToEvent()),
                    };
                    det.Team1Points = det.Events.Where(ev => ev.Team == 1).Sum(ev => ev.Points);
                    det.Team2Points = det.Events.Where(ev => ev.Team == 2).Sum(ev => ev.Points);
                    return det;
                });
        }

        private async Task<T> ExecuteCall<T, M>(
            HttpMethod method,
            string url,
            Func<M, T> outputMapper,
            string content = null,
            [CallerMemberName]string callerName = "") where T : new()
        {
            try
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(method, new Uri(url));
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (content != null)
                    requestMessage.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
                string responseString = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    return outputMapper(JsonConvert.DeserializeObject<M>(responseString));
                }
                else
                {
                    throw new Exception($"Error in response {callerName} Error {responseString}");
                }
            }
            catch (Exception exc)
            {
                throw new Exception($"Exception executing {callerName} Exception {exc.ToString()}", exc);
            }
        }
    }
}
