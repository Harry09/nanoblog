<template>
<form v-on:submit="handleSubmit">
    <div v-if="errorMsg.length > 0" class="alert alert-danger" role="alert">
        {{errorMsg}}
    </div>

    <div class="form-group">
        <label>E-mail</label>
        <input
        type="email"
        class="form-control"
        name="email"
        placeholder="E-mail"
        v-model="email"
        />
    </div>
    <div class="form-group">
        <label>Nazwa użytkownika</label>
        <input
        type="text"
        class="form-control"
        name="username"
        placeholder="Nazwa użytkownika"
        v-model="username"
        />
    </div>
    <div class="form-group">
        <label>Hasło</label>
        <input
        type="password"
        class="form-control"
        name="password"
        placeholder="Hasło"
        v-model="password"
        />
    </div>

    <button type="submit" class="btn btn-primary" name="submit">
        Zarejestruj się
    </button>
</form>
</template>

<script>
import AccountApi from "@/api/AccountApi";
import router from "@/router";

export default {
  data() {
    return {
      email: "",
      username: "",
      password: "",
      errorMsg: ""
    };
  },
  methods: {
    async handleSubmit(e) {
      e.preventDefault();

      try {
        var data = await AccountApi.register(
          this.email,
          this.username,
          this.password
        );

        router.push({ name: "login" });
      } catch (ex) {
        this.errorMsg = ex.response.data.message;
      }
    }
  }
};
</script>
