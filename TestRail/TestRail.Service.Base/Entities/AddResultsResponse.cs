namespace TestRail.Service.Base.Entities
{
    public class AddResultsResponse
    {
        public int assignedto_id { get; set; }
        public string comment { get; set; }
        public int created_by { get; set; }
        public int created_on { get; set; }
        public string defects { get; set; }
        public string elapsed { get; set; }
        public int id { get; set; }
        public int status_id { get; set; }
        public int test_id { get; set; }
        public string version { get; set; }
    }
}
