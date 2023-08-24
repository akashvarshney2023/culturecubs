<template>
  <v-main>
    <v-card max-width="1200" class="mx-auto mt-8 rounded-lg" elevation="12" height="650" width="100%">
      <v-card-title class="text-center justify-center py-6">
        <h1 class="font-weight-bold text-h4 ">
          Add New Contest
        </h1>
      </v-card-title>
      <v-card-text>
        <v-row>
          <v-col cols="12" md="6">
            <v-text-field v-model="contestName" label="Contest Name" outlined></v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="endDate" label="End Date" outlined type="date"></v-text-field>
          </v-col>
        </v-row>
        <v-textarea v-model="contestDescription" label="Contest Description" outlined rows="1"></v-textarea>
      </v-card-text>

      <v-tabs v-model="tab" bg-color="transparent" color="primary" align-tabs="center">
        <v-tab v-for="(item, index) in items" :key="item.value" :value="index">
          {{ item.key }}
        </v-tab>
        <v-btn @click="addNewTab">+</v-btn> <!-- Add button for adding tabs -->
      </v-tabs>

      <v-window v-model="tab">
        <v-window-item v-for="item in items" :key="item" :value="item">
          <v-card flat min-height="500">
            <div style="padding-top: 10px;">
              <v-row>
                <v-col cols="12" md="10" offset-md="1">
                  <QuillEditor ref="quillEditor" :options="editorOptions" v-model="editorContent"
                    @image-added="handleImageAdded" />
                </v-col>
              </v-row>
            </div>
          </v-card>
        </v-window-item>
      </v-window>
      <v-btn color="primary"> Save </v-btn>
      <v-btn> Cancel </v-btn>
    </v-card>
  </v-main>
</template>

<script lang="ts">
import { QuillEditor } from '@vueup/vue-quill'
import '@vueup/vue-quill/dist/vue-quill.snow.css'
import '@vueup/vue-quill/dist/vue-quill.bubble.css'
export default {
  components: {
    QuillEditor,
  },
  methods: {
      // ...existing methods...
      addNewTab() {
        this.items.push({ key: 'New Tab', value: 'new-tab' });
        this.tab = this.items.length - 1; // Switch to the newly added tab
      }
    },
  data() {
    return {
      tab: 0,
      items: [
        { key: 'Problem Statement', value: 'problem' },
        { key: 'Eligibility Criteria', value: 'eligibility' },
        // ...existing items...
      ],
      editorContent: '',
      editorOptions: {
        placeholder: 'Start typing here...',
        modules: {
          toolbar: [
            [{ header: [1, 2, false] }],
            ['bold', 'italic', 'underline', 'strike'],
            [{ color: [] }, { background: [] }],
            [{ list: 'ordered' }, { list: 'bullet' }],
            ['link', 'image', 'video'],
            ['clean'],
          ],
        },
      },
      contestName: '',           // Add contest name property
      contestDescription: '',    // Add contest description property
      endDate: '',               // Add end date property
    }     
  },
}
</script>
<style scoped>
/* Helper classes */
.bg-basil {
  background-color: #FFFBE6 !important;
}

.text-basil {
  color: #356859 !important;
}
</style>
