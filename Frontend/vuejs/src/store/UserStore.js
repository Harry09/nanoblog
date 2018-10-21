import TokenStore from "@/store/TokenStore";
import AccountApi from "@/api/AccountApi";
import router from "@/router";

const UserStore = {
  data: {
    id: "",
    userName: "",
    isLoaded: false
  },
  methods: {
    async login(login, password) {
      const { data } = await AccountApi.login(login, password);

      TokenStore.methods.setSession(data);

      if (TokenStore.methods.isAuthenticated()) {
        this.refreshUserInfo();
      }
    },
    async logout() {
      console.log("Logging out...");

      await AccountApi.revokeToken(TokenStore.data.refreshToken);

      UserStore.data.id = null;
      UserStore.data.userName = null;
      UserStore.data.isLoaded = false;

      TokenStore.methods.resetStorage();
    },
    getUserId() {
      var jwtDecode = require("jwt-decode");

      if (TokenStore.data.accessToken) {
        var token = jwtDecode(TokenStore.data.accessToken);

        return token.sub;
      } else {
        return undefined;
      }
    },
    async refreshUserInfo() {
      const id = this.getUserId();

      UserStore.data.id = id;

      if (id !== undefined) {
        const {
          data
        } = await AccountApi.getUser(id);

        if (data !== null) {
          UserStore.data.userName = data.userName;
          UserStore.data.isLoaded = true;
        }
      }
    }
  }
};

export default UserStore;
