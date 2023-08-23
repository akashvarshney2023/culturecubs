package com.culturecub.contest.services;

import com.culturecub.contest.entities.Contest;
import com.culturecub.contest.repositories.ContestRepository;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class ContestService {

    private final ContestRepository contestRepository;

    public ContestService(ContestRepository contestRepository) {
        this.contestRepository = contestRepository;
    }

    public List<Contest> findAll(String tenantId) {
        return contestRepository.findAll(tenantId);
    }

    public Optional<Contest> findById(String id,String tenantId) {
        Optional<Contest> contest = contestRepository.findById(id,tenantId);
        return contest;
    }

    public Contest save(Contest contest) {
        return contestRepository.save(contest);
    }

    public void deleteById(String id) {
        contestRepository.deleteById(id);
    }

    public Contest updateContest(String id, Contest updatedContest) {
        if (!contestRepository.existsById(id)) {
            return null;
        }
        updatedContest.setId(id);
        Contest savedContest = contestRepository.save(updatedContest);
        return savedContest;
    }

    public Integer getCount(String tenantId) {
        return Math.toIntExact(contestRepository.count(tenantId));
    }

    public Contest activate(String id, Contest contest) {
        if (!contestRepository.existsById(id)) {
            return null;
        }
        contest.setActive(true);
        Contest savedContest = contestRepository.save(contest);
        return  savedContest;
    }
}
