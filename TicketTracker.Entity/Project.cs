using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity
{
    public class Project
    {
        #region Constructors

        public Project()
        {
            Id = ProjectId.Default;
            Name = string.Empty;
            Tickets = new List<TicketId>();
            ProjectManager = new ProjectManager();
        }

        private Project(ProjectId id, string name, ProjectManager projectManager, IReadOnlyList<TicketId>? tickets)
        {
            Id = id;
            Name = name;
            Tickets = tickets;
            ProjectManager = projectManager;
        }

        #endregion Constructors

        #region Properties

        public ProjectId Id { get; }

        public string Name { get; private set; }

        public IReadOnlyList<TicketId>? Tickets { get; private set; }

        public ProjectManager ProjectManager { get; private set; }

        #endregion Properties

        #region Factory Methods

        public static Project Create(string name, ProjectManager projectManager, List<TicketId>? ticketIds = null)
        {
            if (projectManager == null) throw new ArgumentNullException(nameof(projectManager));
            return Create(ProjectId.Default, name, projectManager, ticketIds);
        }

        public static Project Create(ProjectId projectId, string name,
            ProjectManager projectManager, List<TicketId>? ticketIds = null)
        {
            if (projectId == null) throw new ArgumentNullException(nameof(projectId));
            if (projectManager == null) throw new ArgumentNullException(nameof(projectManager));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            ticketIds ??= new List<TicketId>();

            return new Project(projectId, name, projectManager, ticketIds);
        }

        #endregion Factory Methods

        #region Public Methods

        public void AddTicket(TicketId ticketId)
        {
            if (ticketId == null) throw new ArgumentNullException(nameof(ticketId));

            var ticketIds = Tickets!.ToList();
            ticketIds.Add(ticketId);

            Tickets = ticketIds;
        }

        public void AddProjectManager(AccountId accountId)
        {
            if (accountId == null) throw new ArgumentNullException(nameof(accountId));

            ProjectManager.AddAccount(accountId);
        }

        #endregion Public Methods
    }

    public record ProjectId(Guid Id) : ObjectId<ProjectId>(Id)
    {
        private static readonly Lazy<ProjectId> DefaultValue = new(new ProjectId(Guid.Empty));

        public static ProjectId Default => DefaultValue.Value;

        public override string ToString()
        {
            return Id.ToString();
        }
    }

    public record ProjectManager
    {
        #region Constructors

        public ProjectManager()
        {
            Accounts = new List<AccountId>();
        }

        public ProjectManager(List<AccountId>? accounts)
        {
            Accounts = accounts ?? new List<AccountId>();
        }

        #endregion Constructors

        public IReadOnlyList<AccountId> Accounts { get; private set; }

        public void AddAccount(AccountId accountId)
        {
            if (accountId == null) throw new ArgumentNullException(nameof(accountId));

            var accounts = Accounts!.ToList();
            accounts.Add(accountId);

            Accounts = accounts;
        }
    }
}