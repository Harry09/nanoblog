import Api from "./Api";
import UserStore from "@/store/UserStore";

export default {
  async getAllEntries() {
    return await Api().get("/api/entries");
  },
  async addEntry(text) {
    var token = UserStore.data.auth.accessToken;

    return await Api().post(
      "/api/entries",
      {
        text: text
      },
      {
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          Authorization: `Bearer ${token}`
        }
      }
    );
  },
  async removeEntry(id) {
    var token = UserStore.data.auth.accessToken;

    return await Api().delete(`/api/entries/${id}`, {
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: `Bearer ${token}`
      }
    });
  }
};
