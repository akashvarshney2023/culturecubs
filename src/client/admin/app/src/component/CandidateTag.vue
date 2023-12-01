<template>
    <v-row justify="center">
        <v-dialog v-model="dialog" scrollable width="auto">           
            <v-card>
                <v-card-title>Select Contest</v-card-title>
                <v-divider></v-divider>
                <v-card-text style="height: 300px;">
                    <v-radio-group v-model="dialogm1" column>
                        <v-radio v-for="item in result" :key="item.id" :label="item.title" :value="item.title"></v-radio>
                    </v-radio-group>
                </v-card-text>
                <v-divider></v-divider>
                <v-card-actions>
                    <v-btn color="blue-darken-1" variant="text" @click="closeDialog">
                        Close
                    </v-btn>
                    <v-btn color="blue-darken-1" variant="text" @click="saveSelection">
                        Save
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-row>
</template>
<script lang="ts" setup>
import { ContestApi, type GetcontestsbytenantidRequest, type Contest } from '@/api/microsite';
import { ref, defineProps, onMounted, onUnmounted, defineEmits, type Ref } from 'vue';
const contestAPI = new ContestApi();
const result: Ref<Contest[]> = ref([]);
const dialogm1 = ref<string>("");
const dialog = ref(false);
const emits = defineEmits();

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



const saveSelection = () => {
    // Emit an event to send the selected ID to the parent page
    emits("save", dialogm1.value);
    dialog.value = false;
};

</script>
