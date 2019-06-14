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

      for (let i = 0; i < _entries.length; i++)
      {
        var author = await AccountApi.getUser(_entries[i].authorId);

        _entries[i].author = author.data;
      }

      EntryStore.data.entries = _entries;
    }
  }
};

export default EntryStore;
