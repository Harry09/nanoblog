<template>
<form v-on:submit="handleSubmit">
    <div class="form-group">
        <label htmlFor="input-email-form">E-mail</label>
        <input
            type="email"
            id="input-email-form"
            class="form-control"
            name="email"
            placeholder="E-mail"
            v-model="login"
        />
    </div>

    <div class="form-group">
        <label htmlFor="input-password-form">Hasło</label>
        <input
            type="password"
            id="input-password-form"
            class="form-control"
            name="password"
            placeholder="Hasło"
            v-model="password"
        />
    </div>

    <button type="submit" class="btn btn-primary" name="submit">
        Zaloguj się
    </button>
</form>
</template>

<script>
import UserStore from "@/store/UserStore";

export default {
  data() {
    return {
      login: "",
      password: ""
    };
  },
  methods: {
    handleSubmit(e) {
      e.preventDefault();

      UserStore.methods.login(this.login, this.password);
    }
  }
};
</script>


<style scoped>
</style>

