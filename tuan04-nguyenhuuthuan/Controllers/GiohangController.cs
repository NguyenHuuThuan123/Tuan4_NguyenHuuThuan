using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tuan04_nguyenhuuthuan.Models;
namespace tuan04_nguyenhuuthuan.Controllers
{
    public class GiohangController : Controller
    {
        MydataDataContext data = new MydataDataContext();
        public List<giohang> Laygiohang()
        {
            List<giohang> ListGiohang = Session["Giohang"] as List<giohang>;
            if (ListGiohang == null)
            {
                ListGiohang = new List<giohang>();
                Session["Giohang"] = ListGiohang;
            }
            return ListGiohang;
        }
        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<giohang> ListGiohang = Laygiohang();
            giohang sanpham = ListGiohang.Find(n => n.masach == id);
            if (sanpham == null)
            {
                sanpham = new giohang(id);
                ListGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<giohang> ListGiohang = Session["GioHang"] as List<giohang>;
            if (ListGiohang != null)
            {
                tsl = ListGiohang.Sum(n => n.iSoluong);
            }
            return tsl;
        }
        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<giohang> ListGiohang = Session["GioHang"] as List<giohang>;
            if (ListGiohang != null)
            {
                tsl = ListGiohang.Count;
            }
            return tsl;
        }
        private double TongTien()
        {
            double tt = 0;
            List<giohang> Listgiohang = Session["GioHang"] as List<giohang>;
            if (Listgiohang != null)
            {
                tt = Listgiohang.Sum(n => n.dThanhtien);
            }
            return tt;
        }
        public ActionResult GioHang()
        {
            List<giohang> ListGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(ListGiohang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }
        public ActionResult XoaGiohang(int id)
        {
            List<giohang> ListGiohang = Laygiohang();
            giohang sanpham = ListGiohang.SingleOrDefault(n => n.masach == id);
            if (sanpham != null)
            {
                ListGiohang.RemoveAll(n => n.masach == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<giohang> LstGiohang =Laygiohang();
            giohang sanpham = LstGiohang.SingleOrDefault(n => n.masach == id);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(collection["txtSolg"].ToString());
            }
            return RedirectToAction("GioHang");

        }
        public ActionResult XoaTatCaGioHang()
        {
            List < giohang > ListGiohang = Laygiohang();
            ListGiohang.Clear();
            return RedirectToAction("GioHang");
        }
    }
}