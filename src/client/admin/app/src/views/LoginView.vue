<template>
    <v-main>
        <v-container fluid>
            <v-row class="d-flex justify-center align-center" style="height: 100vh;">
                <v-col cols="auto">
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
</template>
  
<script lang="ts" setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import authPlugin from '@/plugins/auth';
import { UserApi, type ApiUserLoginPostRequest } from '@/api/user';
import { type Userlogin } from '@/api/user/models'
const router = useRouter();

const userApi = new UserApi();
const username = ref('');
const password = ref('');

const login = async () => {
    try {
        // Create the request object
        const request: ApiUserLoginPostRequest = {
            userlogin: {
                userName: username.value,
                password: password.value
            }
        };
         // Use await to handle the asynchronous response
    const response = await userApi.apiUserLoginPost(request);

       

    } catch (error) {
        console.error('Login error:', error);
    }
};
</script>