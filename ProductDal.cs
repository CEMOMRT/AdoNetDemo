using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AdoNetDemo
{
    public class ProductDal//Dal: Data Access Layer
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB;initial catalog=ETrade;integrated security=true");
        private void ConnectionControl()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)//Halihazırda açık olup olmadığını kontrol ediyor.
            {
                _connection.Open();
            }
        }
        public List<Product> GetAll()
        {
            /*Bağlantı bilgileri yazılır
            *  @ yazılanı tamamen string okuturur
            *  intial catalog bağlanılacak veri tabanını belirtir
            *  son kısım Windows Authentication ile bağlanır
            */
            ConnectionControl();
            SqlCommand command = new SqlCommand("Select * From Products", _connection);//Yazılan sorguyu oluşan bağlantıya göndermemiz gerekir.
            SqlDataReader dataReader = command.ExecuteReader();
            //DataTable dataTable = new DataTable(); DataTable oldukça fazla memory alanı kaplar ve serileştirme özelliği olmayan bir nesnedir bu yüzden kullanılmaz.
            //dataTable.Load(dataReader);
            List<Product> products = new List<Product>();
            while (dataReader.Read())//Gelen kayıdı tek tek okur.
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(dataReader["Id"]),
                    Name = dataReader["Name"].ToString(),
                    StockAmount = Convert.ToInt32(dataReader["StockAmount"]),
                    UnitPrice = Convert.ToDecimal(dataReader["UnitPrice"])
                };
                products.Add(product);
            }
            dataReader.Close();
            _connection.Close();
            return products;
        }
        public void Add(Product product)
        {
            ConnectionControl();
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Products values(@name,@unitPrice,@stockAmount)", _connection);
            sqlCommand.Parameters.AddWithValue("@name", product.Name);//Product sınıfındaki name alanını yukarıdaki komutta bulunan name parametresine ekliyor.
            sqlCommand.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            sqlCommand.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            sqlCommand.ExecuteNonQuery();
            _connection.Close();
        }
        public void Update(Product product)
        {
            ConnectionControl();
            SqlCommand sqlCommand = new SqlCommand("UPDATE Products SET Name=@name, UnitPrice=@unitPrice, StockAmount=@stockAmount WHERE Id=@id", _connection);
            sqlCommand.Parameters.AddWithValue("@name", product.Name);//Product sınıfındaki name alanını yukarıdaki komutta bulunan name parametresine ekliyor.
            sqlCommand.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            sqlCommand.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            sqlCommand.Parameters.AddWithValue("@id", product.Id);
            sqlCommand.ExecuteNonQuery();
            _connection.Close();
        }
        public void Delete(int ID)
        {
            ConnectionControl();
            SqlCommand sqlCommand = new SqlCommand("DELETE FROM Products WHERE Id=@id", _connection);
            sqlCommand.Parameters.AddWithValue("@id", ID);
            sqlCommand.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
