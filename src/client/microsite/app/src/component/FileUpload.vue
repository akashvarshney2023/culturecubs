<template>
  <div class="d-flex align-center">
    <v-file-input
      label="Resume Attachment (docx, pdf)"
      v-model="attachment"
      accept=".docx, .pdf"
      @change="handleFileChange"
    ></v-file-input>
    <v-btn color="primary" @click="uploadFile" :disabled="!attachment">
      <v-icon>mdi-upload</v-icon>
    </v-btn>

    <v-dialog v-model="successDialog" max-width="400">
      <v-card>
        <v-card-title>Upload Success</v-card-title>
        <v-card-text>
          File uploaded successfully!
        </v-card-text>
        <v-card-actions>
          <v-btn @click="closeSuccessDialog">OK</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>


<script lang="ts">
import { BlobServiceClient } from '@azure/storage-blob';

export default {
  props: {
    loading: Boolean, 
  },
  data() {
    return {
      selectedFile: null,
      attachment: null,
      successDialog: false,
    };
  },
  methods: {
    handleFileChange(event) {
      this.selectedFile = event.target.files[0];
      if (!this.attachment) {
        this.successDialog = false; 
        // Disable the upload button when there is no attachment
        // This will also handle the case when the user cancels the selection
        this.attachment = null;
      }
    },
    async uploadFile() {
    
      const storageAccountString:string = process.env.VITE_STRG_CONNECTION_STRING;
      const containerName = process.env.VITE_STRG_CONTAINER_NAME;
      const blobName = this.selectedFile.name;
      const sasToken = process.env.VITE_STRG_SAS_TOKEN;

      // Construct the BlobServiceClient URL with the SAS token
      const blobServiceClient = new BlobServiceClient(storageAccountString);
      const containerClient = blobServiceClient.getContainerClient(containerName);
      const blockBlobClient = containerClient.getBlockBlobClient(blobName);

      try {
        await new Promise(resolve => setTimeout(resolve, 2000));
        const arrayBuffer = await this.readFileAsync(this.selectedFile);
     var result =   await blockBlobClient.uploadData(arrayBuffer, {
          blockSize: 4 * 1024 * 1024, // 4MB block size
          concurrency: 20, // 20 concurrency
          onProgress: (ev) => console.log(ev),
        });
        console.log(result);
        console.log('File uploaded successfully');
        this.successDialog = true;
      } catch (error) {
        console.error('Error uploading file:', error);
        this.successDialog = false;
      }
      finally {
        // Hide loading spinner regardless of success or failure
        this.loading = false;
      }
    },
    readFileAsync(file) {
      return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onloadend = () => resolve(reader.result);
        reader.onerror = reject;
        reader.readAsArrayBuffer(file);
      });
    },
    closeSuccessDialog() {
      this.successDialog = false;
    },
  },
};
</script>
<style scoped>
.d-flex {
  display: flex;
}
.align-center {
  align-items: center;
}

/* Add some spacing between the file input and the button */
.d-flex > *:not(:last-child) {
  margin-right: 10px; /* Adjust the value as needed */
}

</style>