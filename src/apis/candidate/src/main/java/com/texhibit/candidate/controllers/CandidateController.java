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

@RestController
@RequestMapping(value = "/api")
public class CandidateController {

    private final CandidateService candidateService;

    @Autowired
    private ObjectMapper objectMapper;

    public CandidateController(final CandidateService candidateService) {
        this.candidateService = candidateService;
    }

    @GetMapping(value = "/candidates")
    @CrossOrigin
    @Operation(summary = "Get all candidates")
    public List<CandidateDto> getAll(@RequestHeader(value = "tenantId") String tenantId) {
        List<CandidateDto> candidates = candidateService.getAll(tenantId);
        return candidates;
    }

    // @GetMapping(value = "/candidates")
    // @CrossOrigin 
    // @Operation(summary = "Get all candidates by contest Id")
    // public List<CandidateDto> getAllCandidateByContestId(
    //   @RequestHeader(value = "tenantId") String tenantId, 
    //   @RequestParam(value = "contestId") Integer contestId) {    
    //   List<CandidateDto> candidates = candidateService.getAllCandidatesByContestId(tenantId, contestId);    
    //   return candidates;
    
    // }
    

    @PostMapping("/candidate")
    @Operation(summary = "Add new Candidate")
    public Candidate addCandidate(@RequestBody Candidate candidate, @RequestHeader(value = "tenantId") String tenantId) throws IOException {
        candidate.setTenantId(tenantId);
        Candidate returnValue = candidateService.addCandidate(candidate);
        return returnValue;
    }

    @PostMapping("/candidates")
    @Operation(summary = "Add new Candidates in Bulk")
    public List<Candidate> addCandidates(@RequestBody List<Candidate> candidates, @RequestHeader(value = "tenantId") String tenantId) throws IOException {
    candidates.forEach(candidate -> candidate.setTenantId(tenantId));
    List<Candidate> savedCandidates = candidateService.addCandidates(candidates);    
        return savedCandidates;
    }

    @PutMapping("/candidate/{id}")
    @Operation(summary = "Update existing Candidate")
    public Candidate updateCandidate(@PathVariable String id, @RequestBody Candidate candidate, @RequestHeader(value = "tenantId") String tenantId) {
        candidate.setTenantId(tenantId);
        return candidateService.updateCandidate(id, candidate,tenantId);
    }    

    @GetMapping("/candidate/{id}")
    @Operation(summary = "Get candidate by id")
    public Candidate getCandidate(@PathVariable String id, @RequestHeader(value = "tenantId") String tenantId) {
        return candidateService.getCandidate(id, tenantId);
    }

    @DeleteMapping("/candidate/{id}")
    @Operation(summary = "Delete candidate by id")
    public String deleteCandidate(@PathVariable String id, @RequestHeader(value = "tenantId") String tenantId) {

        return candidateService.deleteCandidate(id, tenantId);
    }

    @GetMapping("/candidates/count")
    @Operation(summary = "Get candidate count")
    public Integer getCount(@RequestHeader(value = "tenantId") String tenantId) {
        return candidateService.getCount(tenantId);
    }

    @GetMapping(value = "/participants")
    @CrossOrigin
    @Operation(summary = "Get participants")
    public List<CandidateDto> getAllParticipants(@RequestHeader(value = "tenantId") String tenantId) {

        List<CandidateDto> candidates = candidateService.getAllParticipants(tenantId);
        return candidates;
    }
    
}
