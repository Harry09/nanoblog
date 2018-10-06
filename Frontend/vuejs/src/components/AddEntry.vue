<template>
    <div>
        <h3>Dodaj post</h3>
        <form v-on:submit="addEntry">
          <div class="form-group">
            <textarea
              id="input-text-form"
              rows="5"
              v-model="entryText"
            />
          </div>

          <button type="submit" class="btn btn-primary">
            Dodaj
          </button>
        </form>
    </div>
</template>

<script>
import EntryStore from "@/store/EntryStore.js";

export default {
  data() {
    return {
      entryText: ""
    };
  },
  methods: {
    async addEntry(e) {
      e.preventDefault();

      try {
        let result = await EntryStore.methods.addEntry(this.entryText);
        EntryStore.methods.updateList();
      } catch (exc) {
        console.log(exc, exc.response);
      }

      this.entryText = "";
    }
  }
};
</script>

<style scoped>
#input-text-form {
  width: 100%;
}
</style>


