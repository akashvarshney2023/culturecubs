<template>
  <v-main>
    <v-card flat>
      <v-card-title>Candidates</v-card-title>
      <v-card v-if="candidates.listItems.length">
        <v-card-title>
          <v-text-field v-model="candidates.search" append-icon="mdi-magnify" label="Search" single-line
            hide-details></v-text-field>
        </v-card-title>
        <v-data-table :headers="candidates.headers" :items="candidates.listItems" :search="candidates.search" show-select
          :dense="candidates.dense">
          <template v-slot:item.actions="{ item }" style="align-items:end;">
            <v-icon color="primary" @click="viewOrDownloadAttachment(item.raw.attachment)">mdi-download</v-icon>
          </template>
          <template v-slot:item.tagContest="{ item }" style="align-items:end;">
            <v-icon color="success" @click="openDialog()">mdi-tag</v-icon>
            <v-row justify="center">
              <v-dialog v-model="dialog" scrollable width="auto">
                <v-card flat>
                  <v-card-title>Select Contest</v-card-title>
                  <v-divider></v-divider>
                  <v-card-text style="height: 400px;">
                    <v-radio-group v-model="dialogm1" column>
                      <v-radio v-for="item in result" :key="item.id" :label="item.title" :value="item.title"></v-radio>
                    </v-radio-group>
                  </v-card-text>
                  <v-divider></v-divider>
                  <v-card-actions>
                    <v-btn color="blue-darken-1" variant="text" @click="closeDialog">
                      Close
                    </v-btn>
                    <v-btn color="blue-darken-1" variant="text" @click="tagCandidateToContest(item)">
                      Save
                    </v-btn>
                  </v-card-actions>
                </v-card>
              </v-dialog>
            </v-row>
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
import CandidateTag from '@/component/CandidateTag.vue'
import { CandidateControllerApi, type CandidateDto, type GetAllRequest } from '@/api/candidate';
import { ContestApi, type Contest, type GetcontestsbytenantidRequest } from '@/api/microsite';
import { onMounted, ref, type Ref } from 'vue'
import { useRouter } from 'vue-router'
const canidateDetails = ref<CandidateDto[]>([]);
const candidateApi = new CandidateControllerApi();
const contestAPI = new ContestApi();
const result: Ref<Contest[]> = ref([]);
const dialog = ref(false)
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
    { key: 'tagContest', title: 'Tag To Contest' }
  ],
  listItems: canidateDetails
});
const openDialog = async () => {
  dialog.value = true;
  try {
    const request: GetcontestsbytenantidRequest = {
      guid: 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068'
    };
    // Assuming result is a ref
    // You need to define and initialize result somewhere in your component
    result.value = await contestAPI.getcontestsbytenantid(request);
  } catch (error) {
    console.log(error);
  }
};

const getAllCandidatesByTenantId = async () => {
  try {
    const request: GetAllRequest = {
      tenantId: 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068'
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
const closeDialog = () => {
  dialog.value = false;
};
const tagCandidateToContest = async (candidate: CandidateDto) => {
  //call Contest APi to view a popup list of contest 
  try {
    const request: GetcontestsbytenantidRequest = {
      guid: 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068'
    }
    result.value = await contestAPI.getcontestsbytenantid(request);
  } catch (error) {
    console.log(error);
  }

  console.log(candidate);
  console.log(`Tagging candidate ${candidate.name} to the contest`);
};

onMounted(async () => {
  console.log("Component is mounted."); // Debugging check
  await getAllCandidatesByTenantId();
});
</script>
  