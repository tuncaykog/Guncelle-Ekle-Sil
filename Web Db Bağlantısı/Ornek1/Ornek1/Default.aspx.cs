using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;

namespace Ornek1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KayitlariGetir();
            }
        }

        protected void btnEkle_Click(object sender, EventArgs e)// Kullanıcıdan alınan bilgileri Customer tablosuna ekler.
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StokTakipDB"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO Customer (CustomerCode, CustomerName, City, Country, Addrees, Telephone) " +
                                 "VALUES (@CustomerCode, @CustomerName, @City, @Country, @Adress, @Telephone)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerCode", txtCustomerCode.Text);
                        cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
                        cmd.Parameters.AddWithValue("@City", txtCity.Text);
                        cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                        cmd.Parameters.AddWithValue("@Adress", txtAdress.Text);
                        cmd.Parameters.AddWithValue("@Telephone", txtTelephone.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                lblMesaj.ForeColor = System.Drawing.Color.Green;
                lblMesaj.Text = "Kayıt başarılı bir şekilde eklendi!";
                KayitlariGetir();
                Temizle();
            }
            catch (Exception)
            {
                lblMesaj.ForeColor = System.Drawing.Color.Red;
                lblMesaj.Text = "Hata oluştu: ";
            }
        }

        private void KayitlariGetir()// Customer tablosundaki kayıtları getirir ve GridView'de gösterir.
        {
            string cs = ConfigurationManager.ConnectionStrings["StokTakipDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string sql = @"SELECT CustomerCode, CustomerName, City, Country, Addrees, Telephone FROM Customer";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dbGridview.DataSource = dt;
                dbGridview.DataBind();
            }
        }

        protected void dbGridview_RowEditing(object sender, GridViewEditEventArgs e)// GridView'deki bir satırda düzenleme işlemi yapılmasını sağlar.
        {
            dbGridview.EditIndex = e.NewEditIndex;
            KayitlariGetir();
        }

        protected void dbGridview_RowUpdating(object sender, GridViewUpdateEventArgs e)// Seçilen kaydın güncellenmesini sağlar.
        {
            string code = dbGridview.DataKeys[e.RowIndex].Value.ToString();
            TextBox txtName = dbGridview.Rows[e.RowIndex].FindControl("txtCustomerName") as TextBox;
            TextBox txtCity = dbGridview.Rows[e.RowIndex].FindControl("txtCity") as TextBox;
            TextBox txtCountry = dbGridview.Rows[e.RowIndex].FindControl("txtCountry") as TextBox;
            TextBox txtAdress = dbGridview.Rows[e.RowIndex].FindControl("txtAdress") as TextBox;
            TextBox txtTelephone = dbGridview.Rows[e.RowIndex].FindControl("txtTelephone") as TextBox;

            
            if (txtName == null || txtCity == null || txtCountry == null || txtAdress == null || txtTelephone == null) // Alanların null olup olmadığını kontrol eder.
            {
                lblMesaj.ForeColor = Color.Red;
                lblMesaj.Text = "Güncelleme alanlarından biri bulunamadı!";
                return;
            }

            string name = txtName.Text;
            string city = txtCity.Text;
            string country = txtCountry.Text;
            string adress = txtAdress.Text;
            string telephone = txtTelephone.Text;

            string cs = ConfigurationManager.ConnectionStrings["StokTakipDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string sql = @"UPDATE Customer 
                       SET CustomerName=@Name, City=@City, Country=@Country, 
                           Addrees=@Adress, Telephone=@Telephone 
                       WHERE CustomerCode=@Code";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Parameters.AddWithValue("@Adress", adress);
                    cmd.Parameters.AddWithValue("@Telephone", telephone);
                    cmd.ExecuteNonQuery();
                }
            }

            dbGridview.EditIndex = -1;
            KayitlariGetir();
            lblMesaj.ForeColor = Color.Green;
            lblMesaj.Text = "Güncelleme başarıyla yapıldı.";
        }

        protected void dbGridview_RowDeleting(object sender, GridViewDeleteEventArgs e)// Seçilen kaydın silinmesini sağlar.
        {
            string code = dbGridview.DataKeys[e.RowIndex].Value.ToString();
            string cs = ConfigurationManager.ConnectionStrings["StokTakipDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string sql = "DELETE FROM Customer WHERE CustomerCode=@Code";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.ExecuteNonQuery();
                }
            }
            KayitlariGetir();
            lblMesaj.ForeColor = Color.Green;
            lblMesaj.Text = "Kayıt silindi.";
        }

        protected void dbGridview_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)// Düzenleme işleminden vazgeçilmesini sağlar.
        {
            dbGridview.EditIndex = -1;
            KayitlariGetir();
        }

        private void Temizle()// TextBox'ları temizler.
        {
            txtCustomerCode.Text = "";
            txtCustomerName.Text = "";
            txtCity.Text = "";
            txtCountry.Text = "";
            txtAdress.Text = "";
            txtTelephone.Text = "";
        }

    }
}