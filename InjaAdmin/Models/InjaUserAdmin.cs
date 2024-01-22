using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InjaData.Models;

namespace InjaAdmin.Models;

public class InjauserAdmin
{
  public static InjauserAdmin FromDbInjaUser(Injauser? dbItem)
  {
    if (dbItem == null)
      return new InjauserAdmin();
    var iuadmin = new InjauserAdmin
    {
      Id = dbItem.Id,
      Docnumber = dbItem.Docnumber,
      Docid = dbItem.Docid,
      Firstname = dbItem.Firstname,
      Lastname = dbItem.Lastname,
      Cityid = dbItem.Cityid,
      Mail = dbItem.Mail,
      Nickname = dbItem.Nickname,
      Street = dbItem.Street,
      Number = dbItem.Number,
      Phone = dbItem.Phone,
      Active = dbItem.Active,
      Urlphoto = dbItem.Urlphoto,
      Language = dbItem.PreferredLanguage,
      
      Pass = dbItem.Pass,
      Pass1 = dbItem.Pass
    };
    return iuadmin;
  }

  public static Injauser ToDbInjaUser(InjauserAdmin memUser, Injauser? dbUser = null)
  {
    dbUser ??= new Injauser();

    dbUser.Id = memUser.Id;
    dbUser.Docnumber = memUser.Docnumber;
    dbUser.Docid = memUser.Docid;
    dbUser.Firstname = memUser.Firstname;
    dbUser.Lastname = memUser.Lastname;
    dbUser.Cityid = memUser.Cityid;
    dbUser.Mail = memUser.Mail;
    dbUser.Nickname = memUser.Nickname;
    dbUser.Street = memUser.Street;
    dbUser.Number = memUser.Number;
    dbUser.Phone = memUser.Phone;
    dbUser.Active = memUser.Active;
    dbUser.Urlphoto = memUser.Urlphoto;
    dbUser.Pass = memUser.Pass;
    dbUser.PreferredLanguage = memUser.Language;

    return dbUser;
  }

  public int Id { get; set; }

  [Required, EmailAddress]
  [UniqueEmailValidation]
  public string Mail { get; set; } = string.Empty;

  [Required, PasswordPropertyText] public string Pass { get; set; } = string.Empty;
  [Required, PasswordPropertyText] public string Pass1 { get; set; } = string.Empty;

  [Required] [MinLength(1)] public string Lastname { get; set; } = string.Empty;

  [Required] [MinLength(1)] public string Firstname { get; set; } = string.Empty;

  [Required] [MinLength(1)] public string Docnumber { get; set; } = string.Empty;
  [Required] public short Docid { get; set; }

  public string? Street { get; set; }

  public string? Number { get; set; }

  public string? Phone { get; set; }
  [Required] public int? Cityid { get; set; }

  [Required] public bool? Active { get; set; } = true;

  public string? Urlphoto { get; set; }

  public string Name => $"{Lastname}, {Firstname}";

  public string? Nickname { get; set; }
  public string Language { get; set; } = Helper.LengEnglish;
}

[AttributeUsage(AttributeTargets.Property)]
public class UniqueEmailValidation : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    var result = ValidationResult.Success;
    if (value == null) return result;

    var db = new InjaData.Models.dbContext();
    var memItem = validationContext.ObjectInstance as InjauserAdmin;

    var cond = db.Injausers.ToList().FirstOrDefault(dbItem =>
      memItem != null && memItem.Id != dbItem.Id &&
      Convert.ToString(dbItem.Mail).ToLowerInvariant().Trim() == Convert.ToString(value)!.ToLowerInvariant().Trim());
    return cond == null
      ? result
      : new ValidationResult("Email Already Exists", new[] { validationContext.MemberName! });
  }
}

[AttributeUsage(AttributeTargets.Property)]
public class UniqueDoc : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    var result = ValidationResult.Success;
    if (value == null) return result;

    var db = new InjaData.Models.dbContext();
    var memItem = validationContext.ObjectInstance as InjauserAdmin;

    var cond = db.Injausers.ToList().FirstOrDefault(dbItem =>
      memItem != null && memItem.Id != dbItem.Id &&
      Convert.ToString(dbItem.Docnumber).ToLowerInvariant().Trim() == Convert.ToString(value)!.ToLowerInvariant().Trim() &&
      dbItem.Docid == memItem.Docid);
    return cond == null
      ? result
      : new ValidationResult("DocType/DocNumber Already Exists", new[] { validationContext.MemberName! });
  }
}

[AttributeUsage(AttributeTargets.Property)]
public class CheckPass : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    var result = ValidationResult.Success;
    if (value == null) return result;
    var memItem = validationContext.ObjectInstance as InjauserAdmin;

    return memItem == null || Convert.ToString(memItem.Pass) != Convert.ToString(memItem.Pass1) ? new ValidationResult("Password Not Matched", new[] { validationContext.MemberName! }) : result;
  }
}