package com.texhibit.candidate;

import io.swagger.v3.oas.annotations.OpenAPIDefinition;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springdoc.core.SwaggerUiConfigParameters;
import org.springdoc.core.SwaggerUiConfigParametersBuilder;
import org.springframework.context.annotation.Bean;
import org.springdoc.core.SpringDocConfigProperties;
import org.springdoc.core.SwaggerUiConfig;

@SpringBootApplication
@OpenAPIDefinition
@EnableOpenApi
public class CandidateApplication {

	public static void main(String[] args) {
		SpringApplication.run(CandidateApplication.class, args);
	}

}
