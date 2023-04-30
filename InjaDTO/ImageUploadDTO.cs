using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjaDTO;

public class ImageUploadDTO
{
	public int EventId { get; set; }
	public int ChallengeId { get; set; }
	public int ContenderId { get; set; }
	public int PhotographerId { get; set; }
	public string? Photo64string { get; set; }
}
