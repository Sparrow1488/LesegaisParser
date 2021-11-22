namespace LesegaisParser.Requests
{
    internal class GraphQlRequest
    {
        public string query { get; set; }
        public string operationName { get; set; }
        public object variables { get; set; }
    }
}
