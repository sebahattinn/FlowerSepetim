namespace CicekSepeti.Domain.Entities;

public class User
{
    //  PROPERTYLER (Dışarıya kapalı, sadece metotlarla değişir) private set yani ab
    public int Id { get; private set; }
    public int RoleId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public byte[] PasswordHash { get; private set; }
    public byte[] PasswordSalt { get; private set; }

    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }

    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    //  PRIVATE CONSTRUCTOR (ORM ve Dapper için)
    // CS8618 Uyarılarını çözmek için "null!" ataması yapıyoruz.
    // Bu, derleyiciye "Merak etme, veritabanından dolacak bunlar" demektir.
    private User()
    {
        FirstName = null!;
        LastName = null!;
        Email = null!;
        PasswordHash = null!;
        PasswordSalt = null!;
    }

    //  FACTORY METHOD: YENİ KULLANICI (Controller Burayı Çağırıyor)
    // O garip FileSystem hatasını çözen yer burası! 
    public static User Create(string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt)
    {
        // Validasyonlar (Basit Domain Kuralları)
        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("İsim boş olamaz");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email boş olamaz");

        return new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            RoleId = 2, // default rol 2 yani admin olmayan
            IsActive = true,
            CreatedAt = DateTime.Now
        };
    }

    //  LOAD METHOD: VERİTABANINDAN YÜKLEME
    public static User Load(int id, int roleId, string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt, bool isActive, DateTime createdAt, string? refreshToken, DateTime? refreshTokenExpiryTime)
    {
        return new User
        {
            Id = id,
            RoleId = roleId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IsActive = isActive,
            CreatedAt = createdAt,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = refreshTokenExpiryTime
        };
    }

    //  DOMAIN BEHAVIORS
    public void AssignRefreshToken(string token, DateTime expiryTime)
    {
        RefreshToken = token;
        RefreshTokenExpiryTime = expiryTime;
    }
}