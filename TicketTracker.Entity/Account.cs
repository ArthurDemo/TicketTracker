using System.Text.RegularExpressions;

using TicketTracker.Entity.Exceptions;
using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity;

public class Account
{
    #region Constructors

    public Account()
    {
        Id = AccountId.Default;
        Email = string.Empty;
        Password = string.Empty;
        CreateDate = DateTimer.Now;
        ModifiedDate = null;
    }

    private Account(AccountId id, string email, string password, DateTime createDate, DateTime? modifiedDate)
    {
        Id = id;
        Email = email;
        Password = password;
        CreateDate = createDate;
        ModifiedDate = modifiedDate;
    }

    #endregion Constructors

    #region Properties

    public AccountId Id { get; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public DateTime CreateDate { get; }

    public DateTime? ModifiedDate { get; private set; }

    #endregion Properties

    #region Factory Methods

    public static Account Create(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

        return Create(email, password, DateTimer.Now);
    }

    public static Account Create(string email, string password, DateTime createDate, DateTime? modifiedDate = null)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

        return new Account(AccountId.Default, email, password, DateTimer.Now, modifiedDate);
    }

    #endregion Factory Methods

    #region Public Methods

    public void ChangeEmail(string email)
    {
        EmailFormatIsIncorrectException.ThrowIfNotMatch(email,
            entity => string.IsNullOrWhiteSpace(entity) == false ||
                      Regex.IsMatch(entity, @" ^[^@\s] +@[^@\s] +\.[^@\s] + $"));

        Email = email;
        ModifiedDate = DateTimer.Now;
    }

    public void ChangePassword(string password)
    {
        PasswordFormatIsIncorrectException.ThrowIfNotMatch(password,
            entity => string.IsNullOrWhiteSpace(password) == false);

        Password = password;
        ModifiedDate = DateTimer.Now;
    }

    #endregion Public Methods
}

public record AccountId(Guid Id) : ObjectId<AccountId>(Id)
{
    private static readonly Lazy<AccountId> DefaultValue = new(new AccountId(GuidMaker.NewGuid()));

    public static AccountId Default => DefaultValue.Value;
}