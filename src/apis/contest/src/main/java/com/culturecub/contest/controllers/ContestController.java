package com.culturecub.contest.controllers;

import com.culturecub.contest.entities.Contest;
import com.culturecub.contest.services.ContestService;
import io.swagger.v3.oas.annotations.Operation;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("/api/contests")
public class ContestController {

    private final ContestService contestService;

    public ContestController(ContestService contestService) {
        this.contestService = contestService;
    }

    @GetMapping
    @Operation(summary = "Get all contest")
    public List<Contest> getAllContests(@RequestHeader(value = "tenantId") String tenantId) {
        return contestService.findAll(tenantId);
    }

    @GetMapping("/{id}")
    @Operation(summary = "Get contest byu Id")
    public ResponseEntity<Contest> getContestById(@PathVariable String id,@RequestHeader(value = "tenantId") String tenantId) {
        Optional<Contest> contest = contestService.findById(id,tenantId);
        return contest.map(ResponseEntity::ok).orElse(ResponseEntity.notFound().build());
    }

    @PostMapping
    @Operation(summary = "Create contest")
    public Contest createContest(@RequestBody Contest contest,@RequestHeader(value = "tenantId") String tenantId) {
        contest.setTenantId(tenantId);
        return contestService.save(contest);
    }

    @PutMapping("/{id}")
    @Operation(summary = "Update contest")
    public ResponseEntity<Contest> updateContest(@PathVariable String id, @RequestBody Contest updatedContest) {
        Contest savedContest = contestService.updateContest(id, updatedContest);
        if (savedContest == null) {
            return ResponseEntity.notFound().build();
        }
        return ResponseEntity.ok(savedContest);
    }

    @DeleteMapping("/{id}")
    @Operation(summary = "Delete contest")
    public ResponseEntity<Void> deleteContest(@PathVariable String id) {
        contestService.deleteById(id);
        return ResponseEntity.noContent().build();
    }

    @GetMapping("/count")
    @Operation(summary = "Get contest count")
    public Integer getCount(@RequestHeader(value = "tenantId") String tenantId){
        return contestService.getCount(tenantId);
    }

    @PutMapping("/activate/{id}")
    public  Contest activate(@PathVariable String id, @RequestBody Contest updatedContest){
       return contestService.activate(id, updatedContest);
    }
}
