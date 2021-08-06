using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tt.ViewModels
{
    public class BarChartVM
    {
        public List<string> labels { get; set; }
        public List<BarChartChildVM> datasets { get; set; }

        public BarChartVM()
        {
            // Cac nhan truc hoanh (truc x)
            labels = new List<string>();
            // DATA MOI DUONG CUA BIET DO
            // MOT ChildVM la mot duong
            datasets = new List<BarChartChildVM>();
        }

        public class BarChartChildVM
        {
            public string label { get; set; }
            public string backgroundColor { get; set; }
            public string borderColor { get; set; }
            public int borderWidth { get; set; }
            public bool fill { get; set; }

            public double lineTension { get; set; }
            public int radius { get; set; }
            public List<int> data { get; set; }
            public BarChartChildVM()
            {
                data = new List<int>();
            }
        }
    }
}