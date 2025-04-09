using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace ActorMovieList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            Console.WriteLine( "choose a first name of an actor?");

            var imput = Console.ReadLine();

            var command1 = new SqlCommand(@"
                    SELECT actor.first_name, film.title 
                    FROM actor 
                    INNER JOIN film_actor ON actor.actor_id = film_actor.actor_id 
                    INNER JOIN film ON film.film_id = film_actor.film_id
                    WHERE actor.first_name = @name", connection);

            connection.Open();

            command1.Parameters.AddWithValue("@name", imput);

            var rec = command1.ExecuteReader();

            if (rec.HasRows)
            {
                while (rec.Read())
                {
                    Console.WriteLine($"{rec["first_name"]} - {rec["title"]}");
                }
            }

            connection.Close();
        }
    }
}

