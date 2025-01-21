using System.Data.OleDb;
using Microsoft.AspNetCore.Mvc;
using Punktik3.Models;

namespace Punktik3.Controllers
{
    public class WagesController : Controller
    {
        public IActionResult Index()
        {
            List<Wages> wages = GetWagesFromDatabase();
            return View(wages);
        }
        private List<Wages> GetWagesFromDatabase()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\DB\\DBASE;Extended Properties=dBASE III;";
            string query = "SELECT Name, Check_Date, Pay_Rate, Net_Pay, Fica FROM Wages;";
            List<Wages> wages = new List<Wages>();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Wages wage = new Wages
                    {
                        Name = reader.GetValue(0),
                        Check_Date = reader.GetValue(1),
                        Pay_Rate = reader.GetValue(2),
                        Net_Pay = reader.GetValue(3),
                        Fica= reader.GetValue(4)


                    };
                    wages.Add(wage);
                }
            }

            return wages;
        }
    }
}
