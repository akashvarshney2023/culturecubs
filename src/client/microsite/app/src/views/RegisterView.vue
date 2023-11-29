<template>
  <v-main>
    <v-container fluid>
      <v-row>
        <v-col cols="9">
          <div class="green-base-image">
          </div>
        </v-col>
        <v-col cols="3">
          <v-card>
            <v-card-title class="text-center">Register</v-card-title>
            <v-card-text>
              <v-form @submit.prevent="submitForm">
                <v-text-field label="Full Name" type="text" v-model="user" prepend-icon="mdi-account"
                  required></v-text-field>
                <v-text-field label="Email" type="email" v-model="email" prepend-icon="mdi-email" required></v-text-field>
                <v-text-field label="Phone" type="tel" v-model="phone" prepend-icon="mdi-phone" required></v-text-field>
                <v-text-field label="Company" type="text" v-model="company" prepend-icon="mdi-domain"
                  required></v-text-field>
                <FileUpload :loading="loading" /> 

                <!-- Loading spinner for the entire page -->
                <v-overlay :value="loading">
                  <v-progress-circular indeterminate color="primary"></v-progress-circular>
                </v-overlay>

                <v-checkbox label="I agree to the terms and conditions of Curlture Cubs and am happy to sign this"
                  v-model="agree"></v-checkbox>
                <v-row class="d-flex justify-center">
                  <v-col cols="auto">
                    <v-btn color="primary" type="submit" @click="submitForm" block>Submit</v-btn>
                  </v-col>
                  <v-col cols="auto">
                    <v-spacer></v-spacer>
                    <v-btn color="secondary" block>Cancel</v-btn>
                  </v-col>
                </v-row>
              </v-form>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-main>
</template>

<script lang="ts" setup>
import { defineComponent, ref, type Ref } from 'vue';
import { useRouter } from 'vue-router';
import { CandidateApi } from '@/apis/microsite/apis';
import type { CandidateregistrationRequest } from '@/apis/microsite/apis';
import type { CandidateRegistration } from '@/apis/microsite/models';
import type { AddCandidateRequest } from '@/apis/candidate/apis';
import type { Candidate } from '@/apis/candidate/models';
import FileUpload from "@/component/FileUpload.vue";

const router = useRouter();
const candidateApi = new CandidateApi();

const user = ref('');
const email = ref('');
const phone = ref('');
const company = ref('');
const attachment: Ref<File | null> = ref(null);
const agree = ref(false);
const loading =ref(false);


const submitForm = async () => {
  if (attachment.value) {
    this.loading = true;
  }

  // Create a new candidate registration object
  const candidateRegistration: CandidateregistrationRequest = {
    company: company.value,
    user: user.value,
    email: email.value,
    phone: phone.value,
  };
};
const navigate = (name: string) => {
  router.push({ name });
};

</script>

<style>
.green-base-image {
  background-image: url('./../../assets/images/login_left.jpg');
  background-size: cover;
  background-position: center center;
  height: 100vh;
  width: 100%;
  background-color: green;
}

.width-50 {
  width: 100%;
}
</style>
