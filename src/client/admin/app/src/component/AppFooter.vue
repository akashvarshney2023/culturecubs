<template>    
  <v-footer class="bg-green-lighten-1" style="position: fixed; bottom: 0; width: 100%;" >
    <v-row justify="center" no-gutters>
      <v-btn color="white" variant="text" class="mx-3 rounded-xl" @click="navigate('home')">Home</v-btn>
      <v-btn color="white" variant="text" class="mx-3 rounded-xl" @click="navigate('about')">About Us</v-btn>
      <v-btn color="white" variant="text" class="mx-3 rounded-xl" @click="navigate('team')">Team</v-btn>
      <v-btn color="white" variant="text" class="mx-3 rounded-xl" @click="navigate('services')">Services</v-btn>
      <v-btn color="white" variant="text" class="mx-3 rounded-xl" @click="navigate('blog')">Blog</v-btn>
      <v-btn color="white" variant="text" class="mx-3 rounded-xl" @click="navigate('contactus')">Contact Us</v-btn>
      <v-col class="text-center mt-4" cols="12">
        {{ new Date().getFullYear() }} — <strong>Cultre Cubs</strong>
      </v-col>
    </v-row>
  </v-footer>
</template>

<script lang='ts' setup>
import { onMounted, ref, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'

const router = useRouter()
const navigate = (name: string) => {
  router.push({
    name: name
  });
};
</script>