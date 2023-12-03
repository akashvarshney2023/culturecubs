<template>
  <v-main>
    <v-card flat>
      <v-card-title style=" font-size: xx-large;" >        
        <v-icon icon="mdi-account-star"></v-icon> &nbsp;
        Participants</v-card-title>
      <v-card>
        <v-card-title>
          <v-text-field v-model="participants.search" append-icon="mdi-magnify" label="Search" single-line
            hide-details></v-text-field>
        </v-card-title>
        <v-data-table :headers="participants.headers" :items="participants.listItems" :search="participants.search"
          show-select :dense="participants.dense">
          <template v-slot:item.actions="{ item }">
            <v-icon color="primary" @click="viewOrDownloadAttachment(item.raw.attachment)">mdi-download</v-icon>
          </template>
        </v-data-table>
      </v-card>
    </v-card>
  </v-main>
</template>

<script lang="ts" setup>
import { CandidateControllerApi, type CandidateDto, type GetAllParticipantsRequest } from '@/api/candidate';
import { ContestApi, type Contest, type GetcontestsbytenantidRequest } from '@/api/microsite';
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { CandidateTranslator } from './TranslatorListItem';
const contestApi = new ContestApi();
const contestDetails = ref<Contest[]>([]);
const canidateDetails = ref<CandidateDto[]>([]);
const candidateApi = new CandidateControllerApi();
const participants = ref({
  dense: "true",
  search: '',
  headers: [
    { align: 'start', key: 'name', sortable: true, title: 'Name' },
    { key: 'email', title: 'Email',align: 'start' },
    { key: 'phone', title: 'Phone',align: 'start' },
    { key: 'contest', title: 'Contest Name',align: 'start' },
    { key: 'actions', title: 'Download/View Resume',align: 'center',width:200 },
  ],
  listItems: canidateDetails
});
const getAllContestbyTenantId = async () => {
  const request: GetcontestsbytenantidRequest = {
    guid: 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068'
  }
  return contestDetails.value = await contestApi.getcontestsbytenantid(request);
}

const getAllparticipantsByTenantId = async () => {
  try {
    const request: GetAllParticipantsRequest = {
      tenantId: 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068'
    }
    contestDetails.value = await getAllContestbyTenantId();
    const translator = new CandidateTranslator(contestDetails.value);
    const data: CandidateDto[] = await candidateApi.getAllParticipants(request);
    canidateDetails.value = translator.translate(data);

  
  } catch (error) {
    console.log(error);
  }
};
const viewOrDownloadAttachment = (blobUrl: string) => {

  const sasToken = 'sp=r&st=2023-12-01T00:04:45Z&se=2024-02-03T08:04:45Z&sv=2022-11-02&sr=c&sig=k9%2BrmVYmQseQQ3OhpBAMQc%2BYazwg1eYG9GrjgrxAV%2FU%3D';
  const urlWithSAS = `${blobUrl}?${sasToken}`;

  window.open(urlWithSAS, '_blank');
};

onMounted(async () => {
  await getAllparticipantsByTenantId();
});
</script>
