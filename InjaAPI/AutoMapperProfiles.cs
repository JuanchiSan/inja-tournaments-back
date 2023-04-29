﻿namespace InjaAPI;

using AutoMapper;

public class AutoMapperProfiles : Profile
{
	public AutoMapperProfiles()
	{
		CreateMap<InjaData.Models.Ninjauser, InjaDTO.UserDTO>();
		CreateMap<InjaData.Models.City, InjaDTO.CityDTO>();
		CreateMap<InjaData.Models.Doctype, InjaDTO.DoctypeDTO>();
		CreateMap<InjaData.Models.Country, InjaDTO.CountryDTO>();
		CreateMap<InjaData.Models.Challenge, InjaDTO.ChallengeDTO>();
		CreateMap<InjaData.Models.Challengejuzmentcriterion, InjaDTO.ChallengejuzmentcriterionDTO>();
		CreateMap<InjaData.Models.Competitiontype, InjaDTO.CompetitionDTO>();
		CreateMap<InjaData.Models.Division, InjaDTO.DivisionDTO>();
		CreateMap<InjaData.Models.Event, InjaDTO.EventDTO>();
		CreateMap<InjaData.Models.Ninjagroup, InjaDTO.GroupDTO>();
		CreateMap<InjaData.Models.Inscription, InjaDTO.InscriptionDTO>();
		CreateMap<InjaData.Models.Judgmentcriterion, InjaDTO.JudgmentcriterionDTO>();
		CreateMap<InjaData.Models.Point, InjaDTO.PointDTO>();
		CreateMap<InjaData.Models.Usertype, InjaDTO.UsertypeDTO>();
		CreateMap<InjaData.Models.Persongroup, InjaDTO.UserGroupDTO>();
	}
}