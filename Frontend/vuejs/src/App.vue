<template>
  <div>
    <NavBar />
    <main role="main" class="container">
     <router-view />
    </main>

    <footer class="footer">
      <div class="container">
        <span class="text-muted">Piotr Krupa &copy; 2018</span>
      </div>
    </footer>
  </div>
</template>

<script>
import NavBar from "./components/NavBar.vue";
import Home from "./views/Home.vue";

import TokenStore from "./store/TokenStore";
import UserStore from "./store/UserStore";

export default {
  name: "app",
  components: { NavBar, Home },
  async created() {
    try {
      await TokenStore.methods.loadStorage();
      await TokenStore.methods.tryRefreshToken();
      if (TokenStore.methods.isAuthenticated()) {
        await UserStore.methods.refreshUserInfo();
      }
    } catch (ex) {
      console.log(ex);
    }
  }
};
</script>

<style src="bootstrap/dist/css/bootstrap.min.css">
</style>

<style scoped>
.container {
  margin-top: 16px;
}
</style>
