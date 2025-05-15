using GameRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; 

namespace GameRentalSystem.DAL
{
    internal class CategoryDAL
    {
        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            string commandText = "SELECT CategoryID, Name, Description FROM Categories ORDER BY Name";

           
            using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText))
            {
               
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                      
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description"))
                    });
                }
            }

            return categories;
        }

    }
}