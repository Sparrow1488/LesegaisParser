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
            Query = "query SearchReportWoodDeal($size: Int!, $number: Int!, $filter: Filter, $orders: [Order!])\n {  searchReportWoodDeal(filter: $filter, pageable: \n    {         number: $number,\n         size: $size},\n         orders: $orders)\n         {\n            content\n             {\n                sellerName\n                sellerInn\n                buyerName\n                buyerInn\n                woodVolumeBuyer\n                woodVolumeSeller\n                dealDate\n                dealNumber\n                __typename\n             \n}\n          __typename\n        \n}\n    }";
        }

        public string Query { get; private set; }
        public string OperationName { get; private set; } = "SearchReportWoodDeal";
        public string EndPoint { get; private set; } = "https://www.lesegais.ru/open-area/graphql";
        public GraphQLRequest Request { get; set; }

        public async Task<int> GetTotalCountAsync()
        {
            var client = new GraphQLHttpClient(EndPoint, new NewtonsoftJsonSerializer());
            var request = CreateTotalSizeRequest();
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

        private GraphQLRequest CreateTotalSizeRequest()
        {
            var query = "query SearchReportWoodDealCount($size: Int!, $number: Int!, $filter: Filter, $orders: [Order!])\n {\n    searchReportWoodDeal(filter: $filter, pageable:\n     {\n        number: $number, size: $size    \n},\n     orders: $orders)\n         {\n            total\n           number\n            size\n            overallSellerVolume\n            __typename\n        \n}\n    }";
            return new GraphQLRequest(query: query, operationName: "SearchReportWoodDealCount", variables: new
            {
                size = 500,
                number = 0
            });
        }
    }
}
