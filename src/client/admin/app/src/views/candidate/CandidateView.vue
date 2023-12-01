<template>
    <v-main>
      <v-card flat>
        <v-card-title>Candidates</v-card-title>
        <v-card v-if="candidates.listItems.length">
          <v-card-title>
            <v-text-field v-model="candidates.search" append-icon="mdi-magnify" label="Search" single-line hide-details></v-text-field>
          </v-card-title>
          <v-data-table :headers="candidates.headers" :items="candidates.listItems" :search="candidates.search" show-select :dense="candidates.dense">
            <template v-slot:item.actions="{ item }" class="end">
            <v-icon color="primary" @click="viewOrDownloadAttachment(item.raw.attachment)">mdi-download</v-icon>
          </template>
          </v-data-table>
        </v-card>
        <v-card v-else>
          Loading...
        </v-card>
      </v-card>
    </v-main>
  </template>
  
  <script lang="ts" setup>
  import { CandidateControllerApi, type CandidateDto, type GetAllRequest } from '@/api/candidate';
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
      { key: 'gender', title: 'Gender' },
      { key: 'dob', title: 'DOB' },
      { key: 'actions', title: 'Download/View Resume' }, 
    ],
    listItems: canidateDetails
  });
  
  const getAllCandidatesByTenantId = async () => {
    try {
      const request : GetAllRequest ={
      tenantId:'B97684C9-7ACD-40DC-80AC-42F1D0E2F068'
    }
      const data: CandidateDto[] = await candidateApi.getAll(request);
      canidateDetails.value = data
    } catch (error) {
      console.log(error);
    }
  };
  const viewOrDownloadAttachment = (blobUrl: string) => {

  const sasToken = 'sp=r&st=2023-12-01T00:04:45Z&se=2024-02-03T08:04:45Z&sv=2022-11-02&sr=c&sig=k9%2BrmVYmQseQQ3OhpBAMQc%2BYazwg1eYG9GrjgrxAV%2FU%3D';
  const urlWithSAS = `${blobUrl}?${sasToken}`;
  console.log(urlWithSAS)
  window.open(urlWithSAS, '_blank');
};
  
  onMounted(async () => {
    console.log("Component is mounted."); // Debugging check
    await getAllCandidatesByTenantId();
  });
  </script>
  