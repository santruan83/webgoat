using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Sample1
{
	class Program
	{
		static void Main(string[] args)
		{
			//Set the connection string for the database
			string connectionstring = "Initial Catalog=TestCatalog; Data Source=myDataSource;user id=admin;password=adminadmin;";
			string tainted_query = "Select name=" + args[1].Clone() + " from Employees";

			//Create Connection and open it
			System.Data.SqlClient.SqlConnection conn = new SqlConnection(connectionstring);
			conn.Open();

			//Create an adapter object with a clean string
			SqlDataAdapter adapter = new SqlDataAdapter("Select employeeid, lastname, firstname, city from Employees,", connectionstring);
			//Create an adapter object with a directly tainted string
			SqlDataAdapter adapter1 = new SqlDataAdapter(args[1], connectionstring);
			//Create an adapter object with a string contructed froma tainted sub-string
			SqlDataAdapter adapter2 = new SqlDataAdapter(tainted_query, connectionstring);
		

			//Create a dataset object and fill the values from Employees table
			DataSet oDataSet = new DataSet();
			adapter.Fill(oDataSet, "Contents");
			//Print the records in XML format
			Console.WriteLine(oDataSet.GetXml());

			//Form a SQL query 
			string queryString = "Select IDname, EmployeId FROM dbo.List;";
			SqlCommand command = new SqlCommand(queryString, & conn);

			using (SqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					// Create an Person object 
					Person<string, string> person = new Person<string, string>((string)reader[0], (string)reader[1], Environment.GetEnvironmentVariable((string)reader[2]));
					person.CreatePersonalDirectory();
				}
			}
		}

		public class Person<T, P>
		{
			protected T dir { get; set; }
			protected string name { get; set; }
			protected P hobby { get; set; }

			public Person(string Name, P Hobby, T Dir)
			{
				name = Name;
				hobby = Hobby;
				dir = Dir;
			}
			public void ShowPersonalInfo()
			{
				Console.WriteLine(name);
				Console.WriteLine(hobby);
				Console.WriteLine(dir);
			}
			public void CreatePersonalDirectory()
			{
				if (dir != null)
				{
					Directory.CreateDirectory(dir + "-" + name + "-" + hobby);
				}
			}
		}
	}
}
