using System;
using System.Collections.Generic;

namespace InjaDTO;

public partial class GroupDTO
{
    public int Id { get; set; }

    public int Eventid { get; set; }

    public string Name { get; set; } = null!;

    public virtual EventDTO Event { get; set; } = null!;

    public virtual ICollection<UserGroupDTO> UserGroups { get; set; } = new List<UserGroupDTO>();
}
