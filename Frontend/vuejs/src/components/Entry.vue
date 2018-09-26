<template>
<div class="post">
    <div class="post-header">
        <div class="post-author">
        <a href="">{{entry.author.userName}}</a>
        </div>

        <div class="post-date">
        <a href="">{{entry.createTime}}</a>
        </div>

        <div v-if="entry.author.id === UserStore.user.id" class="post-delete">
          <a href="" class="post-delete-link" v-on:click="onDelete">
            Usuń
          </a>
        </div>
    </div>

    <div class="post-text">{{entry.text}}</div>
    </div>
</template>

<script>
import EntryStore from "@/store/EntryStore";
import UserStore from "@/store/UserStore";

export default {
  name: "Entry",
  props: {
    entry: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      UserStore: UserStore.data
    };
  },
  methods: {
    async onDelete(e) {
      e.preventDefault();

      await EntryStore.methods.removeEntry(this.entry.id);
      await EntryStore.methods.updateList();
    }
  }
};
</script>

<style scoped>
.post {
  padding: 10px;
  font-family: Arial;
  margin-top: 16px;
  border: 1px solid #ccc;
  border-radius: 10px;
}

.comment {
  border: 0;
}

.post-header {
  border-bottom: 1px #aaa solid;
  padding: 8px;
}

.post-author {
  display: inline-block;
}

.post-date {
  margin-left: 4px;
  display: inline-block;
  font-size: 0.8em;
}

.post-delete {
  display: inline-block;
  font-size: 0.6em;
  margin-left: 8px;
}

.post-delete a:link {
  color: red;
}

.post-delete a:visited {
  color: red;
}

.post-delete a:active {
  color: red;
}

.post-plus {
  float: right;
}

.post-plus-num {
  display: inline-block;
}

.post-plus-button {
  display: inline-block;
}

.post-text {
  padding: 8px;
}

.post-text-deleted {
  padding: 8px;
  font-style: italic;
}

.post-image img {
  max-width: 100%;
  height: auto;
}

.post-menu {
  font-size: 0.9em;
  margin-left: 16px;
}

.post .comments {
  margin-left: 32px;
}

.post .show-more {
  margin: 0 auto;
  margin-top: 8px;
  font-size: 0.8em;
  text-align: center;
}
</style>
