using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Application.Models.BoardGameGeek;
using System.Xml.Serialization;

namespace Borkfolio.Infrastructure.Services.BoardGameGeek
{
    public class BoardGameGeekService : IBoardGameGeekService
    {
        private readonly HttpClient _httpClient;

        public BoardGameGeekService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BoardGameGeek");
        }

        public async Task<BggGameDetailsItem> GetBoardGameDetails(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"thing?id={id}&type=boardgame,boardgameexpansion");

            var xml = await httpResponseMessage.Content.ReadAsStringAsync();

            BggGameDetailsSet? result;
            XmlSerializer serializer = new XmlSerializer(typeof(BggGameDetailsSet));

            using (StringReader reader = new StringReader(xml))
            {
                result = (BggGameDetailsSet?)serializer.Deserialize(reader);
            }

            if (result?.Items?[0] == null)
            {
                throw new Exception("TODO: update this null collection exception");
            }

            return result.Items[0];
        }

        public async Task<List<BggCollectionItem>> GetMyCollection()
        {
            var httpResponseMessage = await _httpClient.GetAsync("collection?username=borkmeister&subtype=boardgame&own=1");

            var xml = await httpResponseMessage.Content.ReadAsStringAsync();

            BggCollectionSet? result;
            XmlSerializer serializer = new XmlSerializer(typeof(BggCollectionSet));

            using (StringReader reader = new StringReader(xml))
            {
                result = (BggCollectionSet?)serializer.Deserialize(reader);
            }

            if (result == null)
            {
                throw new Exception("TODO: update this null collection exception");
            }

            return result.Items;
        }

        public async Task<List<BggSearchResultItem>> SearchBoardGames(string name)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"search?query={name}&type=boardgame");

            var xml = await httpResponseMessage.Content.ReadAsStringAsync();

            BggSearchResultSet? result;
            XmlSerializer serializer = new XmlSerializer(typeof(BggSearchResultSet));

            using (StringReader reader = new StringReader(xml))
            {
                result = (BggSearchResultSet?)serializer.Deserialize(reader);
            }

            return result?.Items ?? new List<BggSearchResultItem>();
        }
    }
}
