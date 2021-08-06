using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tt.ViewModels
{
    public class DashBoardByDataTypeVM
    {   
        public TTVTVM TTVT { get; set; }
        public LoaiDuLieuVM LoaiDuLieu { get; set; }
        public BarChartVM BarChartData { get; set; }
    }
}