using Punktik3.Models;
using Microsoft.AspNetCore.Mvc;
using Punktik3.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Microsoft.DotNet.Scaffolding.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dbase3Viewer.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            List<Employee> employees = GetEmployeesFromDatabase();
            return View(employees);
        }
        private List<Employee> GetEmployeesFromDatabase()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\DB\\DBASE;Extended Properties=dBASE III;";
            string query = "SELECT LastName, Phone, Departure, Cost, TravelPlan FROM Travel;";
            List<Employee> employees = new List<Employee>();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        LASTNAME = reader.GetString(0),
                        PHONE = reader.GetString(1),
                        DEPARTURE = DateOnly.FromDateTime(reader.GetDateTime(2)),
                        Cost=reader.GetValue(3),
                        TravelPlan=reader.GetString(4)


                    };
                    employees.Add(employee);
                }
            }

            return employees;
        }
    }
}
