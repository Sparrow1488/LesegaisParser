using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using LesegaisParser.Entities;
using LesegaisParser.Intefraces;
using System.IO;
using System.Threading.Tasks;

namespace LesegaisParser
{
    public class RentForestAreaParser : ILesegaisParser<ReportWoodDeal>
    {
        public RentForestAreaParser()
        {
            Query = File.ReadAllText(@"C:\Users\Dom\Desktop\Репозитории\LesegaisParser\src\LesegaisParser\LesegaisParser\RequestInfo\QlQuery.txt");
        }

        public string Query { get; private set; }
        public string OperationName { get; private set; } = "SearchReportWoodDeal";
        public string EndPoint { get; private set; } = "https://www.lesegais.ru/open-area/graphql";
        public GraphQLRequest Request { get; set; }

        public async Task<int> GetTotalCountAsync()
        {
            var client = new GraphQLHttpClient(EndPoint, new NewtonsoftJsonSerializer());
            var request = CreateTotalSizeRequest(@"C:\Users\Dom\Desktop\Репозитории\LesegaisParser\src\LesegaisParser\LesegaisParser\RequestInfo\QlQueryTotal.txt");
            var response = await client.SendQueryAsync<Data<SearchReportWoodDeal>>(request);
            var totalSize = response.Data.SearchReportWoodDeal.Total;
            return totalSize;
        }

        public async Task<Data<ReportWoodDeal>> ParseAsync(int count, int page)
        {
            Request = CreateDataRequest(count, page);

            var client = new GraphQLHttpClient(EndPoint, new NewtonsoftJsonSerializer());
            var response = await client.SendQueryAsync<Data<ReportWoodDeal>>(Request);
            return response.Data;
        }

        private GraphQLRequest CreateDataRequest(int parseCount, int page)
        {
            return new GraphQLRequest(query: Query, operationName: OperationName, variables: new
            {
                number = page,
                size = parseCount
            });
        }

        private GraphQLRequest CreateTotalSizeRequest(string txtFileWithQueryPath)
        {
            var query = File.ReadAllText(txtFileWithQueryPath);
            return new GraphQLRequest(query: query, operationName: "SearchReportWoodDealCount", variables: new
            {
                size = 500,
                number = 0
            });
        }
    }
}
