package com.culturecub.contest.repositories;

import com.culturecub.contest.entities.Contest;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface ContestRepository extends MongoRepository<Contest,String> {
}
