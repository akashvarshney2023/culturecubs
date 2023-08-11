package com.texhibit.candidate;

import org.springdoc.core.SwaggerUiConfigParameters;
import org.springdoc.core.SwaggerUiConfigParametersBuilder;
import org.springframework.context.annotation.Bean;
import org.springdoc.core.SpringDocConfigProperties;
import org.springdoc.core.SwaggerUiConfig;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springdoc.core.SwaggerUiConfigParametersBuilder; // Make sure this import is present
import org.springdoc.core.EnableOpenApi; // Make sure this import is present

@SpringBootApplication
@OpenAPIDefinition
@EnableOpenApi
public class CandidateApplication {

	public static void main(String[] args) {
		SpringApplication.run(CandidateApplication.class, args);
	}

}
