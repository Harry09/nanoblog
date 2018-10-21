<template>
<form v-on:submit="handleSubmit">
    <div v-if="errorMsg.length > 0" class="alert alert-danger" role="alert">
        {{errorMsg}}
    </div>

    <div class="form-group">
        <label htmlFor="input-email-form">E-mail</label>
        <input
            type="email"
            id="input-email-form"
            class="form-control"
            name="email"
            placeholder="E-mail"
            v-model="form.login"
            v-bind:disabled="isProceeding"
        />
    </div>

    <div class="form-group">
        <label htmlFor="input-password-form">Password</label>
        <input
            type="password"
            id="input-password-form"
            class="form-control"
            name="password"
            placeholder="Password"
            v-model="form.password"
            v-bind:disabled="isProceeding"
        />
    </div>

    <button type="submit" class="btn btn-primary" name="submit" v-bind:disabled="isProceeding">
        <span v-if="isProceeding == false">
            Log in
        </span>
        <span v-else>
            Proceeding...
        </span>
    </button>
</form>
</template>

<script>
import UserStore from "@/store/UserStore";

export default {
  data() {
    return {
      form: {
        login: "",
        password: ""
      },
      isProceeding: false,
      errorMsg: ""
    };
  },
  methods: {
    async handleSubmit(e) {
      e.preventDefault();

      this.isProceeding = true;

      try {
        await UserStore.methods.login(this.form.login, this.form.password);

        router.push({
          name: "home"
        });
      } catch (ex) {
        this.isProceeding = false;

        if (ex.response == null) {
          this.errorMsg = "Unknown error!";
        } else {
          this.errorMsg = ex.response.data.message;
        }
      }
    }
  }
};
</script>


<style scoped>
</style>

