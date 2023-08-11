package com.texhibit.candidate.controllers;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.texhibit.candidate.dtos.CandidateDto;
import com.texhibit.candidate.entities.Candidate;
import com.texhibit.candidate.services.CandidateService;
import io.swagger.v3.oas.annotations.Operation;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.io.IOException;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping(value = "/api")
public class CandidateController {

    private final CandidateService candidateService;

    @Autowired
    private ObjectMapper objectMapper;

    public CandidateController( final CandidateService candidateService) {
        this.candidateService = candidateService;
    }

    @GetMapping(value="/candidates")
    @CrossOrigin
    @Operation(summary = "Get all canidates")
    public List<CandidateDto> getAll(){

        List<CandidateDto> candidates = candidateService.getAll();
        return candidates;
    }

    @PostMapping("/candidate")
    @Operation(summary = "Add new Candidate")
    public Candidate addProduct(@RequestBody Candidate candidate) throws IOException {

        Candidate returnValue = candidateService.addCandidate(candidate);
        return returnValue;
    }

    @PutMapping("/candidate/{id}")
    @Operation(summary = "Update existing Candidate")
    public Candidate updateCandidate(@PathVariable String id, @RequestBody Candidate candidate){
        return candidateService.updateCandidate(id, candidate);
    }

    @GetMapping("/candidate/{id}")
    @Operation(summary = "Get candidate by id")
    public Optional<Candidate> getCandidate(@PathVariable String id){
        return candidateService.getCandidate(id);
    }

    @DeleteMapping("/candidate/{id}")
    @Operation(summary = "Delete candidate by id")
    public String deleteCandidate(@PathVariable String id){

        return candidateService.deleteCandidate(id);
    }

    @GetMapping("/candidates/count")
    @Operation(summary = "Get candidate count")
    public Integer getCount(){
        return candidateService.getCount();
    }

    @GetMapping(value="/participants")
    @CrossOrigin
    @Operation(summary = "Get participants")
    public List<CandidateDto> getAllParticipants(){

        List<CandidateDto> candidates = candidateService.getAllParticipants();
        return candidates;
    }
}
