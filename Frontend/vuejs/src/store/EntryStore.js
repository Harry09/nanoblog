import EntryApi from "@/api/EntryApi";

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
      // console.log("Update...");

      const data = await EntryApi.getAllEntries();

      EntryStore.data.entries = data.data;
    }
  }
};

export default EntryStore;
