using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_EntityFrameworkStatistics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Db3Project20Entities db = new Db3Project20Entities();
        private void Form1_Load(object sender, EventArgs e)
        {
            //Toplam kategori sayısı
            int countCategoryCount = db.TblCategory.Count();
            label2.Text = countCategoryCount.ToString();
            //Toplam ürün sayısı
            int countProduct = db.TblProduct.Count();
            label3.Text = countProduct.ToString();
            //Toplam müşteri sayısı
            int countCustomer = db.TblCustomer.Count();
            label5.Text = countCustomer.ToString();
            //Toplam sipariş sayısı
            int countOrder = db.TblOrder.Count();
            label7.Text = countOrder.ToString();
            //Toplam stok sayısı
            var countStock = db.TblProduct.Sum(x => x.ProductStock);
            label9.Text = countStock.ToString();
            //Ortalama ürün fiyatı
            var avarageProductprice = db.TblProduct.Average(x => x.ProductPrice);
            label19.Text = avarageProductprice.ToString() + "₺";
            //Toplam Meyve Stoğu
            var totalProductCountByCategoryIsFruit = db.TblProduct.Where(x => x.CategoryId == 1).Sum(y => y.ProductStock);
            label17.Text = totalProductCountByCategoryIsFruit.ToString();
            //Toplam salep işlem hacmi
            var totalProductcountBycategoryIsSalepGetStock = db.TblProduct.Where(a => a.ProductName == "Salep")
                                                                          .Select(y => y.ProductStock).FirstOrDefault();
            var totalProductcountBycategoryIsSalepUnitPrice = db.TblProduct.Where(x => x.ProductName == "Salep")
                                                                           .Select(y => y.ProductPrice).FirstOrDefault();
            var totalProductcountBycategory = totalProductcountBycategoryIsSalepGetStock * totalProductcountBycategoryIsSalepUnitPrice;
            label15.Text = totalProductcountBycategory.ToString() + "₺";
            //Stok sayısı 100den az olan ürünler
            var productCountBystockcountthansmallerHundered = db.TblProduct.Where(x => x.ProductPrice < 100).Count();
            label13.Text = productCountBystockcountthansmallerHundered.ToString();
            //Aktif Sebze stoğu
            //int id=db.TblCategory.Where(z=>z.CategoryName == "Sebze").Select(y=>y.CategoryId).FirstOrDefault();
            var productStockcountByCategorynameisSebzeStatusIsTrue = db.TblProduct.Where
                (x => x.CategoryId == (db.TblCategory.Where(z => z.CategoryName == "Sebze").
                Select(y => y.CategoryId).FirstOrDefault()) && x.ProductStatus == true).Sum(y => y.ProductStock);
            label11.Text = productStockcountByCategorynameisSebzeStatusIsTrue.ToString();
            //Türkiyeden Yapılan Siparişler(SQL)
            int orderCountfromTurkey = db.Database.SqlQuery<int>("Select count(*) from TblOrder where CustomerId In " +
                "(Select CustomerId from TblCustomer where CustomerCountry='Türkiye')").FirstOrDefault();
            label29.Text = orderCountfromTurkey.ToString();
            //Türkiyeden Yapılan Siparişler(EF)
            var turkishCustomerid=db.TblCustomer.Where(x=>x.CustomerCountry=="Türkiye")
                                                .Select(y => y.CustomerId).ToList();
            var orderCountfromturkeywithEf = db.TblOrder.Count(z => turkishCustomerid.Contains(z.CustomerId.Value));
            label27.Text = orderCountfromturkeywithEf.ToString();
            //Kategorisi Meyve olan ürünlerin toplam satış fiyatı

        }
    }
}
