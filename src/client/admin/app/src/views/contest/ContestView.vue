<template>
  <v-main>
    <v-card flat>
      <v-card-title class="d-flex align-center pe-2">
        <v-icon icon="mdi-trophy"></v-icon> &nbsp;
        Contest Details
        <v-spacer></v-spacer>
        <v-text-field v-model="search" prepend-inner-icon="mdi-magnify" density="compact" label="Search" single-line flat
          hide-details variant="solo-filled"></v-text-field>
      </v-card-title>
      <v-divider></v-divider>
      <v-data-table v-model:search="search" :items="serverItems" :headers="headers" >        
        <template v-slot:item.title="{ item }">
          <div class="text-start">
            <v-chip :color="item.title ? 'green' : 'red'" :text="item.title" class="text-uppercase" label 
              size="small"></v-chip>
          </div>
        </template>
        <template v-slot:item.isActive="{ item }">
          <v-icon :color="item.raw.isActive ? 'green' : 'red'"> {{ item.raw.isActive ? 'mdi-check-circle' : 'mdi-close-circle' }}
          </v-icon>
        </template>
        <template v-slot:item.edit="{ item }">
          <v-icon @click="editContest(item)" color="primary">
            mdi-pencil
          </v-icon>
        </template>
      </v-data-table>
    </v-card>
  </v-main>
</template>

<script lang="ts" setup>
import { ContestApi, type GetcontestsbytenantidRequest, type Contest } from '@/api/microsite';
import { onMounted, ref, type Ref } from 'vue';
const serverItems = ref<Contest[]>([]);
const search = ref('');
const totalItems = ref(0);
const headers = [
  { title: 'Title', align: 'start', sortable: false, key: 'title' },
  { title: 'Category', key: 'category', align: 'start' },
  { title: 'Location', key: 'location', align: 'start' },
  { title: 'Experience Level (g)', key: 'experience', align: 'start' },
  { title: 'RegistrationEndDate', key: 'registrationEndDate', align: 'start' },
  { title: 'Summary', key: 'summary', align: 'start' },
  { title: 'IsActive', key: 'isActive', align: 'start' },
  { title: 'Edit', key: 'edit', align: 'start' },
];



const editContest = (item: Contest) => {
  // Implement edit contest logic using the item data
  console.log(item);
};

const deleteContest = (index: number) => {
  //contests.value.splice(index, 1)
}

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
</style>
