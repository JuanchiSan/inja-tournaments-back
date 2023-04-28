namespace InjaDTO;

public partial class InscriptionDTO
{
    public long Id { get; set; }

    public int Userid { get; set; }

    public int Eventid { get; set; }

    public DateTime Date { get; set; }

    public short? Paid { get; set; }

    public DateTime? Paiddate { get; set; }

    public decimal? Paidamount { get; set; }

    public int? Channelid { get; set; }
}
