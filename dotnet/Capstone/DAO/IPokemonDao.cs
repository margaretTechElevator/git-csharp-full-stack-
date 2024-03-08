using PokemonDB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonDB.DAO
{
    public interface IPokemonDao
    {
        List<PokemonDetail> GetAllFavoritesByUserId(int id);

        PokemonDetail SaveFavorites(PokemonDetail detail, int userId);

        PokemonDetail GetFavoriteById(int id);
    }
}
