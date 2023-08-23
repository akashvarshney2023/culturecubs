package com.texhibit.candidate.repositories;

import com.texhibit.candidate.entities.Candidate;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.Collection;
import java.util.List;

@Repository
public interface CandidateRepository extends MongoRepository<Candidate,String> {


//    List<Candidate> findAllParticipants();

    @Query("{id: ?0,tenantId: ?1}")
    Candidate findCandidateById(String id, String tenantId);

    @Query("{tenantId: ?0}")
    List<Candidate> findAllCandidates(String tenantId);
    @Query("{tenantId: ?0}")
    Integer count(String tenantId);
    @Query("{isParticipant: true, tenantId: ?1}")
    List<Candidate> findAllParticipants(boolean isParticipant,String tenantId);
}
