<template>
    <div>
      <!-- <h2>{{ pokemon.name }}</h2> -->
      <p>{{ pokemon.name }}</p>
      <p>Height: {{ pokemon.height }}</p>
      <p>Weight: {{ pokemon.weight }}</p>
      <img :src="spriteImgFront" alt="" /> &nbsp;
      <img :src="spriteImgBack" alt="" />

      <button @click="saveFavorites">Save to Favorites?</button>
    </div>
  </template>
  
  <script>
  import pokemonService from "../services/PokemonService";
  import authService from '../services/AuthService';
  export default {
    name: "pokemon-detail",
    props: {
      id: String,
    },
    data() {
      return {
        pokemon: {},
        spriteImgBack: "",
        spriteImgFront: "",
      };
    },
    methods: {
      saveFavorites(){
        let newPokemon = {
          apiId: this.pokemon.id,
          name: this.pokemon.name,
          base_experience: this.pokemon.base_experience,
          height: this.pokemon.height,
          weight: this.pokemon.weight,
          sprites : {
            back_default: this.spriteImgBack,
            front_default: this.spriteImgFront
          }
        }
        authService.saveFavorite(newPokemon)
        .then((response) => {
          if(response.status === 201){
            alert("Pokemon is saved!");
            this.$router.push({name: 'pokemon'});
          }
        })
      }
    },
    created() {
      let numId = +this.id;
      pokemonService.getPokemonDetail(numId).then((response) => {
        //   console.log(response.data);
        this.pokemon = response.data;
        this.spriteImgFront = response.data.sprites.front_default;
        this.spriteImgBack = response.data.sprites.back_default;
        console.log(this.spriteImg);
      });
    },
  };
  </script>
  
  <style>
  </style>