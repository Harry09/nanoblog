<template>
    <div class="entries">
        <Entry 
            v-for="entry in EntryStore.entries" 
            v-bind:key="entry.id" 
            v-bind:entry="entry" 
            />

        <h3 v-if="EntryStore.entries.length === 0">Nic tutaj nie ma ;(</h3>
    </div>
</template>

<script>
import Entry from "./Entry.vue";
import EntryStore from "@/store/EntryStore.js";

export default {
  name: "Entries",
  components: { Entry },
  data() {
    return {
      EntryStore: EntryStore.data
    };
  },
  async created() {
    await EntryStore.methods.updateList();
  }
};
</script>

