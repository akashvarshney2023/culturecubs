<template>
  <v-layout min-width="max">
    <v-card max-width="1400" class="mx-auto mt-8 rounded-lg" elevation="12" height="700" width="100%">
      <v-row>
        <v-col cols="12" md="10">
          <v-card-title class="text-center justify-center py-6">
            <h5 class="font-weight-bold text-h4 " style="padding-left: 215px;">
              Edit Contest
            </h5>
          </v-card-title>
        </v-col>
        <v-col cols="12" md="2" class="text-center justify-center py-10">
          <v-btn color="primary" @click="saveOrUpdateContest()">Save</v-btn>
        </v-col>
      </v-row>
      <v-card-text>
        <v-row>
          <v-col cols="12" md="4">
            <v-text-field v-model="contestName" label="Contest Name" outlined density="compact"></v-text-field>
          </v-col>
          <v-col cols="12" md="4">
            <v-text-field v-model="endDate" label="End Date" outlined type="date" density="compact"></v-text-field>
          </v-col>
          <v-col cols="12" md="4">
            <v-textarea v-model="contestDescription" label="Contest Description" outlined rows="1"
              density="compact"></v-textarea>
          </v-col>
        </v-row>

      </v-card-text>
      <v-card-text>
        <v-row>
          <v-col cols="12" md="4">
            <v-text-field v-model="category" label="Category" outlined density="compact"></v-text-field>
          </v-col>
          <v-col cols="12" md="4">
            <v-text-field v-model="location" label="Location" outlined density="compact"></v-text-field>
          </v-col>
          <v-col cols="12" md="4">
            <v-select v-model="experienceLevel" :items="['Beginner', 'Senior', 'Mid-Senior', 'Expert']"
              label="Experience Level" outlined density="compact"></v-select>
          </v-col>
        </v-row>
      </v-card-text>

      <v-tabs v-model="tab" bg-color="transparent" color="primary" align-tabs="center">
        <v-tab v-for="(item, index) in items" :key="item.value" :value="index">
          {{ item.key }}
          <v-icon v-if="item.editingTitle" @click.stop="endTitleEditing(index)">mdi-check</v-icon>
          <v-icon v-else @click.stop="startTitleEditing(index)">mdi-pencil</v-icon>
          <v-icon v-if="item.editable" @click.stop="removeTab(index)">mdi-close</v-icon>
        </v-tab>
        <v-icon color="primary" @click="openAddTabDialog">mdi-plus</v-icon>
      </v-tabs>
      <v-card flat min-height="500" v-if="items.length === 0">
        <div class="empty-card">
          No Tabs Available, Please Click on "+" Sign to create a new contest
        </div>
      </v-card>
      <v-window v-model="tab">
        <v-window-item v-for="(item, index) in items" :key="item.value" :value="index">
          <v-card flat min-height="500">
            <div style="padding-top: 5px;">
              <v-row>
                <v-col cols="12" md="10" offset-md="1">
                  <QuillEditor :options="editorOptions" v-model:content="items[index].content" contentType="html"
                    style="height: 300px; border: 1px solid #d1d5db;" />
                </v-col>
              </v-row>
            </div>
          </v-card>
        </v-window-item>
      </v-window>
    </v-card>

    <v-dialog v-model="showAddTabDialog" max-width="300">
      <v-card>
        <v-card-title>Add New Tab</v-card-title>
        <v-card-text>
          <v-text-field v-model="newTabName" label="Tab Name"></v-text-field>
        </v-card-text>
        <v-card-actions>
          <v-btn @click="cancelAddTab">Cancel</v-btn>
          <v-btn color="primary" @click="confirmAddTab">Add</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-layout>
</template>

<script lang="ts" setup>
import { ref, type Ref, type PropType } from 'vue';
import { Quill, QuillEditor } from '@vueup/vue-quill';
import '@vueup/vue-quill/dist/vue-quill.snow.css';
import '@vueup/vue-quill/dist/vue-quill.bubble.css';
import { ContestApi, type Contest, type CreateorupdatecontestRequest } from '@/api/microsite';
import { onMounted } from 'vue';
const contestDTO: Ref<Contest | null> = ref(null);
const tenantId: Ref<string> = ref('511b3e72-1fe2-424f-b0c7-23b0699ad03e');
const tab = ref(0);
const items = ref([]);
const { contestData } = defineProps(['contestData']);
const editorOptions = {
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
};
const contestName = ref('');
const contestDescription = ref('');
const endDate = ref('');
const showAddTabDialog = ref(false);
const category = ref('');
const location = ref('');
const experienceLevel = ref('');
const newTabName = ref('');

const startTitleEditing = (index: number) => {
  items.value[index].editingTitle = true;
  showAddTabDialog.value = true;
};

const endTitleEditing = (index: number) => {
  items.value[index].editingTitle = false;
};

const openAddTabDialog = () => {
  showAddTabDialog.value = true;
};

const cancelAddTab = () => {
  showAddTabDialog.value = false;
  newTabName.value = '';
};

const confirmAddTab = () => {
  if (newTabName.value.trim() !== '') {
    items.value.push({
      key: newTabName.value,
      value: newTabName.value.toLowerCase().replace(/\s/g, '-'),
      content: '',
      editingTitle: false,
    });
    tab.value = items.value.length - 1;
    showAddTabDialog.value = false;
    newTabName.value = '';
  }
};

const updateEditorContent = (index: number, content: string) => {
  items.value[index].content = content;
};

const removeTab = (index: number) => {
  items.value.splice(index, 1);
  if (tab.value >= items.value.length) {
    tab.value = items.value.length - 1;
  }
};

async function saveOrUpdateContest() {
  const contestApi = new ContestApi();

  // Create the Contest object
  const contest: Contest = {
    id: contestDTO.value?.Id || 0, // Use existing Id if available
    tenantId: tenantId.value,
    title: contestName.value,
    description: contestDescription.value,
    category: category.value,
    experience: experienceLevel.value,
    location: location.value,
    registrationEndDate: new Date(endDate.value),
    isActive: true,
    // ...
    tabs: items.value.map(tab => ({
      id: tab.id || 0,
      key: tab.key,
      value: tab.content,
      contestId: contestDTO.value?.Id || 0,
    })),
  };

  // Create the request object
  const request: CreateorupdatecontestRequest = {
    body: contest,
    guid: tenantId.value
  };

  try {
    // Call the API using async/await
    const result = await contestApi.createorupdatecontest(request);

    // Handle the result as needed
    console.log('API call result:', result);
  } catch (error) {
    // Handle errors
    console.error('Error calling API:', error);
    // You might want to throw or handle the error appropriately
  }
}

onMounted(() => {
  contestName.value = contestData.title || '';
  contestDescription.value = contestData.raw.description || '';
  endDate.value = contestData.raw.registrationEndDate || '';
  category.value = contestData.raw.category || '';
  location.value = contestData.raw.location || '';
  experienceLevel.value = contestData.raw.experienceLevel || '';
});

</script>

<style scoped>
/* Helper classes */
.bg-basil {
  background-color: #FFFBE6 !important;
}

.text-basil {
  color: #356859 !important;
}

.ql-editor {
  height: 100px !important;
}


.empty-card {
  display: flex;
  align-items: center;
  /* Center vertically */
  justify-content: center;
  /* Center horizontally */
  height: 350px;
  border: 1px solid #d1d5db;
  font-size: 24px;
  /* Adjust the font size as needed */
  text-align: center;
  /* Center the text */
  opacity: 0.5;
  /* Adjust the opacity for the watermark effect */
  color: #000;
  /* Set the text color */
}
</style>
