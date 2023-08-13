package com.culturecub.contest.entities;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class Timelines {
    public Integer signedUp;
    public Integer attemptStarted;
    public Integer submitted;
    public Integer reneged;
    public Integer shortlisted;
    public String winner;
}
