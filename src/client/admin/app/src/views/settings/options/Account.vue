<template>
    <v-container>
      <v-row>
        <v-col cols="6">
            <v-card class="tenant-card">
        <v-card-title>
          <h2>{{ editMode ? 'Edit Tenant Theme Settings' : 'View Tenant Theme Settings' }}</h2>
        </v-card-title>
        <v-card-text>
          <v-form ref="form" @submit.prevent="saveTenant">
          
            <v-row>
              <v-col cols="6">
                <v-text-field v-model="tenantThemePrimaryColor" label="Primary Color"></v-text-field>
                <v-icon @click="openColorPicker('tenantThemePrimaryColor')">mdi-palette</v-icon>
              </v-col>
              <v-col cols="6">
                <v-text-field v-model="tenantThemeSecondaryColor" label="Secondary Color"></v-text-field>
                <v-icon @click="openColorPicker('tenantThemeSecondaryColor')">mdi-palette</v-icon>
              </v-col>
            </v-row>
            <v-text-field v-model="tenantLogo" label="Tenant Logo"></v-text-field>
            <v-text-field v-model="tenantIcon" label="Tenant Icon"></v-text-field>
            <v-row>
              <v-col cols="6">
                <v-text-field v-model="tenantThemeBackground" label="Theme Background"></v-text-field>
                <v-icon @click="openColorPicker('tenantThemeBackground')">mdi-palette</v-icon>
              </v-col>
              <v-col cols="6">
                <v-text-field v-model="tenantThemeSurface" label="Theme Surface"></v-text-field>
                <v-icon @click="openColorPicker('tenantThemeSurface')">mdi-palette</v-icon>
              </v-col>
            </v-row>
            <v-btn @click="toggleEditMode" class="edit-button">
              {{ editMode ? 'Save' : 'Edit' }}
            </v-btn>
          </v-form>
        </v-card-text>
      </v-card>
        </v-col>
        
        <v-col cols="6">
           <v-card class="tenant-card">
          <v-card-title>
            <h2>Other Card</h2>
          </v-card-title>
          <v-card-text>
            <!-- ... Add content for the second card ... -->
          </v-card-text>
        </v-card>
        </v-col>
      </v-row>
    </v-container>
  </template>
  
  
<script setup lang="ts">
import { ref, onMounted } from 'vue';

const tenantName = ref('');
const tenantCode = ref('');
const tenantThemeBackground = ref('');
const editMode = ref(true);
const colorPickerField = ref(null);

const saveTenant = () => {
  // Save logic
};

const toggleEditMode = () => {
  editMode.value = !editMode.value;
};

const openColorPicker = (field) => {
  colorPickerField.value = field;
};

const updateColor = (color) => {
  // Update the color of the selected field with the chosen color
  if (colorPickerField.value === 'tenantThemeBackground') {
    tenantThemeBackground.value = color.hex;
  }
  // ... Similar update logic for other fields ...
};

onMounted(() => {
  // Fetch tenant data
});
</script>

<style scoped>
.tenant-card {
  margin: 10px;
  padding: 20px;
  text-align: left; /* Add this line */
}

.edit-button {
  margin-top: 10px;
}
</style>