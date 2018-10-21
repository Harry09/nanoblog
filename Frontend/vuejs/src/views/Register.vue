<template>
<form disabled v-on:submit="handleSubmit">
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
        v-model="form.email"
        v-bind:disabled="isProceeding"
        />
    </div>
    <div class="form-group">
        <label>Username</label>
        <input
        type="text"
        class="form-control"
        name="username"
        placeholder="Username"
        v-model="form.username"
        v-bind:disabled="isProceeding"
        />
    </div>
    <div class="form-group">
        <label>Password</label>
        <input
        type="password"
        class="form-control"
        name="password"
        placeholder="Password"
        v-model="form.password"
        v-bind:disabled="isProceeding"
        />
    </div>

    <button type="submit" class="btn btn-primary" name="submit" v-bind:disabled="isProceeding">
        <span v-if="isProceeding == false">
          Register
        </span>
        <span v-else>
          Proceeding...
        </span>
    </button>
</form>
</template>

<script>
import AccountApi from "@/api/AccountApi";
import router from "@/router";

export default {
  data() {
    return {
      form: {
        email: "",
        username: "",
        password: ""
      },
      errorMsg: "",
      isProceeding: false
    };
  },
  methods: {
    async handleSubmit(e) {
      e.preventDefault();

      this.isProceeding = true;

      try {
        var data = await AccountApi.register(
          this.form.email,
          this.form.username,
          this.form.password
        );

        router.push({ name: "login" });
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
