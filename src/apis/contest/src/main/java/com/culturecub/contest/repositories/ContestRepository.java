package com.culturecub.contest.repositories;

import com.culturecub.contest.entities.Contest;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface ContestRepository extends MongoRepository<Contest,String> {
    @Query("{id: ?0,tenantId: ?1}")
    Contest findCandidateById(String id, String tenantId);

    @Query("{tenantId: ?0}")
    List<Contest> findAll(String tenantId);
    @Query("{tenantId: ?0}")
    Integer count(String tenantId);
    @Query("{isParticipant: true, tenantId: ?1}")
    Optional<Contest> findById(String id, String tenantId);
}
