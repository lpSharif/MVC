using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    public class CustomerController : Controller
    {
        string connectionString = @"Data Source=LpSharif;Initial Catalog=Customer;Integrated Security=True";
        public int id = -1;

        // GET: CustomerController
        [HttpGet]
        public ActionResult Index()
        {
            DataTable tbl = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter data = new SqlDataAdapter("select * from Customer", con);
                data.Fill(tbl);
            }
                return View(tbl);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new CustomerModel());
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerModel customer)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("insert into Customer values (@firstName, @lastName,@age,@city)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@firstName", customer.firstName);
                    cmd.Parameters.AddWithValue("@lastName", customer.lastName);
                    cmd.Parameters.AddWithValue("@age", customer.age);
                    cmd.Parameters.AddWithValue("@city", customer.city);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            CustomerModel customer = new CustomerModel();
            DataTable dtbl = new DataTable();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter data = new SqlDataAdapter("Select * from Customer where customerId = @ID", con);
                data.SelectCommand.Parameters.AddWithValue("@ID", id);
                data.Fill(dtbl);
            }
            if (dtbl.Rows.Count == 1)
            {
                customer.customerId = Convert.ToInt32(dtbl.Rows[0][0].ToString());
                customer.firstName = dtbl.Rows[0][1].ToString();
                customer.lastName = dtbl.Rows[0][2].ToString();
                customer.age = dtbl.Rows[0][3].ToString();
                customer.city = dtbl.Rows[0][4].ToString();
                return View(customer);
            }
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerModel customer)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("update Customer set firstName=@firstname, lastName=@lastName, age=@age, city=@city where customerId = @id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", customer.customerId);
                    cmd.Parameters.AddWithValue("@firstName", customer.firstName);
                    cmd.Parameters.AddWithValue("@lastName", customer.lastName);
                    cmd.Parameters.AddWithValue("@age", customer.age);
                    cmd.Parameters.AddWithValue("@city", customer.city);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("delete from Customer where customerId = @id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
                return RedirectToAction("Index");
        }

        
    }
}
