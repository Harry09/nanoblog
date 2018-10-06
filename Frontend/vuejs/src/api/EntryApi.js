import Api from "./Api";
import TokenStore from "@/store/TokenStore";

export default {
  async getAllEntries() {
    return await Api().get("/api/entries");
  },
  async addEntry(text) {
    TokenStore.methods.tryRefreshToken();

    var token = TokenStore.data.accessToken;

    return await Api().post(
      "/api/entries", {
        text: text
      }, {
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          Authorization: `Bearer ${token}`
        }
      }
    );
  },
  async removeEntry(id) {
    TokenStore.methods.tryRefreshToken();

    var token = TokenStore.data.accessToken;

    return await Api().delete(`/api/entries/${id}`, {
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`
      }
    });
  }
};
