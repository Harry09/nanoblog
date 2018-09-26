import Api from "./Api";

export default {
  async register(email, userName, password) {
    return await Api().post("/api/accounts/register", {
      email: email,
      userName: userName,
      password: password
    });
  },

  async login(email, password) {
    console.log(email, password);

    return await Api().post(
      "/api/accounts/login",
      {
        email: email,
        password: password
      },
      {
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json"
        }
      }
    );
  },

  async getUser(userId) {
    return await Api().get(`/api/accounts/user/${userId}`);
  },

  async refreshToken(token) {
    return await Api().get(`/api/accounts/tokens/refresh/${token}`);
  },

  async revokeToken(token) {
    return await Api().post(`/api/accounts/tokens/revoke/${token}`);
  }
};
