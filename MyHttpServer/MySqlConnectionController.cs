using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServer
{
    class MySqlConnectionController
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader = null;
        private string myConnectionString = "Database=server_db_internet_technology;Data Source=localhost;User Id=root;Password=123456root";


        public MySqlConnectionController()
        {
            connect();
            connection.Open();
            command = new MySqlCommand();
        }
        public MySqlConnectionController(MySqlCommand comm)
        {
            command = comm;
            connect();
        }

        public MySqlConnection getConnection() { return connection; }
        public MySqlCommand getCommand() { return command; }

        public MySqlDataReader getReader() { return reader; }


        public void setConnection(MySqlConnection conn) { connection = conn; }
        public void setCommand(MySqlCommand comm) { command = comm; }

        public void setReader(MySqlDataReader reader) { this.reader = reader; }

        public void connect()
        {
            try
            {
                connection = new MySqlConnection(myConnectionString);
                //Console.WriteLine("connected");
            }
            catch (MySqlException e)
            {
                //Console.Write("Error:" + e.ToString());
            }
        }

        public void open() { connection.Open(); }
        public void close() { connection.Close(); }




        public void add(Man man)
        {
            //Console.WriteLine("adding");
            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO man (name,lastname,age) " +
              "VALUES(?nam,?lnam,?ag)";
            command.Prepare();
            command.Parameters.AddWithValue("?nam", man.Name);
            command.Parameters.AddWithValue("?lnam", man.Lastname);
            command.Parameters.AddWithValue("?ag", man.Age);
            //command.CommandText = "Insert into man (name) values('hahaha')";
            command.ExecuteNonQuery();
        }


        public int findIndexOf(int id)
        {
            command = connection.CreateCommand();
            command.CommandText = "select(count(id)) from man where id <= ?id";
            command.Prepare();
            command.Parameters.AddWithValue("?id", id);
            reader = command.ExecuteReader();
            reader.Read();

            return int.Parse(reader[0].ToString());
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Man findOne(int id)
        {
            command.CommandText = "Select * from man where id = ?id";
            command.Prepare();
            command.Parameters.AddWithValue("?id", id);
            reader = command.ExecuteReader();
            reader.Read();
            Man man;

            try
            {
                string str = reader[8].ToString();
                char c = str[0];
                man = new Man(reader[1].ToString(), reader[2].ToString(), int.Parse(reader[3].ToString()));
                reader.Close();
            }
            catch (Exception ex)
            {
                reader.Close();
                return null;
            }

            return man;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<Man> findAll()
        {
            List<Man> men = new List<Man>();
            command.CommandText = "Select * from man";
            reader = command.ExecuteReader();
            while (reader.Read())
            {

                men.Add(new Man(int.Parse(reader[0].ToString()),reader[1].ToString(), reader[2].ToString(), int.Parse(reader[3].ToString())));
            }
            reader.Close();
            return men;
        }
    }
}
