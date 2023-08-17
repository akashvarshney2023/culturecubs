<template>
  <v-main>
    <v-container fluid>
      <v-row>
        <v-col cols="9">
          <div class="green-base-image"> // Registraiton Image could be used as per tenant Id
          </div>
        </v-col>
        <v-col cols="3">
          <v-card>
            <v-card-title class="text-center">Register</v-card-title>
            <v-card-text>
              <v-form @submit.prevent="submitForm">
                <v-text-field label="Company" type="text" v-model="company" prepend-icon="mdi-domain"
                  required></v-text-field>
                <v-text-field label="User" type="text" v-model="user" prepend-icon="mdi-account" required></v-text-field>
                <v-text-field label="Email" type="email" v-model="email" prepend-icon="mdi-email" required></v-text-field>
                <v-text-field label="Phone" type="tel" v-model="phone" prepend-icon="mdi-phone" required></v-text-field>
                <v-text-field label="URL" type="url" v-model="url" prepend-icon="mdi-link" required></v-text-field>
               
                <v-checkbox label="I agree to the terms and conditions of Curlture Cubs and am happy to sign this"
                  v-model="agree"></v-checkbox>
                <v-row class="d-flex justify-center">
                  <v-col cols="auto">
                    <v-btn color="primary" type="submit" block>Submit</v-btn>
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

<script lang="ts">
import { defineComponent, ref, type Ref } from 'vue';
import { useRouter } from 'vue-router';
import { CandidateApi } from '@/api/microsite/apis';
import type { CandidateregistrationRequest } from '@/api/microsite/apis/CandidateApi';
import type { CandidateRegistration } from '@/api/microsite/models';
import type { AddCandidateRequest } from '@/api/candidate/apis/CandidateControllerApi';
import type { PersonalInformation, Candidate } from '@/api/candidate/models';

export default defineComponent({
  setup() {
    const router = useRouter();
    const candidateApi = new CandidateApi();

    const company = ref('');
    const user = ref('');
    const email = ref('');
    const phone = ref('');
    const url = ref('');
    const attachment: Ref<File | null> = ref(null);  // Stores the selected attachment file
    const agree = ref(false);
    const handleAttachment = (file: File) => {
      attachment.value = file;
    };
    const submitForm = async () => {
      if (attachment.value) {

      }

      //Create a new candidate regsitration details 
      const canidateDetails: Candidate = {
        personalInformation: {}

      }
      // Create a new candidate registration object
      const candidateRegistration: CandidateRegistration = {
        company: company.value,
        user: user.value,
        email: email.value,
        phone: phone.value,
        url: url.value,  
      };

    

      try {
   
     
   
        // Reset the form fields
        company.value = '';
        user.value = '';
        email.value = '';
        phone.value = '';
        url.value = '';
        attachment.value = null;
        agree.value = false;
        // Redirect to the home page or perform other actions
        navigate('home');
      } catch (error) {
        console.error('Save Failed:', error);
      }
    };

    const navigate = (name: string) => {
      router.push({ name });
    };

    return {
      company,
      user,
      email,
      phone,
      url,
      attachment,
      agree,
      handleAttachment,
      submitForm,
    };
  },
});
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
