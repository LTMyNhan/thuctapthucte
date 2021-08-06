using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tt.Models;
using tt.ViewModels;
using static tt.ViewModels.BarChartVM;
using Oracle.ManagedDataAccess.Client;

namespace tt.Controllers
{
    public class DashBoardController : Controller
    {
        // Để cho người dùng thay đổi kiểu thông tin Biều Đồ
        static LoaiDuLieuVM loaiDuLieuVM = new LoaiDuLieuVM();
        //
        static TTVTVM ttvtVM = new TTVTVM();
        // Connection String (trong web.config)
        string oradb = ConfigurationManager.ConnectionStrings["ODB"].ToString();
        // View Biểu Đồ
        public ActionResult DashBoardByDataType()
        {
            //Master Model chứa các loại dữ liệu và Toàn bộ dữ liệu để vẽ biểu đồ
            var masterModel = new DashBoardByDataTypeVM();
            var barChartData = GetBarChartData();
            masterModel.BarChartData = barChartData;
            masterModel.LoaiDuLieu = loaiDuLieuVM;
            /*masterModel.TTVT = ttvtVM;*/
            return View(masterModel);
        }

        public ActionResult DashBoardByTTVT()
        {
            var masterModel = new DashBoardByDataTypeVM();
            var barChartData = GetBarChartData();
            masterModel.BarChartData = barChartData;
            masterModel.LoaiDuLieu = loaiDuLieuVM;
            masterModel.TTVT = ttvtVM;
            return View(masterModel);
        }
        // Cung cấp các thông tin cần thiết để vẽ biểu đồ
        private BarChartVM GetBarChartData()
        {
            OracleConnection connection = new OracleConnection(oradb);
            getLoaiDuLieu();
            var colors = ColorsForChart();
            var barChartData = new BarChartVM();
            var labels = getLabelsData(connection);
            var datasets = new List<BarChartChildVM>();
            // select * from THONGKE where MATKT= order by THOIGIAN
            var tkts = getTKTs(connection);
            var i = 0;
            foreach (TKT t in tkts)
            {
                var childModel = new BarChartChildVM();
                childModel.label = t.tenTKT;
                childModel.borderWidth = 3;
                childModel.backgroundColor = colors[i]/*@"rgb(49,130,189)"*/;
                childModel.borderColor = colors[i]/*@"rgb(49,130,189)"*/;
                childModel.fill = false;
                childModel.lineTension = 0.4;
                childModel.radius = 6;
                var dataList = getDataList(connection, t.maTKT);
                childModel.data = dataList;
                datasets.Add(childModel);
                i++;
            }
            //
            barChartData.datasets = datasets;
            barChartData.labels = labels;
            return barChartData;
        }

        
        // Bảng màu cho các đường của biểu đồ
        private string[] ColorsForChart()
        {    
            var Colors = new [] { "#FB3640", "#605F5E", "#1D3461", "#1F487E", "#247BA0", "#241023",
            "#6B0504", "#A3320B", "#D5E68D","#47A025","#0A2E36","#27FB6B","#036D19","#F9DBBD","#FCA17D",
            "#DA627D", "#9A348E", "#0D0628", "08415C", "CC2936", "6B818C"};
            return Colors;
        }
        // Các kiểu dữ liệu Biểu Đồ
        private void getLoaiDuLieu()
        {
            loaiDuLieuVM.loaiDuLieus = (new Dictionary<string, string>(){
                { "SoLuongTBPTM", "Số lượng thuê bao mới phát triển" },
                { "SoLuongTBML", "Số lượng thuê bao mở lại"},
                { "SoLuongTBNH", "Số lượng thuê bao ngưng huỷ" },
                { "TiLeGiamTBH", "Tỷ lệ giảm thuê bao huỷ" }
            });
            if (loaiDuLieuVM.selected == null)
                loaiDuLieuVM.selected = loaiDuLieuVM.loaiDuLieus.FirstOrDefault().Key;
        }
        // Hàm trả ra các nhãn ở dưới biểu đồ
        private List<string> getLabelsData(OracleConnection connection)
        {
            var labels = new List<string>();
            string queryString = "select distinct ltrim(TO_CHAR(ThangNam,'MM-YYYY'),'0') " +
                                 "as ThangNam from SoLieu order by ThangNam";
            OracleCommand command = new OracleCommand(queryString, connection);
            connection.Open();
            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                labels.Add(reader[columnName: "ThangNam"].ToString());
            }
            connection.Close();
            return labels;
        }
        // Hàm trả ra danh sách TTVT
        private List<TTVT> getTTVTs(OracleConnection connection)
        {
            var trungTam = new List<TTVT>();
            string queryString = "select * from TTVT";
            OracleCommand command = new OracleCommand(queryString, connection);
            connection.Open();
            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var t = new TTVT();
                t.maTTVT = reader["MATTVT"].ToString();
                t.tenTTVT = reader["TENTTVT"].ToString();
                trungTam.Add(t);
            }
            connection.Close();
            return trungTam;
        }
        // Hàm trả ra danh sách các tổ kỹ thuật
        private List<TKT> getTKTs(OracleConnection connection)
        {
            var tokythuats = new List<TKT>();
            string queryString = "select * from TKT";
            OracleCommand command = new OracleCommand(queryString, connection);
            connection.Open();
            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var t = new TKT();
                t.maTKT = reader["MATKT"].ToString();
                t.tenTKT = reader["TENTKT"].ToString();
                tokythuats.Add(t);
            }
            connection.Close();
            return tokythuats;
        }
        private List<TKT> getTKTs(OracleConnection connection, string maTTVT)
        {
            var tokythuats = new List<TKT>();
            string queryString = "select * from TKT where substr(MATKT,5,1) ="+ Convert.ToInt32(maTTVT.Substring(5,1));
            OracleCommand command = new OracleCommand(queryString, connection);
            connection.Open();
            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var t = new TKT();
                t.maTKT = reader["MATKT"].ToString();
                t.tenTKT = reader["TENTKT"].ToString();
                tokythuats.Add(t);
            }
            connection.Close();
            return tokythuats;
        }
        // Hàm để lấy thông số các điểm trên biểu đồ
        private List<int> getDataList(OracleConnection connection, string maTKT)
        {
            var dataList = new List<int>();
            string queryString = "select * from SoLieu where MATKT = '" + maTKT + "' order by ThangNam";
            OracleCommand command = new OracleCommand(queryString, connection);
            connection.Open();
            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int i = Convert.ToInt32(reader[loaiDuLieuVM.selected]);
                dataList.Add(i);
            }
            connection.Close();
            return dataList;
        }
        // Hàm phía dưới [HttpPost] sẽ trực tiếp xử lý thông tin kiểu POST nhận vào từ người dùng
        [HttpPost]
        // Hàm để thay đổi kiểu dữ liệu hiển thị trên Biểu Đồ
        public ActionResult HandleRequest (FormCollection formCollection)
        {   
            // Trích kiểu dữ liệu mà người dùng đã yêu cầu
            string loaiDLMoi = formCollection["loaiDL"].ToString();
            //string ttvtMoi = formCollection["ttvt"].ToString();
            // Gán vào selected của loaiDuLieuVM
            loaiDuLieuVM.selected = loaiDLMoi;
            //ttvtVM.selected = ttvtMoi;
            // Redirect lại để thực hiện thay đổi
            return RedirectToAction("DashBoardByDataType");
        }




        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////
        /// </summary>
        // GET: TTVT
        List<TTVT> ttvts = new List<TTVT>();
        List<TKT> tkts = new List<TKT>();
        // 
       
        // Read Data From The Database
        public void ReadData(string oradb)
        {    
            string queryString = "select * from TTVT";
            try
            {
                using (OracleConnection connection = new OracleConnection(oradb))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    connection.Open();
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        // Always call Read before accessing data.
                        while (reader.Read())
                        {
                            ttvts.Add(new TTVT() { tenTTVT = reader["tenTTVT"].ToString(),
                                                   maTTVT = reader["maTTVT"].ToString()});
                        }
                    }
                }
            } 
            catch ( Exception e) { throw e; }
        }

        
    }
}