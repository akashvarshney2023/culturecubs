package com.texhibit.candidate.services;

import com.texhibit.candidate.converters.ConvertToCandidateDto;
import com.texhibit.candidate.dtos.CandidateDto;
import com.texhibit.candidate.entities.Candidate;
import com.texhibit.candidate.repositories.CandidateRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;


import javax.persistence.EntityManager;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Predicate;
import javax.persistence.criteria.Root;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class CandidateService {
    @Autowired
    private CandidateRepository candidateRepository;

    private EntityManager entityManager;

    public List<CandidateDto> getAll() {
        List<Candidate> candidates = candidateRepository.findAll();
        List<CandidateDto> candidateDtos = ConvertToCandidateDto.convert(candidates);
        return candidateDtos;
    }

    public Candidate addCandidate(Candidate candidate) throws IOException {
        // String parseResume =  Parser.parse(candidate.getResumePath());
        // candidate.setResumeOrCV(parseResume);
        Candidate temp = candidateRepository.save(candidate);

        return temp;
    }

    public Candidate updateCandidate(String id, Candidate candidate) {
        candidate.setId(id);
        return candidateRepository.save(candidate);
    }

    public String deleteCandidate(String id) {
        candidateRepository.deleteById(id);
        return "Candidate with id "+ id +" deleted successfully..";
    }

    public Optional<Candidate> getCandidate(String id) {
        Optional<Candidate> candidate = candidateRepository.findById(id);
        return  candidate;
    }

    public Integer getCount() {
        return Math.toIntExact(candidateRepository.count());
    }


    public List<CandidateDto> getAllParticipants() {

        List<Candidate> publishedCandidates = candidateRepository.findAllParticipants();
        List<CandidateDto> candidateDtos = ConvertToCandidateDto.convert(publishedCandidates);
        return candidateDtos;

    }

}
