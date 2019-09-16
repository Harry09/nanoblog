import EntryApi from "@/api/EntryApi";
import AccountApi from "../api/AccountApi";

const EntryStore = {
  data: {
    entries: []
  },
  methods: {
    async addEntry(text) {
      return await EntryApi.addEntry(text);
    },
    async removeEntry(id) {
      return await EntryApi.removeEntry(id);
    },
    async updateList() {
      const data = await EntryApi.getAllEntries();

      let _entries = data.data;

      EntryStore.data.entries = _entries;
    }
  }
};

export default EntryStore;
