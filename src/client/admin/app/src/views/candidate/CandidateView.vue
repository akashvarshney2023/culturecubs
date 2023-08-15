<template>
    <v-main>
      <v-card flat>
        <v-card-title>Participants</v-card-title>
        <v-card v-if="candidates.listItems.length">
          <v-card-title>
            <v-text-field v-model="candidates.search" append-icon="mdi-magnify" label="Search" single-line hide-details></v-text-field>
          </v-card-title>
          <v-data-table :headers="candidates.headers" :items="candidates.listItems" :search="candidates.search" show-select :dense="candidates.dense"></v-data-table>
        </v-card>
        <v-card v-else>
          Loading...
        </v-card>
      </v-card>
    </v-main>
  </template>
  
  <script lang="ts" setup>
  import { CandidateControllerApi, type CandidateDto } from '@/api';
  import { onMounted, ref } from 'vue'
  import { useRouter } from 'vue-router'
  const canidateDetails = ref<CandidateDto[]>([]);
  const candidateApi = new CandidateControllerApi();
  const candidates = ref({
    dense: "true",
    search: '',
    headers: [
      { align: 'start', key: 'name', sortable: false, title: 'Name' },
      { key: 'email', title: 'Email' },
      { key: 'phone', title: 'Phone' },
      { key: 'gender', title: 'Company' },
      { key: 'dob', title: 'DOB' },
    ],
    listItems: canidateDetails
  });
  
  const getAllCandidatesByTenantId = async () => {
    try {
      const data: CandidateDto[] = await candidateApi.getAll();
      console.log(data)
      canidateDetails.value = data
    } catch (error) {
      console.log(error);
    }
  };
  
  onMounted(async () => {
    console.log("Component is mounted."); // Debugging check
    await getAllCandidatesByTenantId();
  });
  </script>
  