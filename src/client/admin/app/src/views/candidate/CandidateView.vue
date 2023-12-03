<template>
  <v-main>
    <v-card flat>
      <v-card-title>
        <span style="text-align: center; font-size: xx-large;">
          <v-icon icon="mdi-account-multiple"></v-icon> Candidates &nbsp;
        </span>
        <v-btn outlined style="text-align: right; font-size: large; margin-left: 73%;" color="primary"
          @click="openbulkUploadFileDialog()" prepend-icon="mdi-file-upload">
          Upload bulk
        </v-btn>
      </v-card-title>
      <v-card v-if="candidates.listItems.length">
        <v-card-title>
          <v-text-field v-model="candidates.search" append-icon="mdi-magnify" label="Search" single-line
            hide-details></v-text-field>
        </v-card-title>
        <v-data-table :headers="candidates.headers" :items="candidates.listItems" :search="candidates.search" show-select
          :dense="candidates.dense">
          <template v-slot:item.actions="{ item }" style="align-items:end;">
            <div v-if="item.raw.attachment == null || item.raw.attachment === ''">
              <v-icon color="error">mdi-close-circle</v-icon>
            </div>
            <div v-if="item.raw.attachment">
              <v-icon color="primary" @click="viewOrDownloadAttachment(item.raw.attachment)">mdi-download</v-icon>
            </div>
          </template>
          <template v-slot:item.tagContest="{ item }">
            <v-icon color="success" @click="openDialog()">mdi-tag</v-icon>
            <v-row justify="center">
              <v-dialog v-model="dialog" scrollable width="auto" style="opacity: 0.3; !important">
                <v-card flat>
                  <v-card-title>Select Contest</v-card-title>
                  <v-divider></v-divider>
                  <v-card-text style="height: 400px;">
                    <v-radio-group v-model="dialogm1" column>
                      <v-radio v-for="item in result" :key="item.id" :label="item.title" :value="item.id"></v-radio>
                    </v-radio-group>
                  </v-card-text>
                  <v-divider></v-divider>
                  <v-card-actions>
                    <v-btn color="primary" variant="text" @click="closeDialog">
                      Close
                    </v-btn>
                    <v-btn color="primary" variant="text" @click="tagCandidateToContest(item.value)">
                      Save
                    </v-btn>
                  </v-card-actions>
                </v-card>
              </v-dialog>
            </v-row>
          </template>
        </v-data-table>
      </v-card>
      <v-card v-else>
        <v-skeleton-loader class="mx-auto border" min-width="2000"
          type="table-thead,table-tbody,table-row-divider,table-row"></v-skeleton-loader>
      </v-card>
    </v-card>
    <v-dialog v-model="successDialog" max-width="400">
      <v-card>
        <v-card-title>Candiate Tagging</v-card-title>
        <v-card-text color="green">
          Canidate is tag to the contest successfully.
        </v-card-text>
        <v-card-actions>
          <v-btn @click="closeSuccessDialog">OK</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- File input dialog -->
    <v-dialog v-model="excelFileUploadDialog" max-width="600">
      <v-card>
        <v-card-title>
          <span style="text-align: left;">
            Select File
          </span>
          <span>
            <v-card-actions>
              <v-btn style="text-align: right; margin-left: 56%;" color="primary"
                @click="downloadBulkCandidateUploadTemplate()" prepend-icon="mdi-file-upload"> Download Template
              </v-btn>
            </v-card-actions>
          </span>

        </v-card-title>
        <v-card-text>
          <v-file-input v-model="uploadExcelSelectFile" label="Choose only excel file" accept=".xls,.xlsx"
            @change="handleFileChange"></v-file-input>
        </v-card-text>
        <v-card-actions>
          <v-btn prepend-icon="mdi-close-circle" @click="closeExcelFileUploadDialog">Cancel</v-btn>
          <v-btn prepend-icon="mdi-file-upload" @click="bulkUploadCandidate" color="primary">Upload</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <div v-if="showProgress">

    </div>
  </v-main>
  <v-progress-circular :size="100" :width="7" color="green" indeterminate class="progress-overlay"
    v-if="showProgress"></v-progress-circular>
</template>
  
<script lang="ts" setup>
import { CandidateControllerApi, type AddCandidateRequest, type CandidateDto, type GetAllRequest, type UpdateCandidateRequest, type GetCandidateRequest, type Candidate, type AddCandidatesRequest } from '@/api/candidate';
import { ContestApi, type Contest, type GetcontestsbytenantidRequest } from '@/api/microsite';
import { onMounted, ref, type Ref } from 'vue'
import * as XLSX from 'xlsx';
import { useRouter } from 'vue-router'
const canidateDetails = ref<CandidateDto[]>([]);
const candidateApi = new CandidateControllerApi();
const contestAPI = new ContestApi();
const result: Ref<Contest[]> = ref([]);
const dialog = ref(false)
const dialogm1 = ref<string>("");
const successDialog = ref(false);
const uploadExcelSelectFile: Ref<File[]> = ref([]);
const excelFileUploadDialog = ref(false);
const candidateUploadList: Ref<Candidate[]> = ref([]);
const showProgress = ref(false);
const candidates = ref({
  dense: "true",
  search: '',
  headers: [
    { align: 'start', key: 'name', sortable: false, title: 'Name' },
    { align: 'start', key: 'phone', title: 'Phone' },
    { align: 'start', key: 'email', title: 'Email' },
    { align: 'start', key: 'gender', title: 'Gender' },
    { align: 'start', key: 'dob', title: 'DOB' },
    { align: 'center', key: 'actions', title: 'Download/View Resume', width: 100 },
    { align: 'center', key: 'tagContest', title: 'Tag To Contest', width: 180 }
  ],
  listItems: canidateDetails
});
const openDialog = async () => {
  dialog.value = true;
  try {
    const request: GetcontestsbytenantidRequest = {
      guid: 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068'
    };
    // Assuming result is a ref
    // You need to define and initialize result somewhere in your component
    result.value = await contestAPI.getcontestsbytenantid(request);
  } catch (error) {
    console.log(error);
  }
};

const getAllCandidatesByTenantId = async () => {
  try {
    const request: GetAllRequest = {
      tenantId: 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068'
    }
    const data: CandidateDto[] = await candidateApi.getAll(request);
    canidateDetails.value = data
  } catch (error) {
    console.log(error);
  }
};
const viewOrDownloadAttachment = (blobUrl: string) => {

  const sasToken = 'sp=r&st=2023-12-01T00:04:45Z&se=2024-02-03T08:04:45Z&sv=2022-11-02&sr=c&sig=k9%2BrmVYmQseQQ3OhpBAMQc%2BYazwg1eYG9GrjgrxAV%2FU%3D';
  const urlWithSAS = `${blobUrl}?${sasToken}`;

  window.open(urlWithSAS, '_blank');
};
const closeDialog = () => {
  dialog.value = false;
};
const tagCandidateToContest = async (candidateId: string) => {
  try {
    // get canidate by id
    const candidateRequest: GetCandidateRequest = {
      id: candidateId,
      tenantId: 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068'
    }
    const candidateResponse: Candidate = await candidateApi.getCandidate(candidateRequest);

    const candidateDetails: Candidate = {
      personalInformation: {
        name: candidateResponse.personalInformation?.name,
        contactInformation: {
          email: candidateResponse.personalInformation?.contactInformation?.email,
          phoneNumber: candidateResponse.personalInformation?.contactInformation?.phoneNumber
        }

      },
      resumePath: candidateResponse.resumePath,
      currentCompany: candidateResponse.currentCompany,
      contestId: Number(dialogm1.value),
      participant: true
    }
    const request: UpdateCandidateRequest = {
      id: candidateId,
      tenantId: 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068',
      candidate: candidateDetails
    }
    const updateCandidate = await candidateApi.updateCandidate(request);
    if (updateCandidate) {
      closeDialog();
      successDialog.value = true;
    }
  } catch (error) {
    console.log(error);
  }
};
const closeSuccessDialog = () => {
  successDialog.value = false;
};



const openbulkUploadFileDialog = () => {
  excelFileUploadDialog.value = true;
}

const closeExcelFileUploadDialog = () => {
  excelFileUploadDialog.value = false;
}
const downloadBulkCandidateUploadTemplate = () => {
  const templateUrl = './docs/bulk-candidate-upload.xlsx';
  window.location.href = templateUrl;
}

const bulkUploadCandidate = async () => {
  //close upload dialog
  closeExcelFileUploadDialog();
  //show loading
  showProgress.value = true;
  try {
    const request: AddCandidatesRequest = {
      tenantId: 'B97684C9-7ACD-40DC-80AC-42F1D0E2F068',
      candidate: candidateUploadList.value
    }
    const data: CandidateDto[] = await candidateApi.addCandidates(request);
    if (data.length > 0) {

      //close loading
      showProgress.value = false;
      //success message
      alert("Candiate Uploaded succuessfully");
    }

  } catch (error) {
    console.log(error);
  }


}

const handleFileChange = (event: Event) => {
  const file = (event.target as HTMLInputElement).files?.[0];

  if (file) {
    readExcelFile(file);
  }
};

const readExcelFile = (file: File) => {
  const reader = new FileReader();
  reader.readAsBinaryString(file);
  reader.onload = (e) => {
    const data = (e.target as FileReader).result;
    const workbook = XLSX.read(data, { type: 'binary' });

    // Assuming the sheet name is 'Sheet1'
    const sheetName = workbook.SheetNames[0];
    const sheet = workbook.Sheets[sheetName];

    // Parse rows and create a collection of Candidate objects
    const candidateCollection: Candidate[] = XLSX.utils.sheet_to_json(sheet)
      .map((row: any) => ({
        personalInformation: {
          name: row.FullName,
          contactInformation: {
            email: row.EmailAddress,
            phoneNumber: row.PhoneNumber
          }
        },
        currentCompany: row.CurrentCompany,
        contestId: Number(row.ContestId),
        participant: true
      }));

    candidateUploadList.value = candidateCollection;
  };

  //reader.readAsBinaryString(file);
};

onMounted(async () => {
  await getAllCandidatesByTenantId();
});
</script>
  