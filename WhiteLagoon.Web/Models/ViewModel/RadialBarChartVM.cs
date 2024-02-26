namespace WhiteLagoon.Web.Models.ViewModel
{
    public class RadialBarChartVM
    {
        public decimal TotalCount { get; set; }
        public decimal CountInCurrentMonth { get; set; }
        public decimal IncreaseDecreaseAmount { get; set; }

        public bool HasRatioIncrease { get; set; }

        public int[] Series { get; set; }
    }
}
