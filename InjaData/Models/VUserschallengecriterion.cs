﻿using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VUserschallengecriterion
{
    public int? Eventid { get; set; }

    public int? Challengeid { get; set; }

    public DateTime? ChallengeStardate { get; set; }

    public DateTime? ChallengeEnddate { get; set; }

    public string? ChallengeName { get; set; }

    public int? ChallengeMinimumcontenders { get; set; }

    public int? Ecdivisionid { get; set; }

    public string? Ecddivisionname { get; set; }

    public int? Eventjudgechallengedivisionid { get; set; }

    public string? EventjudgechallengedivisionComment { get; set; }

    public DateTime? EventjudgechallengedivisionCreationdate { get; set; }

    public DateTime? EventjudgechallengedivisionUpdatedate { get; set; }

    public int? Criteriaid { get; set; }

    public string? Judgementcriterianame { get; set; }

    public int? Divisionid { get; set; }

    public decimal? Maxscore { get; set; }

    public decimal? Slotstep { get; set; }

    public int? Slotcant { get; set; }

    public string? Scorebydivision { get; set; }

    public int? Hands { get; set; }

    public short? Rounds { get; set; }

    public int? Utypeid { get; set; }

    public int? Contenderid { get; set; }

    public string? ContenderLastname { get; set; }

    public string? ContenderFirstname { get; set; }

    public int? Ueventid { get; set; }

    public int? Judgeid { get; set; }

    public string? JudgeLastname { get; set; }

    public string? JudgeFirstname { get; set; }
}
