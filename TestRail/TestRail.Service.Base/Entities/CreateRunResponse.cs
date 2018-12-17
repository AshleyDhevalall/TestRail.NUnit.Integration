using System.Collections.Generic;

namespace TestRail.Service.Base.Entities
{
    public class CreateRunResponse
    {
        public int id { get; set; }
        public int suite_id { get; set; }
        public string name { get; set; }
        public object description { get; set; }
        public object milestone_id { get; set; }
        public object assignedto_id { get; set; }
        public bool include_all { get; set; }
        public bool is_completed { get; set; }
        public object completed_on { get; set; }
        public object config { get; set; }
        public List<object> config_ids { get; set; }
        public int passed_count { get; set; }
        public int blocked_count { get; set; }
        public int untested_count { get; set; }
        public int retest_count { get; set; }
        public int failed_count { get; set; }
        public int custom_status1_count { get; set; }
        public int custom_status2_count { get; set; }
        public int custom_status3_count { get; set; }
        public int custom_status4_count { get; set; }
        public int custom_status5_count { get; set; }
        public int custom_status6_count { get; set; }
        public int custom_status7_count { get; set; }
        public int project_id { get; set; }
        public object plan_id { get; set; }
        public int created_on { get; set; }
        public int created_by { get; set; }
        public string url { get; set; }
    }
}
