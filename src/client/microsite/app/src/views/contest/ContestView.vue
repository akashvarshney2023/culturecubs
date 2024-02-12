<template>
    <v-main class="">
        <v-divider></v-divider>
        <v-card class="ma-custom " rounded="0">
            <div class="d-flex justify-center mb-0 bg-surface">
                <v-btn prepend-icon="mdi-account-circle" color="primary" class="ma-2 pa-2"
                    @click="registerForContest(currentContest)">Register for Contest</v-btn>
            </div>
            <v-tabs v-model="activeTab" color="primary" align-tabs="center">
                <v-tab v-for="(tab, index) in currentContest.tabs" :key="index" :value="tab.key">
                    {{ tab.key }}
                </v-tab>
            </v-tabs>
            <v-card-text>
                <v-window v-model="activeTab">
                    <v-window-item v-for="(tab, index) in currentContest.tabs" :key="index" :value="tab.key">
                        <v-container fluid>
                            <v-card-text v-html="tab.value"></v-card-text>
                        </v-container>
                    </v-window-item>
                </v-window>
            </v-card-text>
        </v-card>
    </v-main>
</template>

<script lang="ts" setup>
import { ContestApi } from '@/apis/microsite/apis/ContestApi'
import type { Contest } from '@/apis/microsite/models/Contest'
import { ref, onMounted, type Ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
const router = useRouter()
const route = useRoute();
var contestApi = new ContestApi();
const contestList = ref<Contest[]>([]);
const contest:Ref<Contest> = ref({});
const pageLoading = ref(true);
const activeTab = ref('')
const currentContest = ref({
    id: 15,
    title: '',
    tabs: {},
})

const registerForContest = (currentContest: any) => {
    const contestId = currentContest == null ? 15 : currentContest.id
    router.push({ name: 'Registration', params: { contestId } });

};
onMounted(() => {
    if (true) {
        var tenantId = 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068'
        contestApi.getcontestsbytenantid({ guid: tenantId })
            .then((response) => {
                contestList.value = response as Contest[];
                // Find the contest with the highest ID
                const latestContest = response.reduce((prev, current) => (prev.id > current.id) ? prev : current);
                currentContest.value = latestContest;
            })
            .catch(error => {
                console.log(error);
            });
    } else {
        pageLoading.value = false;
    }
});

</script>

<style scoped> .v-container {
     max-width: 1900px;
 }

 .ma-custom {
     margin: 0px 175px 0px 175px !important;
 }
</style>