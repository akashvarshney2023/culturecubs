<template>
  <v-main>
    <v-card flat>
      <v-card-title>Contests</v-card-title>
    
     <v-data-table-server v-model:items-per-page="itemsPerPage" :headers="headers" :items-length="totalItems"
        :items="serverItems" :loading="loading" :search="search" item-value="name"
        @update:options="loadItems"></v-data-table-server>
    </v-card>
    <!-- <v-container>
      <v-label style="font-size: x-large;">Setup your page and workflow.</v-label>
      <v-card></v-card>
      <v-btn color="primary" @click="navigate('contestnew')">Add New Contest</v-btn>
    </v-container>
    <v-container class="grey lighten-5">
     
      <v-row no-gutters class="align-items-center" v-for="(contest, index) in contests" :key="index">
        <v-col cols="12" sm="6">
          <v-card class="pa-2" outlined tile>
            {{ contest.name }}
          </v-card>
        </v-col>
        <v-col cols="12" sm="2" class="d-flex justify-center">
          <v-switch v-model="contest.isActive" color="green" label="Active" hide-details></v-switch>
        </v-col>
        <v-col cols="12" sm="2" class="d-flex justify-center">
          <v-icon color="primary" @click="editContest(index)">mdi-pencil</v-icon>
        </v-col>
        <v-col cols="12" sm="2" class="d-flex justify-center">
          <v-icon color="error" @click="deleteContest(index)">mdi-delete</v-icon>
        </v-col>
      </v-row>
    </v-container> -->
  </v-main>
</template>

<script lang="ts" setup>
import { ContestApi, type GetcontestsbytenantidRequest, type Contest } from '@/api/microsite';
import { onMounted, ref, watch } from 'vue';
import { useRouter, useRoute } from 'vue-router';

const router = useRouter()
const route = useRoute()
const search = ref('');
const serverItems = ref<Contest[]>([]);
const loading = ref(true);
const totalItems = ref(0);
const itemsPerPage = ref(5);
const headers = [
  { title: 'Title', align: 'start', sortable: false, key: 'title' },
  { title: 'Category', key: 'category', align: 'end' },
  { title: 'Location', key: 'location', align: 'end' },
  { title: 'Experience Level (g)', key: 'experience', align: 'end' },
  { title: 'RegistrationEndDate', key: 'registrationEndDate', align: 'end'  },
  { title: 'Summary', key: 'summary', align: 'end' },
];

const navigate = (name: string) => {
  router.push({
    name: name
  });
};

const editContest = (index: number) => {
  // implement edit contest logic
}

const deleteContest = (index: number) => {
  //contests.value.splice(index, 1)
}

const loadItems = async () => {
  loading.value = true;
  var contestApi = new ContestApi();
  const request: GetcontestsbytenantidRequest = {
    guid: "B97684C9-7ACD-40DC-80AC-42F1D0E2F068"
  }
  try {
    const result: Contest[] = await contestApi.getcontestsbytenantid(request);

    result.forEach(item => {
    item.registrationEndDate = item.registrationEndDate?.getDate(); // Adjust accordingly based on your date format
  });
    console.log(result)
    serverItems.value = result;
    totalItems.value = result.length;
    loading.value = false;
  }
  catch (error) {
    loading.value = false;
  }
}


</script>

<style scoped>
.width-50 {
  width: 50%;
}

.align-items-center {
  align-items: center;
}
</style>
