import AccountApi from "@/api/AccountApi";
import UserStore from "@/store/UserStore";

const TokenStore = {
  data: {
    accessToken: null,
    refreshToken: null,
    expiresAt: null,
    isAuthenticated: false
  },
  methods: {
    async refreshToken() {
      if (TokenStore.data.refreshToken !== null) {
        try {
          var {
            data
          } = await AccountApi.refreshToken(
            TokenStore.data.refreshToken
          );

          console.log("Got new access token! :D");
          this.setSession(data);
        } catch (exc) {
          console.log("No more access tokens :(");

          try {
            await UserStore.methods.logout();
          } catch (ex) {
            console.log(ex);
          }

          this.resetStorage();
        }
      }
    },
    async tryRefreshToken() {
      if (!this.isAuthenticated()) {
        await this.refreshToken();
      }
    },
    isAuthenticated() {
      TokenStore.data.isAuthenticated =
        (new Date().getTime() < TokenStore.data.expiresAt);

      return TokenStore.data.isAuthenticated;
    },
    setSession(data) {
      TokenStore.data.accessToken = data.token;
      TokenStore.data.refreshToken = data.refreshToken;
      TokenStore.data.expiresAt = data.expires;

      this.saveStorage();
    },
    loadStorage() {
      TokenStore.data.accessToken = localStorage.getItem("access_token");
      TokenStore.data.refreshToken = localStorage.getItem("refresh_token");
      TokenStore.data.expiresAt = localStorage.getItem("expires_at");
    },
    saveStorage() {
      localStorage.setItem("access_token", TokenStore.data.accessToken);
      localStorage.setItem("refresh_token", TokenStore.data.refreshToken);
      localStorage.setItem("expires_at", TokenStore.data.expiresAt);
    },
    resetStorage() {
      console.log("Reset");

      localStorage.removeItem("access_token");
      localStorage.removeItem("refresh_token");
      localStorage.removeItem("expires_at");

      TokenStore.data.accessToken = null;
      TokenStore.data.refreshToken = null;
      TokenStore.data.expiresAt = null;
      TokenStore.data.isAuthenticated = false;
    }
  }
};

export default TokenStore;
