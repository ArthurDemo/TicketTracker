using System.Data;
using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity
{
    public class MerchantAccount
    {
        #region Constructors

        public MerchantAccount()
        {
            Id = MerchantAccountId.Default;
            Account = AccountId.Default;
            WorkSpaces = new List<WorkSpace>();
        }

        private MerchantAccount(MerchantAccountId id, AccountId account, List<WorkSpace>? workSpaces = null)
        {
            Id = id;
            Account = account;
            WorkSpaces = workSpaces;
        }

        #endregion Constructors

        #region Properties

        public MerchantAccountId Id { get; private set; }

        public AccountId Account { get; private set; }

        public List<WorkSpace>? WorkSpaces { get; private set; }

        #endregion Properties

        public static MerchantAccount? Create(AccountId account, List<WorkSpace>? workSpaces = null)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            workSpaces ??= new List<WorkSpace>();
            return new MerchantAccount(new MerchantAccountId(GuidMaker.NewGuid()), account, workSpaces);
        }

        public static MerchantAccount Create(MerchantAccountId id, AccountId account,
            List<WorkSpace>? workSpaces = null)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (account == null) throw new ArgumentNullException(nameof(account));
            workSpaces ??= new List<WorkSpace>();

            return new MerchantAccount(id, account, workSpaces);
        }

        #region Public Methods

        public void ChangeAccount(AccountId accountId)
        {
            Account = accountId ?? throw new ArgumentNullException(nameof(accountId));
        }

        public void AddWorkSpace(WorkSpace workSpace)
        {
            if (WorkSpaces!.Any(o => o == workSpace)) throw new DuplicateNameException();

            WorkSpaces!.Add(workSpace);
        }

        public void RemoveWorkSpace(WorkSpace workSpace)
        {
            if (WorkSpaces!.Any(o => o == workSpace) == false) throw new KeyNotFoundException();

            WorkSpaces!.Remove(workSpace);
        }

        #endregion Public Methods
    }

    public record MerchantAccountId(Guid Id) : ObjectId<MerchantAccountId>(Id)
    {
        private static readonly Lazy<MerchantAccountId> DefaultValue = new(new MerchantAccountId(Guid.Empty));

        public static MerchantAccountId Default => DefaultValue.Value;
    }

    public record WorkSpace(string Name, List<ProjectId>? Projects, uint ProjectUpperLimited)
    {
        #region Properties

        public string Name { get; } = Name;

        public List<ProjectId>? Projects { get; } = Projects;

        public uint ProjectUpperLimited { get; } = ProjectUpperLimited;

        #endregion Properties

        #region Factory Methods

        public static WorkSpace Create(string name, uint projectUpperLimit)
            => Create(name, null, projectUpperLimit);

        public static WorkSpace Create(string name, List<ProjectId>? projects, uint projectUpperLimited = 3)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            return new WorkSpace(name, projects, projectUpperLimited);
        }

        #endregion Factory Methods

        #region Public Methods

        public WorkSpace ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            return Create(name, Projects, ProjectUpperLimited);
        }

        public WorkSpace AddProject(ProjectId projectId)
        {
            if (projectId == null) throw new ArgumentNullException(nameof(projectId));

            if (Projects!.Any(o => o == projectId)) throw new DuplicateNameException(nameof(projectId));

            Projects!.Add(projectId);
            return Create(Name, Projects, ProjectUpperLimited);
        }

        public WorkSpace RemoveProject(ProjectId projectId)
        {
            if (projectId == null) throw new ArgumentNullException(nameof(projectId));

            if (Projects!.Any(o => o != projectId)) throw new KeyNotFoundException(nameof(projectId));

            Projects!.Remove(projectId);
            return Create(Name, Projects, ProjectUpperLimited);
        }

        public WorkSpace ChangeProjectUpperLimited(uint upperLimited)
        {
            if (upperLimited <= 0) throw new ArgumentOutOfRangeException(nameof(upperLimited));

            return Create(Name, Projects, upperLimited);
        }

        #endregion Public Methods

        #region Implements IEquable<> Interface

        public virtual bool Equals(WorkSpace? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Projects, ProjectUpperLimited);
        }

        #endregion Implements IEquable<> Interface
    }
}