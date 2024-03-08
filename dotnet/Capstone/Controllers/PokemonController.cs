using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonDB.DAO;
using PokemonDB.Models;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PokemonController : ControllerBase
    {
        private readonly IUserDao userDao;
        private readonly IPokemonDao pokemonDao;

        public PokemonController(IUserDao userDao, IPokemonDao pokemonDao)
        {
            this.userDao = userDao;
            this.pokemonDao = pokemonDao;
        }

        [HttpPost("/favorite")]
        public ActionResult<PokemonDetail> SaveFavorite (PokemonDetail pokemonDetail)
        {
            User user = userDao.GetUserByUsername(User.Identity.Name);
            PokemonDetail createdPokemon = pokemonDao.SaveFavorites(pokemonDetail, user.UserId);
            return Created($"/createdPokemon.id", createdPokemon);
        }

        [HttpGet("/favorites")]
        public ActionResult<List<PokemonDetail>> getAllFavorites()
        {
            User user = userDao.GetUserByUsername(User.Identity.Name);
            return pokemonDao.GetAllFavoritesByUserId(user.UserId);
        }
    }
}
