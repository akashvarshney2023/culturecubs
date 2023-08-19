<template>
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
</template>
  
<script lang="ts" setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import authPlugin from '@/plugins/auth';
import { UserApi, type LoginRequest } from '@/api/tenant/apis';
const router = useRouter();

const userApi = new UserApi();
const username = ref('');
const password = ref('');

const login = async () => {
    try {
        // Create the request object
        const request: LoginRequest = {
            body: {
                userName: username.value,
                password: password.value
            }
        };
        // Use await to handle the asynchronous response
        const response = await userApi.loginRaw(request);
        const token  =  await response.value();
        console.log(response)
        console.log(token)
        if (token) {
            localStorage.setItem('token', token);
            // Redirect to a protected route after successful login
            router.push('/');
        } else {
            console.error('Token missing or login failed');
        }


    } catch (error) {
        console.error('Login error:', error);
    }
};
</script>