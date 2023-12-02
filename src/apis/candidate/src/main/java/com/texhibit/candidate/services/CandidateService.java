package com.texhibit.candidate.services;

import com.texhibit.candidate.converters.ConvertToCandidateDto;
import com.texhibit.candidate.dtos.CandidateDto;
import com.texhibit.candidate.entities.Candidate;
import com.texhibit.candidate.repositories.CandidateRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;


import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

@Service
public class CandidateService {
    @Autowired
    private CandidateRepository candidateRepository;


    public List<CandidateDto> getAll(String tenantId) {
        List<Candidate> candidates = candidateRepository.findAllCandidates(tenantId);
        List<CandidateDto> candidateDtos = ConvertToCandidateDto.convert(candidates);
        return candidateDtos;
    }

    public Candidate addCandidate(Candidate candidate) throws IOException {
        // String parseResume =  Parser.parse(candidate.getResumePath());
        // candidate.setResumeOrCV(parseResume);

        Candidate temp = candidateRepository.save(candidate);

        return temp;
    }

    public List<Candidate> addCandidates(List<Candidate> candidates) throws IOException {
        List<Candidate> savedCandidates = new ArrayList<>();
        for (Candidate candidate : candidates) {           
            savedCandidates.add(addCandidate(candidate));
        }
        return savedCandidates;
    }

    public Candidate updateCandidate(String id, Candidate candidate,String tenantId) {
        candidate.setId(id);
        candidate.setTenantId(tenantId);
        return candidateRepository.save(candidate);
    }

    public String deleteCandidate(String id,String tenantId) {
        candidateRepository.deleteById(id);
        return "Candidate with id "+ id +" deleted successfully..";
    }

    public Candidate getCandidate(String id,String tenantId) {
        Candidate candidate = candidateRepository.findCandidateById(id,tenantId);
        return  candidate;
    }

    public Integer getCount(String tenantId) {
        return Math.toIntExact(candidateRepository.count(tenantId));
    }


    public List<CandidateDto> getAllParticipants(String tenantId) {

        List<Candidate> publishedCandidates = candidateRepository.findAllParticipants(true,tenantId);
        List<CandidateDto> candidateDtos = ConvertToCandidateDto.convert(publishedCandidates);
        return candidateDtos;

    }

}
