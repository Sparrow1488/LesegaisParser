using Kernel.Layer;
using LesegaisParser.Entities;
using LesegaisParser.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LesegaisParser
{
    internal class WoodDealsParser
    {
        public GraphQlRequest Request { get; private set; }
        public readonly string Query = "query SearchReportWoodDeal($size: Int!, $number: Int!, $filter: Filter, $orders: [Order!])\n {  searchReportWoodDeal(filter: $filter, pageable: \n{ number: $number,\nsize: $size},\norders: $orders)\n{\ncontent\n{\nsellerName\nsellerInn\nbuyerName\nbuyerInn\nwoodVolumeBuyer\nwoodVolumeSeller\ndealDate\ndealNumber\n __typename\n\n}\n__typename\n \n}\n}";
        public int ParseItemsCount { get; set; } = 2;
        public int ParseCurrentPage { get; set; } = 0;
        public readonly string EndPoint = @"https://www.lesegais.ru/open-area/graphql";

        public async Task<Woodreportdeals[]> ParseAsync()
        {
            var jsonResponse = await SendGraphQlRequestAsync();
            var container = JsonConvert.DeserializeObject<DataContainer>(jsonResponse);
            return container?.Data?.SearchReportWoodDeal?.Content ?? new Woodreportdeals[0];
        }

        public async Task<string> SendGraphQlRequestAsync()
        {
            var graphQlRequest = new GraphQlRequest
            {
                query = Query,
                operationName = "SearchReportWoodDeal",
                variables = new { size = ParseItemsCount, number = ParseCurrentPage }
            };
            var jsonBody = JsonConvert.SerializeObject(graphQlRequest);

            byte[] dataBytes = Encoding.UTF8.GetBytes(jsonBody);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(EndPoint);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (Stream requestBody = request.GetRequestStream())
                await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return await reader.ReadToEndAsync();
        }
    }
}
