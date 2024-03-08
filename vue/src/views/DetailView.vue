<template>
    <div>
      <pokemon-detail :id="$route.params.id" />
    </div>
  </template>
  
  <script>
  import PokemonDetail from "../components/PokemonDetail.vue";
  export default {
    components: { PokemonDetail },
  };
  </script>
  
  <style>
  </style>