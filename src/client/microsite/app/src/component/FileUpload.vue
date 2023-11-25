<template>
  <div class="d-flex align-center">
    <v-file-input
      label="Resume Attachment (docx, pdf)"
      v-model="attachment"
      accept=".docx, .pdf"
      @change="handleFileChange"
    ></v-file-input>
    <v-btn  color="primary" @click="uploadFile">
      <v-icon>mdi-upload</v-icon>
    </v-btn>
  </div>
</template>

<script type="ts">
import { BlobServiceClient, AnonymousCredential } from '@azure/storage-blob';

export default {
  data() {
    return {
      selectedFile: null,
    };
  },
  methods: {
    handleFileChange(event) {
      this.selectedFile = event.target.files[0];
    },
    async uploadFile() {
      const accountName = process.env.VITE_STRG_ACCOUNT_NAME;
      const containerName = process.env.VITE_STRG_CONTAINER_NAME;
      const blobName = this.selectedFile.name;
      const sasToken = process.env.VITE_STRG_SAS_TOKEN;

      // Construct the BlobServiceClient URL with the SAS token
      const blobServiceClient = new BlobServiceClient(`https://saccdev001.blob.core.windows.net/candidateresumes?sp=racwdli&st=2023-11-25T17:04:47Z&se=2024-10-05T00:04:47Z&sv=2022-11-02&sr=c&sig=cYUISWYkVBgIMuRa4X7OrwSo4kC588I7FbtHQzzFw2U%3D`);
      const containerClient = blobServiceClient.getContainerClient(containerName);
      const blockBlobClient = containerClient.getBlockBlobClient(blobName);

      try {
        const arrayBuffer = await this.readFileAsync(this.selectedFile);
        await blockBlobClient.uploadData(arrayBuffer, {
          blockSize: 4 * 1024 * 1024, // 4MB block size
          concurrency: 20, // 20 concurrency
          onProgress: (ev) => console.log(ev),
        });
        console.log('File uploaded successfully');
      } catch (error) {
        console.error('Error uploading file:', error);
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