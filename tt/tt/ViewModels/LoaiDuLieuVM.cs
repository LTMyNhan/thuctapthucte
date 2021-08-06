using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tt.Models;

namespace tt.ViewModels
{
    public class LoaiDuLieuVM
    {
        public string selected { get; set; }
        public Dictionary<string, string> loaiDuLieus { get; set; }
    }
}