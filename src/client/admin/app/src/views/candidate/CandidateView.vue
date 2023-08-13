<template>
    <v-main>
        <v-card flat>
            <v-card-title>Participants</v-card-title>
            <v-card>
                <v-card-title>
                    <v-text-field v-model="candidates.search" append-icon="mdi-magnify" label="Search" single-line
                        hide-details></v-text-field>
                </v-card-title>
                <v-data-table :headers="candidates.headers" :items="candidates.listItems" :search="candidates.search"
                    show-select :dense="candidates.dense"></v-data-table>
            </v-card>
            <v-spacer></v-spacer>
            <v-btn color="primary">Edit</v-btn>
        </v-card>
    </v-main>
</template>
<script lang="ts" setup>
import { CandidateControllerApi, type CandidateDto } from '@/api';
import { onMounted, ref, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'
const candidateApi = new CandidateControllerApi();
const listItems = ref<CandidateDto>({});
const router = useRouter()
const route = useRoute()
const candidates = ref({
    dense: "true",
    search: '',
    headers: [
        {
            align: 'start',
            key: 'name',
            sortable: false,
            title: 'Name',
        },
        { key: 'email', title: 'Email' },
        { key: 'phone', title: 'Phone' },
        { key: 'gender', title: 'Company' },
        { key: 'dob', title: 'DOB' },
    ],
    listItems: listItems.value
});


const getAllCandidatesByTenantId = async () => {
  try {
    const data : CandidateDto[] = await candidateApi.getAll();
    listItems.value = data;    
  } catch (error) {
    console.log(error);
  }
};
onMounted(() => {
    getAllCandidatesByTenantId();
});
</script>




