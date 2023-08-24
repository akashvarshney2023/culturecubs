<template>
    <v-layout min-width="max">
        <v-card max-width="1400" class="mx-auto mt-8 rounded-lg" elevation="12" height="700" width="100%">
            <v-row>
                <v-col cols="12" md="10">
                    <v-card-title class="text-center justify-center py-6">
                        <h1 class="font-weight-bold text-h4 " style="padding-left: 215px;">
                            Add New Contest
                        </h1>
                    </v-card-title>
                </v-col>
                <v-col cols="12" md="2" class="text-center justify-center py-10">
                    <v-btn color="primary">Save</v-btn>
                </v-col>
            </v-row>
            <v-card-text>
                <v-row>
                    <v-col cols="12" md="4">
                        <v-text-field v-model="contestName" label="Contest Name" outlined></v-text-field>
                    </v-col>
                    <v-col cols="12" md="4">
                        <v-text-field v-model="endDate" label="End Date" outlined type="date"></v-text-field>
                    </v-col>
                    <v-col cols="12" md="4">
                        <v-textarea v-model="contestDescription" label="Contest Description" outlined rows="1"></v-textarea>
                    </v-col>
                </v-row>

            </v-card-text>

            <v-tabs v-model="tab" bg-color="transparent" color="primary" align-tabs="center">
                <v-tab v-for="(item, index) in items" :key="item.value" :value="index">
                    {{ item.key }}
                    <v-icon v-if="item.editingTitle" @click.stop="endTitleEditing(index)">mdi-check</v-icon>
                    <v-icon v-else @click.stop="startTitleEditing(index)">mdi-pencil</v-icon>
                    <v-icon v-if="item.editable" @click.stop="removeTab(index)">mdi-close</v-icon>
                </v-tab>
                <v-icon color="primary" @click="openAddTabDialog">mdi-plus</v-icon>
            </v-tabs>
            <v-window v-model="tab">
                <v-window-item v-for="(item, index) in items" :key="item.value" :value="index">
                    <v-card flat min-height="500">
                        <div style="padding-top: 10px;">
                            <v-row>
                                <v-col cols="12" md="10" offset-md="1">
                                    <QuillEditor :options="editorOptions" v-model="items[index].content"
                                      style="height: 350px; border: 1px solid #d1d5db;" />
                                </v-col>
                            </v-row>
                        </div>
                    </v-card>
                </v-window-item>
            </v-window>
        </v-card>

        <v-dialog v-model="showAddTabDialog" max-width="300">
            <v-card>
                <v-card-title>Add New Tab</v-card-title>
                <v-card-text>
                    <v-text-field v-model="newTabName" label="Tab Name"></v-text-field>
                </v-card-text>
                <v-card-actions>
                    <v-btn @click="cancelAddTab">Cancel</v-btn>
                    <v-btn color="primary" @click="confirmAddTab">Add</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-layout>
</template>
  
<script lang="ts">
import { QuillEditor } from '@vueup/vue-quill'
import '@vueup/vue-quill/dist/vue-quill.snow.css'
import '@vueup/vue-quill/dist/vue-quill.bubble.css'
export default {
    components: {
        QuillEditor,
    },
    methods: {
        startTitleEditing(index) {
            this.items[index].editingTitle = true;
        },
        endTitleEditing(index) {
            this.items[index].editingTitle = false;
        },
        openAddTabDialog() {
            this.showAddTabDialog = true;
        },
        cancelAddTab() {
            this.showAddTabDialog = false;
            this.newTabName = '';
        },
        confirmAddTab() {
            if (this.newTabName.trim() !== '') {
                this.items.push({ key: this.newTabName, value: this.newTabName.toLowerCase().replace(/\s/g, '-'), content: '', editingTitle: false });
                this.tab = this.items.length - 1;
                this.showAddTabDialog = false;
                this.newTabName = '';
            }
        },
        updateEditorContent(index, content) {
            this.items[index].content = content;
        },
        removeTab(index) {
            this.items.splice(index, 1);
            if (this.tab >= this.items.length) {
                this.tab = this.items.length - 1;
            }
        },
    },
    data() {
        return {
            tab: 0,
            items: [
                { key: 'Problem Statement', value: 'problem', content: '', editingTitle: false, editable: false },
                { key: 'Eligibility Criteria', value: 'eligibility', content: '', editingTitle: false, editable: false },
                // ...existing items...
            ],
            editorOptions: {
                placeholder: 'Start typing here...',
                modules: {
                    toolbar: [
                        [{ header: [1, 2, false] }],
                        ['bold', 'italic', 'underline', 'strike'],
                        [{ color: [] }, { background: [] }],
                        [{ list: 'ordered' }, { list: 'bullet' }],
                        ['link', 'image', 'video'],
                        ['clean'],
                    ],
                },
            },
            contestName: '',
            contestDescription: '',
            endDate: '',
            showAddTabDialog: false,
            newTabName: '',
        };
    },
}
</script>
<style scoped>
/* Helper classes */
.bg-basil {
    background-color: #FFFBE6 !important;
}

.text-basil {
    color: #356859 !important;
}

.ql-editor {
    height: 100px !important;  
}</style>
  