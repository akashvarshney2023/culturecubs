<template>
  <v-main>
    <v-card flat>
      <v-card-title style="font-size: xx-large;">
        <v-icon icon="mdi-trophy"></v-icon> &nbsp;
        Contest Details
        <v-spacer></v-spacer>
        <v-text-field v-model="search" prepend-inner-icon="mdi-magnify" density="compact" label="Search" single-line flat
          hide-details variant="solo-filled"></v-text-field>
      </v-card-title>
      <v-divider></v-divider>
      <v-card v-if="loading && !serverItems.length">
        <v-skeleton-loader class="mx-auto border" min-width="2000"
          type="table-thead,table-tbody,table-row-divider,table-row"></v-skeleton-loader>
      </v-card>
      <v-card v-else-if="serverItems.length">
        <v-data-table v-model:search="search" :items="serverItems" :headers="headers">
          <template v-slot:item.registrationEndDate="{ item }">
            {{ formatDate(item.raw.registrationEndDate) }}
          </template>
          <template v-slot:item.title="{ item }">
            <div class="text-start">
              <v-chip :color="item.title ? 'green' : 'red'" :text="item.title" class="text-uppercase" label
                size="small"></v-chip>
            </div>
          </template>
          <template v-slot:item.isActive="{ item }">
            <v-icon :color="item.raw.isActive ? 'green' : 'red'"> {{ item.raw.isActive ? 'mdi-check-circle' :
              'mdi-close-circle' }}
            </v-icon>
          </template>
          <template v-slot:item.edit="{ item }">
            <v-icon @click="editContest(item)" color="primary">
              mdi-pencil
            </v-icon>
          </template>
        </v-data-table>
      </v-card>
      <v-card flat v-else >
        <div  class="empty-card">
          No Contest Available, Please go to settings and new create contest.
        </div>
      </v-card>
    </v-card>
    <v-dialog v-model="showContestEditorDialog" max-width="800">

      <EditContest :contestData="selectedContest" @saveOrUpdateContest="saveOrUpdateContest" />

    </v-dialog>
  </v-main>
</template>

<script lang="ts" setup>
import moment from "moment";
import { ContestApi, type GetcontestsbytenantidRequest, type Contest } from '@/api/microsite';
import { onMounted, ref, type Ref } from 'vue';
import EditContest from './EditContest.vue'
const showContestEditorDialog = ref(false);
const selectedContest = ref<Contest>({});
const serverItems = ref<Contest[]>([]);
const search = ref('');
const loading = ref(true);
const totalItems = ref(0);
const headers = [
  { title: 'Title', align: 'start', sortable: false, key: 'title' },
  { title: 'Category', key: 'category', align: 'start' },
  { title: 'Location', key: 'location', align: 'start' },
  { title: 'Experience Level (g)', key: 'experience', align: 'start' },
  { title: 'Registration End Date', key: 'registrationEndDate', align: 'start' },
  { title: 'Summary', key: 'summary', align: 'start' },
  { title: 'IsActive', key: 'isActive', align: 'center' },
  { title: 'Edit', key: 'edit', align: 'start' },
];



const editContest = (item: Contest) => {
  selectedContest.value = item;
  showContestEditorDialog.value = true;
};

const saveOrUpdateContest = async () => {
  // Handle the logic to save or update the contest
  // You can pass the selectedContest data to your API update function in the ContestEditor component
  // ...

  // Close the dialog after saving/updating
  showContestEditorDialog.value = false;
  // Reload the contest list or update the edited contest in the list
  await loadItems();
};

const loadItems = async () => {
  var contestApi = new ContestApi();
  const request: GetcontestsbytenantidRequest = {
    guid: "B97684C9-7ACD-40DC-80AC-42F1D0E2F068"
  }
  try {
    const result: Contest[] = await contestApi.getcontestsbytenantid(request);
    serverItems.value = result;
    totalItems.value = result.length;
  }
  catch (error) {
  }
  finally {
    loading.value = false;
  }
}
const formatDate = (value: any) => {
  return moment(value).format("MMMM DD YYYY");
}
onMounted(async () => {
  await loadItems();
});


</script>

<style scoped>
.width-50 {
  width: 50%;
}

.align-items-center {
  align-items: center;
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
