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
              <v-form>
                <v-text-field label="Full Name" type="text" v-model="fullName" prepend-icon="mdi-account"
                  required></v-text-field>
                <v-text-field label="Email" type="email" v-model="email" prepend-icon="mdi-email" required></v-text-field>
                <v-text-field label="Phone" type="tel" v-model="phoneNumber" prepend-icon="mdi-phone"
                  required></v-text-field>
                <v-text-field label="Company" type="text" v-model="currentCompany" prepend-icon="mdi-domain"
                  required></v-text-field>
                <FileUpload :loading="loading" @file-uploaded="handleFileUploaded" />
                <v-checkbox label="I agree to the terms and conditions of Curlture Cubs and am happy to sign this"
                  v-model="agree"></v-checkbox>
                <v-row class="d-flex justify-center">
                  <v-col cols="auto">
                    <v-btn color="primary" type="submit" @click="submitForm" block :disabled="!agree">Submit</v-btn>
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
    <v-dialog v-model="successDialog" max-width="400">
      <v-card>
        <v-card-title>Candiate Registration</v-card-title>
        <v-card-text>
          Thanks for participating for the contest. Your details are saved with us and soon you will be notified with
          details. Happy Learing !!
        </v-card-text>
        <v-card-actions>
          <v-btn @click="closeSuccessDialog">OK</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-main>
</template>

<script lang="ts" setup>
import { ref, type Ref } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { CandidateControllerApi, type AddCandidateRequest } from '@/apis/candidate/apis';
import type { Candidate, PersonalInformation } from '@/apis/candidate/models';
import FileUpload from "@/component/FileUpload.vue";

const router = useRouter();
const route = useRoute()
const candidateApi = new CandidateControllerApi();
const attachment: Ref<File | null> = ref(null);
const agree = ref(false);
const loading = ref(false);
const fullName = ref('');
const email = ref('');
const phoneNumber = ref('');
const currentCompany = ref('');
const successDialog = ref(false);
const filepathOfBlobAttachment = ref('');
const handleFileUploaded = (data: any) => {
  const [success, filepath] = data;
  if (success) {
    console.log(filepath)
    filepathOfBlobAttachment.value = filepath;
  } else {
    // Handle failure if needed
  }
};

const submitForm = async () => {
  const candidateDetails: Candidate = {
    personalInformation: {
      name: fullName.value,
      contactInformation: {
        email: email.value,
        phoneNumber: phoneNumber.value
      }

    },
    resumePath: filepathOfBlobAttachment.value,
    currentCompany: currentCompany.value,
    contestId: Number(route.params.contestId),
    participant: true
  }
  // Create a new candidate registration object
  const request: AddCandidateRequest = {
    tenantId: 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068',
    candidate: candidateDetails
  };

  try {
    const result: Candidate = await candidateApi.addCandidate(request);
    loading.value = false;
    //Show success Message
  }
  catch (error) {
    loading.value = false;
  }
};
const navigate = (name: string) => {
  router.push({ name });
};

const closeSuccessDialog = () => {
  successDialog.value = false;
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
