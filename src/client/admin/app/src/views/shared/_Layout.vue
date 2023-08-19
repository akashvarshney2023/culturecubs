<template>
  <AppHeader v-if="showHeader"/>
  <router-view/>
  <AppFooter/>
</template>
<script lang='ts' setup>
import { onMounted, ref, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import  AppHeader from '@/component/AppHeader.vue';
import  AppFooter from '@/component/AppFooter.vue';
const route = useRoute()

var activetab = ref('');
var showHeader = ref(false);
onMounted(() => {
  // Check for token in localStorage
  const token = localStorage.getItem('token');
  showHeader.value = !!token; 
});

watch(() => route.name, () => {
  if (route.name == 'contest') {
    activetab.value = "contest";
  } else if (route.name == 'home') {
    activetab.value = "home";
  } else if (route.name == 'participant') {
    activetab.value = "participant";
  } else if (route.name == 'candidate') {
    activetab.value = "candidate";
  } else {
    activetab.value = "home";
  }
}, { immediate: true });

</script>
