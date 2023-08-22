<template>
  <header class="sticky-top custom-navbar">
    <v-app-bar class="bg-white" density="default" :elevation="0"
      style="border-bottom:1px solid #e0e0e0;padding:0px 10% !important;">
      <v-app-bar-title>
        <v-img src="../../assets/images/logo.png" style="width:350px;height:54px;"></v-img>
      </v-app-bar-title>
      <template v-slot:append>
        <span class="pr-5" style="font-size:large;">Akash Varshney</span>
        <v-btn icon @click="logout">
          <v-icon>mdi-logout</v-icon>
        </v-btn>
      </template>
    </v-app-bar>
    <v-app-bar density="default" style="padding:0px 10% !important;" class="bg-white">
      <v-tabs color="primary" v-model="activetab">
        <v-tab value="home" @click="navigate('home')">Home</v-tab>
        <v-tab value="contest" @click="navigate('contest')">Contest</v-tab>
        <v-tab value="participants" @click="navigate('participants')">Participants</v-tab>
        <v-tab value="candiate" @click="navigate('candidate')">Candidate</v-tab>
        <v-tab value="settings" @click="navigate('settings')">Settings</v-tab>
      </v-tabs>
    </v-app-bar>
  </header>
</template>


<script lang='ts' setup>
import { onMounted, ref, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'

const router = useRouter()
const route = useRoute()
const navigate = (name: string) => {
  router.push({
    name: name
  });
};

const logout = () => {
  // Clear local storage or perform any other necessary logout actions
  localStorage.clear();
  window.location.reload();// it will auto redirect to the login page
};



var activetab = ref('');

watch(() => route.name, () => {
  if (route.name == 'contest') {
    activetab.value = "contest";
  } else if (route.name == 'home') {
    activetab.value = "home";
  } else if (route.name == 'participant') {
    activetab.value = "participant";
  } else if (route.name == 'candidate') {
    activetab.value = "candidate";
  } else if (route.name == 'settings') {
    activetab.value = "settings";
  } else {
    activetab.value = "home";
  }
}, { immediate: true });

</script>

  

