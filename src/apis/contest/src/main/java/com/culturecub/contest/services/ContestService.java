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

    public List<Contest> findAll() {
        return contestRepository.findAll();
    }

    public Optional<Contest> findById(String id) {
        Optional<Contest> contest = contestRepository.findById(id);
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

    public Integer getCount() {
        return Math.toIntExact(contestRepository.count());
    }

    public void activate(String id, Contest contest) {

        contest.setActive(true);
        contestRepository.save(contest);
    }
}
