<template>
    <v-main>
      <v-layout style="min-height: 89%;">
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
            <v-list-item prepend-icon="mdi-account" title="My Account" value="account" @click="setActiveComponent('account')"></v-list-item>
            <v-list-item prepend-icon="mdi-account-group-outline" title="Candidates" value="candidates" @click="setActiveComponent('candidates')"></v-list-item>
            <v-list-item prepend-icon="mdi-account-multiple-plus" title="Participants" value="participants" @click="setActiveComponent('participants')"></v-list-item>
            <v-list-item prepend-icon="mdi-trophy" title="Contests" value="contests" @click="setActiveComponent('contests')"></v-list-item>
            
          </v-list>
        </v-navigation-drawer>
        <v-main>
          <!-- Conditionally render components based on the active component -->
          <Account v-if="activeComponent === 'account'" />
          <CandidateView v-if="activeComponent === 'candidates'" />
          <Participant v-if="activeComponent === 'participants'" />
          <ContestView v-if="activeComponent === 'contests'" />
        </v-main>
      </v-layout>
    </v-main>
  </template>
  
  <script lang="ts" setup>
  import { ref } from 'vue';
  import CandidateView from '@/views/settings/options/CandidateView.vue';
  import Account from '@/views/settings/options/Account.vue';
  import Participant from '@/views/settings/options/Participants.vue';
  import ContestView from '@/views/settings/options/ContestView.vue';
  
  const drawer = ref(true);
  const rail = ref(true);
  const activeComponent = ref(null); // Initialize with null
  
  const setActiveComponent = (value) => {
    activeComponent.value = value;
  };
  </script>
  
  <style scoped>
  .avatar-image {
    width: auto;
    /* Adjust the width as needed */
    height: 24px;
    /* Adjust the height as needed */
    border-radius: 0;
    /* Remove circular border radius */
    padding-right: 10px;
    padding-left: 10px;
  }
  </style>
  