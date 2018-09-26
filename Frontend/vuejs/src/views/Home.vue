<template>
    <div>
      <AddEntry v-if="UserStore.auth.isAuthenticated === true" />
      <Entries />
    </div>
</template>

<script>
import Entries from "@/components/Entries.vue";
import AddEntry from "@/components/AddEntry.vue";
import UserStore from "@/store/UserStore";

export default {
  name: "Home",
  components: { Entries, AddEntry },
  data() {
    return {
      UserStore: UserStore.data
    };
  }
};
</script>

<style scoped>
div {
  margin-top: 16px;
}
</style>
