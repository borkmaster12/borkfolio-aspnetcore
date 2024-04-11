using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Application.Models.BoardGameGeek;
using System.Xml.Serialization;

namespace Borkfolio.Infrastructure.BoardGameGeek
{
    public class BoardGameGeekService : IBoardGameGeekService
    {
        private readonly HttpClient httpClient;

        public BoardGameGeekService(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("BoardGameGeek");
        }

        public Task<BggBoardGameDetails> GetBoardGameDetails(int it)
        {
            throw new NotImplementedException();
        }

        public async Task<BggCollection> GetMyCollection()
        {
            var httpResponseMessage = await httpClient.GetAsync("collection?username=borkmeister&subtype=boardgame&own=1");

            var xml = await httpResponseMessage.Content.ReadAsStringAsync();

            BggCollection? result;
            XmlSerializer serializer = new XmlSerializer(typeof(BggCollection));

            using (StringReader reader = new StringReader(xml))
            {
                result = (BggCollection?)serializer.Deserialize(reader);
            }

            if (result == null)
            {
                throw new Exception("TODO: update this null collection exception");
            }

            return result;
        }

        public Task<BggSearchResultSet> SearchBoardGames(string name)
        {
            throw new NotImplementedException();
        }
    }
}
