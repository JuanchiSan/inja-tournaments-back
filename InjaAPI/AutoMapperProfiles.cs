namespace InjaAPI;

using AutoMapper;

public class AutoMapperProfiles : Profile
{
	public AutoMapperProfiles()
	{
		CreateMap<InjaData.Models.Injauser, InjaDTO.UserDTO>();
		CreateMap<InjaData.Models.City, InjaDTO.CityDTO>();
		CreateMap<InjaData.Models.Doctype, InjaDTO.DoctypeDTO>();
		CreateMap<InjaData.Models.Country, InjaDTO.CountryDTO>();
		CreateMap<InjaData.Models.Challengetype, InjaDTO.ChallengeDTO>();
		CreateMap<InjaData.Models.Challengejuzmentcriterion, InjaDTO.ChallengejuzmentcriterionDTO>();
		CreateMap<InjaData.Models.Competitiontype, InjaDTO.CompetitionDTO>();
		CreateMap<InjaData.Models.Division, InjaDTO.DivisionDTO>();
		CreateMap<InjaData.Models.Event, InjaDTO.EventDTO>();
		CreateMap<InjaData.Models.Injagroup, InjaDTO.GroupDTO>();
		CreateMap<InjaData.Models.Inscription, InjaDTO.InscriptionDTO>();
		CreateMap<InjaData.Models.Judgmentcriterion, InjaDTO.JudgmentcriterionDTO>();
		CreateMap<InjaData.Models.Point, InjaDTO.PointDTO>()
			.ForMember(x => x.eventJudgeChallengeDivisionId, y => y.MapFrom(z => z.Eventjudgechallengeid))
			.ForMember(x => x.contenderId, y => y.MapFrom(z => z.Userid))
			.ReverseMap();
		// CreateMap<InjaData.Models.Usertype, InjaDTO.UsertypeDTO>();
		CreateMap<InjaData.Models.Persongroup, InjaDTO.UserGroupDTO>();
		CreateMap<InjaData.Models.VUserschallengecriterion, InjaDTO.UserChallengeCriteriaDTO>()
			.ForMember(x => x.contenderId, y => y.MapFrom(z => z.Contenderid))
			.ForMember(x=>x.DivisionName, y=>y.MapFrom(z=> z.Ecddivisionname))
			.ReverseMap();
	}
}
