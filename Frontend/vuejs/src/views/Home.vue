<template>
    <div>
      <AddEntry v-if="TokenStore.isAuthenticated === true" />
      <Entries />
    </div>
</template>

<script>
import Entries from "@/components/Entries.vue";
import AddEntry from "@/components/AddEntry.vue";
import TokenStore from "@/store/TokenStore";

export default {
  name: "Home",
  components: { Entries, AddEntry },
  data() {
    return {
      TokenStore: TokenStore.data
    };
  }
};
</script>

<style scoped>
div {
  margin-top: 16px;
}
</style>
