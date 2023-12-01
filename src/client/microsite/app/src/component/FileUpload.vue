<template>
  <div class="d-flex align-center">
    <v-file-input label="Resume Attachment (docx, pdf)" v-model="attachment" accept=".docx, .pdf"
      @change="handleFileChange"></v-file-input>
    <v-btn :disabled="isUploading || !attachment" :loading="isUploading" color="primary" @click="uploadFile">
      <v-icon>mdi-upload</v-icon>
    </v-btn>
    <v-dialog v-model="isUploading" :scrim="true" persistent width="200px">
      <v-card color="primary">
        <v-card-text>
          Please stand by..
          <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>


<script lang="ts">
import { BlobServiceClient } from '@azure/storage-blob';
export default {
  props: {
    isUploading: Boolean,
    filePath: Array,
    phoneNumber:String
  },
  data() {
    return {
      isUploading: false,
      completeUrl: '',
      selectedFile: null,
      attachment:null ,
      successDialog: false,
    };
  },
  methods: {
    handleFileChange(event:any) {
      this.selectedFile = event.target.files[0];
      if (!this.attachment) {
        this.successDialog = false;
        //TODO: Disable the upload button when there is no attachment
        // This will also handle the case when the user cancels the selection
        this.attachment = null;
      }
    },
    async uploadFile() {
      console.log(this.phoneNumber)
      this.isUploading = true;
      const accountName = "saccdev001";
      const containerName = "candidateresumes"
      const storageAccountString = process.env.VITE_STRG_ACCOUNT_KEY;
      const fileExtension = this.selectedFile.name.split('.').pop();
      const blobName = this.selectedFile ? `${this.phoneNumber}.${fileExtension}`: this.selectedFile.name;
      const blobServiceClient = new BlobServiceClient(storageAccountString);
      const containerClient = blobServiceClient.getContainerClient(containerName);
      const blockBlobClient = containerClient.getBlockBlobClient(blobName);

      try {
        await new Promise(resolve => setTimeout(resolve, 2000));
        const arrayBuffer: any = await this.readFileAsync(this.selectedFile);
        await blockBlobClient.uploadData(arrayBuffer, {
          blockSize: 4 * 1024 * 1024, // 4MB block size
          concurrency: 20, // 20 concurrency
        });

        // Emit the event with an array containing confirmation and filepath
        // Construct the complete URL
        const completeUrl = `https://${accountName}.blob.core.windows.net/${containerName}/${blobName}`;

        this.$emit('file-uploaded', [true, completeUrl]);
        this.isUploading = false;
        this.successDialog = true;
      } catch (error) {
        console.error('Error uploading file:', error);
        // Emit the event with an array containing false and an empty string
        this.$emit('file-uploaded', this.completeUrl);
        this.successDialog = false;
        this.isUploading = false;
      }
      finally {
        this.isUploading = false;
      }
    },
    readFileAsync(file: any) {
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
.d-flex>*:not(:last-child) {
  margin-right: 10px;
  /* Adjust the value as needed */
}
</style>