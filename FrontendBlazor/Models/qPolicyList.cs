namespace FrontendBlazor.Models
{
    public class qPolicyList
    {

        public List<ScrapQpolicyViewModel>? policyList { get; set; } = null;

        public bool? avaible { get; set; } = null;

        public List<PolicyTypeViewModel>? types {get;  set; } = null;

        public List<PolicyStateViewModel>? estados { get; set; } = null;


    }
}
