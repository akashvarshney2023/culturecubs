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
                <v-file-input label="Attachment" v-model="attachment" @change="handleAttachment"
                  prepend-icon="mdi-attachment"></v-file-input>
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
import { CandidateApi } from '../openapi/apis';
import type { CandidateregistrationRequest } from '../openapi/apis/CandidateApi';
import type { CandidateRegistration } from '../openapi/models';
const accountName: any = process.env.VUE_APP_STRG_ACCOUNT_NAME;
const containerName: any = process.env.VUE_APP_STRG_CONTAINER_NAME;
const accountKey: any = process.env.VUE_APP_STRG_ACCOUNT_KEY; // Keep this secure, don't expose it in frontend code (used for demonstration purposes only)

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

      // Create a new candidate registration object
      const candidateRegistration: CandidateRegistration = {
        company: company.value,
        user: user.value,
        email: email.value,
        phone: phone.value,
        url: url.value,
        attachment: attachment.value,
        agreeToTerms: agree.value,
      };

      // Create the request body
      const request: CandidateregistrationRequest = {
        body: candidateRegistration,
      };

      try {
        // Call the API to save the candidate registration
        const response = await candidateApi.candidateregistration(request);
        // Extract the candidate ID from the response
        const candidateId = response;
        if (candidateId && attachment.value) {
          // Configure the connection to the Azure Blob Storage
          const sharedKeyCredential = new StorageSharedKeyCredential(accountName, accountKey);
          const pipeline = newPipeline(sharedKeyCredential);
          const blobServiceClient = new BlobServiceClient(`https://${accountName}.blob.core.windows.net`, pipeline);

          // Upload the attachment to the specified container in the storage account
          const containerClient = blobServiceClient.getContainerClient(containerName);

          // Generate the new blob name with the candidate ID
          const originalFileName = attachment.value;
          const newBlobName = `${candidateId}_${originalFileName}`;
          const blockBlobClient = containerClient.getBlockBlobClient(newBlobName);

          try {
            await blockBlobClient.uploadData(attachment.value);
            console.log('Attachment uploaded successfully!');
          } catch (error) {
            console.error('Error uploading attachment:', error);
            // Handle error if needed
          }

          // Reset the attachment field
          attachment.value = null;

        }
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
