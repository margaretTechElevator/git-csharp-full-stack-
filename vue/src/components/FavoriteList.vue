<template>
    <div>
        <h1>Favorites List:</h1>
        <ul>
            <li v-for="fave in favoriteArray" :key="fave.id">
            {{ fave.id }} - {{  fave.name }}</li>
        </ul>
    </div>
</template>

<script>
import authService from '../services/AuthService';

export default {
    data(){
        return {
            favoriteArray: []
        }
    },
    created() {
        authService.getAllFavorites()
        .then ((response) => {
            this.favoriteArray = response.data;
        });
    }
}
</script>


<style>
</style>