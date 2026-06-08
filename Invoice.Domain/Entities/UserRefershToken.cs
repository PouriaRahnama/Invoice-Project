namespace Invoice.Domain.Entities;
/// <summary>
/// رفرش توکن کاربر سیستم
/// </summary>
public class UserRefreshToken : BaseEntity
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedDate { get; set; }
    public string? DeviceName { get; set; }

    #region Navigation
    public User User { get; set; }
    #endregion

}