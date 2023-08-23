package com.texhibit.candidate;

import io.swagger.v3.oas.annotations.OpenAPIDefinition;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
//@OpenAPIDefinition

public class CandidateApplication {

	public static void main(String[] args) {
		SpringApplication.run(CandidateApplication.class, args);
	}

}
