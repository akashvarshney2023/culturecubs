import { BlobServiceClient, StorageSharedKeyCredential, BlobUploadCommonResponse } from "@azure/storage-blob";
import { Readable } from "stream";

interface AzureBlobUploadOptions {
  accountName: string;
  accountKey: string;
  containerName: string;
  fileName: string;
  fileContent: Buffer | Readable;
}

async function uploadFileToAzureBlob(options: AzureBlobUploadOptions): Promise<string> {
  const { accountName, accountKey, containerName, fileName, fileContent } = options;

  // Create a shared key credential for authenticating requests
  const credentials = new StorageSharedKeyCredential(accountName, accountKey);

  // Create a BlobServiceClient to interact with the Blob service
  const blobServiceClient = new BlobServiceClient(`https://${accountName}.blob.core.windows.net`, credentials);

  // Get a reference to a container
  const containerClient = blobServiceClient.getContainerClient(containerName);

  // Get a block blob client
  const blockBlobClient = containerClient.getBlockBlobClient(fileName);

  if (Buffer.isBuffer(fileContent)) {
    // If fileContent is a Buffer, directly upload
    const uploadResponse = await blockBlobClient.upload(fileContent, fileContent.length, {
      blobHTTPHeaders: {
        blobContentType: "application/octet-stream", // Adjust content type as needed
      },
    });
    return blockBlobClient.url;
  } else {
    // If fileContent is a Readable stream, create an options object
    const uploadOptions: UploadStreamToBlockBlobOptions = {
      bufferSize: 4 * 1024 * 1024, // 4MB buffer size (can be adjusted)
      blobHTTPHeaders: {
        blobContentType: "application/octet-stream", // Adjust content type as needed
      },
    };

    // Upload the stream to the block blob
    const uploadResponse: BlobUploadCommonResponse = await blockBlobClient.uploadStream(fileContent, uploadOptions);

    return blockBlobClient.url;
  }
}

// Example usage:
const options: AzureBlobUploadOptions = {
  accountName: "yourStorageAccountName",
  accountKey: "yourStorageAccountKey",
  containerName: "yourContainerName",
  fileName: "yourFileName.txt",
  fileContent: Buffer.from("Hello, Azure Blob Storage!"),
};

uploadFileToAzureBlob(options)
  .then((url) => {
    console.log(`File uploaded successfully. URL: ${url}`);
  })
  .catch((error) => {
    console.error("Error uploading file:", error.message);
  });
