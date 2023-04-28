namespace InjaDTO;

public partial class UserGroupDTO
{
    public int Userid { get; set; }

    public int Groupid { get; set; }

    public DateTime Added { get; set; }

    public int Enabled { get; set; }

    public virtual GroupDTO Group { get; set; } = null!;

    public virtual UserDTO User { get; set; } = null!;
}
