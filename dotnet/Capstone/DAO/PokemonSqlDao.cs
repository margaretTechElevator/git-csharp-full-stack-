using PokemonDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PokemonDB.DAO
{
    public class PokemonSqlDao : IPokemonDao
    {
        private readonly string connectionString;

        /// <summary>
        /// Create a new user DAO with the supplied data source.
        /// </summary>
        /// <param name="connString">Database connection string.</param>
        public PokemonSqlDao(string connString)
        {
            connectionString = connString;
        }


        public List<PokemonDetail> GetAllFavoritesByUserId(int id)
        {
            List<PokemonDetail> list = new List<PokemonDetail>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT p.id, api_id, name, base_experience, height, weight, back_url, front_url FROM pokemon p " +
                "JOIN users_pokemon up ON p.id = up.pokemon_id WHERE " +
                "users_id = @users_id;";
                command.Parameters.AddWithValue("@users_id", id);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PokemonDetail detail = MapRowToPokemonDetail(reader);
                    list.Add(detail);
                }
            }

            return list;
        }

        public PokemonDetail GetFavoriteById(int id)
        {
            throw new NotImplementedException();
        }

        public PokemonDetail SaveFavorites(PokemonDetail detail, int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO pokemon(api_id, name, base_experience, height, weight, back_url, front_url) " +
                    "OUTPUT INSERTED.id " +
                "VALUES(@api_id, @name, @base_experience, @height, @weight, @back_url, @front_url)";
                command.Parameters.AddWithValue("@api_id", detail.apiId);
                command.Parameters.AddWithValue("@name", detail.name);
                command.Parameters.AddWithValue("@base_experience", detail.baseExperience);
                command.Parameters.AddWithValue("@height", detail.height);
                command.Parameters.AddWithValue("@weight", detail.weight);
                command.Parameters.AddWithValue("@back_url", detail.sprites["back_default"]);
                command.Parameters.AddWithValue("@front_url", detail.sprites["front_default"]);

                object newId = command.ExecuteScalar();
                int pokemonId = Convert.ToInt32(newId);

                command.CommandText = "INSERT INTO users_pokemon (pokemon_id, users_id) VALUES (@pokemon_id, @users_id);";
                command.Parameters.AddWithValue("@pokemon_id", pokemonId);
                command.Parameters.AddWithValue("@users_id", userId);

                command.ExecuteNonQuery();
            }
            return null;

        }

        private PokemonDetail MapRowToPokemonDetail(SqlDataReader reader)
        {
            PokemonDetail temp = new PokemonDetail();
            temp.baseExperience = Convert.ToInt32(reader["base_experience"]);
            temp.height = Convert.ToInt32(reader["height"]);
            temp.id = Convert.ToInt32(reader["id"]);
            temp.apiId = Convert.ToInt32(reader["api_id"]);
            temp.weight = Convert.ToInt32(reader["weight"]);
            temp.name = Convert.ToString(reader["name"]); 
            string back_default = Convert.ToString(reader["back_url"]);
            string front_default = Convert.ToString(reader["front_url"]);
            Dictionary<string, string> sprites = new Dictionary<string, string>();
            sprites["back_default"] = back_default;
            sprites["front_default"] = front_default;
            temp.sprites = sprites;
            return temp;
        }
    }
}
