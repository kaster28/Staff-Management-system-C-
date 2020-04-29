using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Windows;
using HRIS.Control;
using HRIS.Teaching;

namespace HRIS.Database
{
    class SchoolDBAAdapter
    {
        //Connect with database
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";
        private static bool ErrorOccurred = false;

        private static MySqlConnection conn = null;

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
        //Make connection to DB
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }
        //get all staff info from DB
        public static List<Staff> FetchAllStaff()
        {
            List<Staff> staff = new List<Staff>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(" SELECT id, given_name, family_name, title, campus, phone, room, email, photo, category from staff", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    
                    staff.Add(new Staff
                    {
                        ID = rdr.GetInt32(0),
                        Given_name = rdr.GetString(1),
                        Family_name = rdr.GetString(2),
                        Title = rdr.GetString(3),
                        Campus = ParseEnum<Campus>(rdr.GetString(4)),
                        Phone = rdr.GetString(5),
                        Room = rdr.GetString(6),
                        Email = rdr.GetString(7),
                        Photo = rdr.GetString(8),
                        Category = ParseEnum<Category>(rdr.GetString(9))

                    });
                }
            }

            catch (MySqlException e)
            //This is analogous to the judgment of "if" "else" 
            {
                Error("Error accessing all staff data", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return staff;
        }

        //get all unit info from DB
        public static List<Unit> FetchAllUnit()
        {
            List<Unit> unit = new List<Unit>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT code, title, coordinator from unit", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    unit.Add(new Unit
                    {
                        Code = rdr.GetString(0),
                        Title = rdr.GetString(1),
                        Coordinator = rdr.GetString(2),
                    });
                }
            }

            catch (MySqlException e)
            //This is analogous to the judgment of "if" "else" 
            {
                Error("Error accessing all unit data", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return unit;
        }

        //Loading data from Consultation by ID
        public static List<Event> FetchEvent(int id)
        {
            List<Event> eve = new List<Event>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT day, start, end from consultation where staff_id=?id", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    eve.Add(new Event
                    {
                        Day = rdr.GetString(0),
                        Start = rdr.GetTimeSpan(1),
                        End = rdr.GetTimeSpan(2)                
                    });
                }
            }
            catch (MySqlException e)
            {
                Error("Error fetching from consultation", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return eve;
        }

        //Loadint data from unit by id
        public static List<Unit> FetchUnitByID(int id)
        {
            List<Unit> TeachUnit = new List<Unit>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT code, title from unit where coordinator=?id", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TeachUnit.Add(new Unit
                    {
                        Code = rdr.GetString(0),
                        Title = rdr.GetString(1),
                    });
                }
            }
            catch (MySqlException e)
            {
                Error("Error fetching from unit", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return TeachUnit;
        }

        //Loading data from class by code
        public static List<UnitClass> FetchClassByCode(string code)
        {
            List<UnitClass> Uclass = new List<UnitClass>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT unit_code, campus, day, start, end, type, room, staff from class where unit_code=?code", conn);
                cmd.Parameters.AddWithValue("code", code);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Uclass.Add(new UnitClass
                    {
                        Unit_Code = rdr.GetString(0),
                        Campus = ParseEnum<Campus>(rdr.GetString(1)),
                        Day = rdr.GetString(2),
                        Start = rdr.GetTimeSpan(3),
                        End = rdr.GetTimeSpan(4),
                        Type = rdr.GetString(5),
                        Room = rdr.GetString(6),
                        Staff = rdr.GetInt32(7)
                    });
                }
            }
            catch (MySqlException e)
            {
                Error("Error fetching from class", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return Uclass;
        }

        //Loading data from class by id
        public static List<UnitClass> FetchClassByID(int id)
        {
            List<UnitClass> Uclass = new List<UnitClass>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT unit_code, day, start, end, room from class where staff=?id", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Uclass.Add(new UnitClass
                    {
                        Unit_Code = rdr.GetString(0),
                        Day = rdr.GetString(1),
                        Start = rdr.GetTimeSpan(2),
                        End = rdr.GetTimeSpan(3),
                        Room = rdr.GetString(4),
                    });
                }
            }
            catch (MySqlException e)
            {
                Error("Error fetching from class", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return Uclass;
        }

        private static void Error(string msg, Exception e)
        {
            ErrorOccurred = true;
            if (ErrorOccurred)
            {
                MessageBox.Show("An error occurred while " + msg + "\nError Details:\n" + e,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
