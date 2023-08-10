package com.texhibit.candidate.repositories;

import com.texhibit.candidate.entities.Candidate;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.Collection;
import java.util.List;

@Repository
public interface CandidateRepository extends MongoRepository<Candidate,String> {

    @Query("{'isParticipant':true}")
    List<Candidate> findAllParticipants();
}
