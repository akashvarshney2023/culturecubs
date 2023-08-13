package com.culturecub.contest.entities;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.stereotype.Indexed;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Document(collection = "contest")
@Indexed
public class Contest {
    @Id
    private String id;
    private String title;
    private String description;
    private String startDate;
    private String endDate;
    private String problemSattement;
    private String eligibilityCriteria;
    private String submissionProcess;
    private String faq;
    private Timelines timelines;
    private Participants participants;
    private String tenantId;
    private boolean isActive;
}
