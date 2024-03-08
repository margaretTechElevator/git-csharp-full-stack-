<template>
    <div>
      <ul>
        <li v-for="pokemon in pokemonArray" :key="pokemon.id">
          <router-link
            :to="{
              name: 'detail',
              params: {
                id: pokemon.id,
              },
            }"
          >
            {{pokemon.id}} - {{ pokemon.name }}
          </router-link>
        </li>
      </ul>
      <button v-if="count> 0" v-on:click="getPrev">Previous</button>
      <button v-if="count < 101" @click="getNext">Next</button>
    </div>
  </template>
  
  <script>
  import pokemonService from "@/services/PokemonService.js";
  export default {
    name: "pokemon-list",
    data() {
      return {
        pokemonArray: [],
        count:0
      };
    },
    methods: {
      getMore(){
        pokemonService.getMorePokemon(this.count)
        .then((response) => {
          // console.log(response.data.results);
        let temp = response.data.results;
        this.pokemonArray = temp.map((item) => {
            let pokemonIndex = item.url.indexOf('pokemon');
            let pokemonStr = item.url.substring(pokemonIndex);
            let slashIndex = pokemonStr.indexOf('/');
            let numberStr = pokemonStr.substring(slashIndex + 1, pokemonStr.length - 1);
            item.id = +numberStr;
            return item;
        })
      })
      },
      getNext() {
        this.count += 20;
        this.getMore();
      },
      getPrev() {
        this.count -= 20;
        this.getMore();
      }
    },
    created() {
      pokemonService.getAllPokemon().then((response) => {
          // console.log(response.data.results);
        let temp = response.data.results;
        this.pokemonArray = temp.map((item) => {
            let pokemonIndex = item.url.indexOf('pokemon');
            let pokemonStr = item.url.substring(pokemonIndex);
            let slashIndex = pokemonStr.indexOf('/');
            let numberStr = pokemonStr.substring(slashIndex + 1, pokemonStr.length - 1);
            item.id = +numberStr;
            return item;
        })
      });
    },
  };
  </script>
  
  <style scoped>
  ul > li {
    list-style-type: none;
  }
  </style>