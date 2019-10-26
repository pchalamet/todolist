using Client.Northwind;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using JustEat.StatsD;

namespace Client
{
    //public class Orders
    //{
    //    public int OrderId;
    //    public string CustomerId;
    //    public string ShipName;
    //}

    class Program
    {
        private static IStatsDPublisher metrics = new StatsDPublisher(new StatsDConfiguration { Host = "localhost" });

        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();
            var db = config.GetValue<string>("Databases:Northwind");
            var cnx = $"Server={db}; Database=Northwind; Trusted_Connection=True";

            //            await CallWebApi();
            CallDatabaseDapper(cnx);
            CallDatabaseEF();
        }

        private static void CallDatabaseEF()
        {
            Console.WriteLine("********************* EF");
            using var dbctx = new Northwind.NorthwindContext();
            var orders = from order in dbctx.Orders
                         where order.Customer.Country == "France"
                         select order;
            foreach(var order in orders)
            {
                metrics.Increment("client.ef");
                Console.WriteLine($"{order.OrderId} => {order.CustomerId}");
            }
        }

        private static async Task CallWebApi()
        {
            var httpClient = new HttpClient();
            var client = new ClientServices.swaggerClient(httpClient); ;
            var data = await client.GetSamplesAsync("titi");
            foreach (var dat in data)
            {
                metrics.Increment("client.webapi");
                Console.WriteLine(dat);
            }
        }


        private static void CallDatabaseDapper(string cnstr)
        {
            Console.WriteLine("********************* Dapper");
            using var cnx = new System.Data.SqlClient.SqlConnection(cnstr);
            cnx.Open();

            var orders = Dapper.SqlMapper.Query<Orders>(cnx, "select * from orders");
            foreach (var order in orders)
            {
//                metrics.Increment("client.dapper");

                Console.WriteLine($"{order.OrderId} => {order.CustomerId}");
            }
        }
    }
}
