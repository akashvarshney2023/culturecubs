<template>
    <header class="sticky-top custom-navbar">
        <v-app-bar class="bg-white" density="default" :elevation="0"
            style="border-bottom:1px solid #e0e0e0;padding:0px 10% !important;">
            <v-app-bar-title>
                <v-img src="../../assets/images/logo.png" style="width:350px;height:54px;"></v-img>
            </v-app-bar-title>
        </v-app-bar>
    </header>
    <v-main>
        <v-container fluid>
            <v-row class="d-flex justify-center align-center" style="height: 50vh;">
                <v-col cols="6">
                    <v-card>
                        <v-card-title class="text-center">Login</v-card-title>
                        <v-card-text>
                            <v-form @submit.prevent="login">
                                <v-text-field label="Username" type="text" v-model="username" prepend-icon="mdi-account"
                                    required></v-text-field>
                                <v-text-field label="Password" type="password" v-model="password" prepend-icon="mdi-lock"
                                    required></v-text-field>
                                <v-row class="d-flex justify-center">
                                    <v-col cols="auto">
                                        <v-btn color="primary" type="submit" block>Login</v-btn>
                                    </v-col>
                                </v-row>
                            </v-form>
                        </v-card-text>
                    </v-card>
                </v-col>
            </v-row>
        </v-container>
    </v-main>
    <v-footer class="bg-green-lighten-1" style="position: fixed; bottom: 0; width: 100%;" >
    <v-row justify="center" no-gutters>     
      <v-col class="text-center mt-4" cols="12">
        {{ new Date().getFullYear() }} — <strong>Powered by Cultre Cubs</strong>
      </v-col>
    </v-row>
  </v-footer>
    <v-progress-circular :size="100" :width="7" color="green" indeterminate class="progress-overlay" v-if="showProgress"></v-progress-circular>
</template>
  
<script lang="ts" setup>
import { onBeforeMount, ref } from 'vue';
import { useRouter } from 'vue-router';
import { UserApi, type LoginRequest } from '@/api/tenant/apis';
const router = useRouter();

const userApi = new UserApi();
const username = ref('');
const password = ref('');
const showProgress = ref(false);



const login = async () => {
    try {
        // Toggle the progress circular visibility
        showProgress.value = true;

        const request: LoginRequest = {
            body: {
                userName: username.value,
                password: password.value
            }
        };
        const response = await userApi.loginRaw(request);
        const token = await response.value();

        if (token) {
            localStorage.setItem('token', token);
            // Redirect to a protected route after successful login
            router.push('/');
        } else {
            console.error('Token missing or login failed');
        }
    } catch (error) {
        console.error('Login error:', error);
    } finally {
        // Always hide the progress circular, whether login succeeds or fails
        showProgress.value = false;
    }
};
</script>
<style scoped>
.progress-overlay {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    z-index: 9999;
    /* Adjust z-index as needed */
}
</style>