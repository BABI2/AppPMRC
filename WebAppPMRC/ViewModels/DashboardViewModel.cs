namespace WebAppPMRC.ViewModels
{
    public class DashboardViewModel
    {
        public int PersonCount { get; set; }
        public decimal TotalAmount { get; set; }
        public int LocalityCount { get; set; }
        public List<string> Localities { get; set; }
        public List<decimal> Amounts { get; set; }
        public List<string> PersonNames { get; set; }
    }
}
