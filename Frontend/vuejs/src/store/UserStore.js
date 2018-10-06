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
      try {
        const {
          data
        } = await AccountApi.login(login, password);

        TokenStore.methods.setSession(data);

        router.push({
          name: "home"
        });

        if (TokenStore.methods.isAuthenticated()) {
          this.refreshUserInfo();
        }
      } catch (ex) {
        console.log(ex);
      }
    },
    async logout() {
      console.log("Logging out...");

      try {
        await AccountApi.revokeToken(TokenStore.data.refreshToken);
      } catch (ex) {
        console.log(ex);
      }

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
