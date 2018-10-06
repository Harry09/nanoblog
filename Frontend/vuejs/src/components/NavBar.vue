<template>
<nav class="navbar navbar-expand navbar-dark bg-dark">
    <a class="navbar-brand" href="">
        Nanoblog
    </a>
    <button
        class="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarNav"
        aria-controls="navbarNav"
        aria-expanded="false"
        aria-label="Toggle navigation"
    >
        <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse">
        <ul class="navbar-nav mr-auto">
        <li class="nav-item active">
            <router-link to="/" class="nav-link">Nanoblog <span class="sr-only">(current)</span></router-link>
        </li>
        </ul>
    </div>

    <span v-if="TokenStore.isAuthenticated">
        <span v-if="UserStore.isLoaded">
            <span class="navbar-text user-welcome">
                Witaj, <a href="">{{UserStore.userName}}</a>
            </span>
            <button v-on:click="onLogout" class="btn btn-secondary"> Logout</button>
        </span>
        <span v-if="UserStore.isLoaded == false" class="navbar-text">
            Ładowanie...
        </span>
    </span>
    <span v-else>
        <router-link to="/login" class="btn btn-secondary" tag="button">Login</router-link>
        <router-link to="/register" class="btn btn-secondary" tag="button">Register</router-link>
    </span>
</nav>
</template>

<script>
import TokenStore from "@/store/TokenStore";
import UserStore from "@/store/UserStore";

export default {
  data() {
    return {
      TokenStore: TokenStore.data,
      UserStore: UserStore.data
    };
  },
  methods: {
    async onLogout() {
      await UserStore.methods.logout();
    }
  }
};
</script>


<style scoped>
.user-welcome {
  margin-right: 16px;
}
</style>
