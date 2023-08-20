
<template>
    <v-main>
        <v-layout style="min-height:89%;">
            <v-navigation-drawer v-model="drawer" :rail="rail" permanent @click="rail = false">
                <v-list-item title="Microsoft" nav>
                    <template v-slot:append>
                        <v-btn variant="text" icon="mdi-chevron-left" @click.stop="rail = !rail"></v-btn>
                    </template>
                    <template v-slot:prepend>
                        <img src="https://www.microsoft.com/favicon.ico?v2" alt="Microsoft Logo" class="avatar-image">
                    </template>
                </v-list-item>
                <v-divider></v-divider>
                <v-list density="compact" nav>
                    <v-list-item prepend-icon="mdi-account" title="My Account" value="account"></v-list-item>
                    <v-list-item prepend-icon="mdi-account-group-outline" title="candidates" value="users"></v-list-item>
                    <v-list-item prepend-icon="mdi-account-multiple-plus" title="participants"
                        value="Participants"></v-list-item>
                </v-list>
            </v-navigation-drawer>
            <v-main style="height: 250px"></v-main>
        </v-layout>
    </v-main>
</template>
<script lang="ts" setup>
import { ref } from 'vue';


const drawer = ref(true);
const rail = ref(true);

</script>
<style scoped>
.avatar-image {
    width: auto;
    /* Adjust the width as needed */
    height: 24px;
    /* Adjust the height as needed */
    border-radius: 0;
    /* Remove circular border radius */
    padding-right : 10px;
    padding-left : 10px;
}</style>