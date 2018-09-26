import AccountApi from "@/api/AccountApi";
import router from "@/router";

const UserStore = {
  data: {
    auth: {
      accessToken: "",
      refreshToken: "",
      expiresAt: "",
      isAuthenticated: false
    },
    user: {
      id: "",
      userName: ""
    }
  },
  methods: {
    async login(login, password) {
      try {
        const { data } = await AccountApi.login(login, password);

        this.setSession(data);

        router.push({ name: "home" });
        this.isAuthenticated();
      } catch (ex) {
        console.log(ex);
      }
    },
    async logout() {
      console.log("Logging out...");

      await AccountApi.revokeToken(UserStore.data.auth.refreshToken);
      this.localStorage.reset();

      this.isAuthenticated();
    },
    async refreshToken() {
      try {
        var { data } = await AccountApi.refreshToken(
          UserStore.data.auth.refreshToken
        );

        console.log("Nowy access token! :D");
        this.setSession(data);
      } catch (exc) {
        console.log("Nie ma access tokenu :(");
        this.logout();
      }
    },
    async tryRefreshToken() {
      if (
        UserStore.data.auth.refreshToken !== null &&
        !this.isAuthenticated()
      ) {
        this.refreshToken();
      }
    },
    isAuthenticated() {
      UserStore.data.auth.isAuthenticated =
        new Date().getTime() < UserStore.data.auth.expiresAt;

      return UserStore.data.auth.isAuthenticated;
    },
    setSession(data) {
      UserStore.data.auth.accessToken = data.token;
      UserStore.data.auth.refreshToken = data.refreshToken;
      UserStore.data.auth.expiresAt = data.expires;

      this.localStorage.save();
    },
    getUserId() {
      var jwtDecode = require("jwt-decode");

      if (UserStore.data.auth.accessToken) {
        var token = jwtDecode(UserStore.data.auth.accessToken);

        return token.sub;
      } else {
        return undefined;
      }
    },
    async refreshUserInfo() {
      const id = this.getUserId();

      UserStore.data.user.id = id;

      if (id !== undefined) {
        const { data } = await AccountApi.getUser(id);

        UserStore.data.user.userName = data.userName;
      }
    },
    localStorage: {
      save() {
        localStorage.setItem("access_token", UserStore.data.auth.accessToken);
        localStorage.setItem("refresh_token", UserStore.data.auth.refreshToken);
        localStorage.setItem("expires_at", UserStore.data.auth.expiresAt);
      },
      load() {
        UserStore.data.auth.accessToken = localStorage.getItem("access_token");
        UserStore.data.auth.refreshToken = localStorage.getItem(
          "refresh_token"
        );
        UserStore.data.auth.expiresAt = localStorage.getItem("expires_at");
      },
      reset() {
        localStorage.removeItem("access_token");
        localStorage.removeItem("expires_at");
        localStorage.removeItem("refresh_token");

        UserStore.data.auth.accessToken = null;
        UserStore.data.auth.refreshToken = null;
        UserStore.data.auth.expiresAt = null;

        UserStore.data.user.id = null;
        UserStore.data.user.userName = null;
      }
    }
  }
};

export default UserStore;
