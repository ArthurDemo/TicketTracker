using TicketTracker.Entity.PrimitiveTypes;

namespace TicketTracker.Entity;

public class Ticket
{
    #region Constructors

    public Ticket()
        : this(TicketId.Default, string.Empty, string.Empty, TicketAttribute.Create(ticketType: TicketType.Default),
            null,
            new TicketStakeholder(AccountId.Default), new TicketTimeInfo(DateTimer.Now))
    {
    }

    private Ticket(TicketId id, string title, string? description,
        TicketAttribute ticketAttribute, Comment[]? comments,
        TicketStakeholder ticketStakeholder, TicketTimeInfo ticketTimeInfo)
    {
        Id = id;
        Title = title;
        Description = description;
        TicketAttribute = ticketAttribute;
        Comments = comments ?? Array.Empty<Comment>();
        TicketStakeholder = ticketStakeholder;
        TicketTimeInfo = ticketTimeInfo;
    }

    #endregion Constructors

    #region Properties

    public TicketId Id { get; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public TicketAttribute TicketAttribute { get; private set; }

    public IReadOnlyList<Comment>? Comments { get; private set; }

    public TicketStakeholder TicketStakeholder { get; private set; }

    public TicketTimeInfo TicketTimeInfo { get; private set; }

    #endregion Properties

    #region Factory Methods

    public static Ticket Create(string title, string? description, AccountId creator)
    {
        return Create(TicketId.Default, title, description, TicketAttribute.Create(ticketType: TicketType.Default),
            null,
            new TicketStakeholder(creator), new TicketTimeInfo(DateTimer.Now));
    }

    public static Ticket Create(TicketId id, string title, string? description,
        TicketAttribute ticketAttribute, Comment[]? comments,
        TicketStakeholder ticketStakeholder, TicketTimeInfo ticketTimeInfo)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));
        if (title == null) throw new ArgumentNullException(nameof(title));
        if (ticketAttribute == null) throw new ArgumentNullException(nameof(ticketAttribute));
        if (ticketStakeholder == null) throw new ArgumentNullException(nameof(ticketStakeholder));
        if (ticketTimeInfo == null) throw new ArgumentNullException(nameof(ticketTimeInfo));

        return new Ticket(id, title, description, ticketAttribute, comments, ticketStakeholder, ticketTimeInfo);
    }

    #endregion Factory Methods

    #region Public Methods

    public void EditTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(nameof(title));

        Title = title;

        TicketTimeInfo = TicketTimeInfo.Update();
    }

    public void EditDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description)) return;

        Description = description;

        TicketTimeInfo = TicketTimeInfo.Update();
    }

    public void AddComment(Comment comment)
    {
        var comments = Comments!.ToList();
        comments.Add(comment);

        Comments = comments;
    }

    public void RemoveComment(Comment comment)
    {
        var comments = Comments!.ToList();
        comments.Remove(comment);

        Comments = comments;
    }

    public void TweakLabel(IEnumerable<Label> labels)
    {
        TicketAttribute = TicketAttribute.ModifiedLabels(labels);

        TicketTimeInfo = TicketTimeInfo.Update();
    }

    public void ChangeTicketType(TicketType ticketType)
    {
        if (ticketType == null) throw new ArgumentNullException(nameof(ticketType));

        TicketAttribute = TicketAttribute.Create(ticketType: ticketType);

        TicketTimeInfo = TicketTimeInfo.Update();
    }

    public void ChangePriority(Priority priority)
    {
        if (priority == null) throw new ArgumentNullException(nameof(priority));

        TicketAttribute = TicketAttribute.Create(priority: priority);

        TicketTimeInfo = TicketTimeInfo.Update();
    }

    public void ChangeEstimationPoint(ushort estimationPoint)
    {
        TicketAttribute = TicketAttribute.Create(estimationPoint: estimationPoint);

        TicketTimeInfo = TicketTimeInfo.Update();
    }

    public void ChangeDueDate(DateTime dueDate)
    {
        TicketAttribute = TicketAttribute.Create(dueDate: dueDate);

        TicketTimeInfo = TicketTimeInfo.Update();
    }

    public void ChangeAssignees(IEnumerable<AccountId> assignees)
    {
        TicketStakeholder = TicketStakeholder.ChangeAssignees(assignees);

        TicketTimeInfo = TicketTimeInfo.Update();
    }

    #endregion Public Methods
}

public record TicketId(Guid Id) : ObjectId<TicketId>(Id)
{
    private static readonly Lazy<TicketId> DefaultValue = new(new TicketId(GuidMaker.NewGuid()));

    public static TicketId Default => DefaultValue.Value;
}

public record TicketAttribute(List<Label>? Labels, TicketType? TicketType, Priority Priority, ushort EstimationPoint,
    DateTime? DueDate)
{
    public List<Label>? Labels { get; } = Labels;

    public TicketType? TicketType { get; } = TicketType;

    public Priority Priority { get; } = Priority;

    public ushort EstimationPoint { get; } = EstimationPoint;

    public DateTime? DueDate { get; } = DueDate;

    internal static TicketAttribute Create(List<Label>? labels = null, TicketType? ticketType = null,
        Priority? priority = null,
        ushort estimationPoint = 0, DateTime? dueDate = null)
    {
        return new(labels ?? new List<Label>(), ticketType, priority ?? Priority.Default, estimationPoint, dueDate);
    }

    internal TicketAttribute ModifiedLabels(IEnumerable<Label> labels)
    {
        var labelList = labels as List<Label> ?? labels.ToList();

        return labelList.Any() == false ? this : Create(labelList);
    }
}

public record Comment(Guid Id, string Content, AccountId? CreatorId, AccountId? EditorIdId, DateTime? CreateDate,
    DateTime? ModifiedDate)
{
    #region Factory Methods

    public static Comment Create(string content, AccountId creator)
    {
        return Create(GuidMaker.NewGuid(), content, creator, null, DateTimer.Now);
    }

    public static Comment Create(Guid id, string content, AccountId? creatorId, AccountId? editorId,
        DateTime? createDate,
        DateTime? modifiedDate = null)
    {
        if (content == null) throw new ArgumentNullException(nameof(content));

        return new Comment(id, content, creatorId, editorId, createDate, modifiedDate);
    }

    #endregion Factory Methods

    #region Implements IEquatable<> Interface

    public virtual bool Equals(Comment? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.ToString().GetHashCode();
    }

    #endregion Implements IEquatable<> Interface
}

public record TicketType(Guid Id, string DisplayName) : LineItem(Id, DisplayName)
{
    private static readonly Lazy<TicketType> DefaultValue = new(new TicketType(Guid.Empty, "中"));

    public static TicketType Default => DefaultValue.Value;
}

public record Label(Guid id, string displayName) : LineItem(id, displayName)
{
}

public record Priority(Guid Id, string DisplayName) : LineItem(Id, DisplayName)
{
    private static readonly Lazy<Priority> DefaultValue = new(new Priority(Guid.Empty, "中"));

    public static Priority Default => DefaultValue.Value;
}

public record TicketStakeholder(AccountId Creator, AccountId[]? Assignees = null)
{
    public AccountId Creator { get; } = Creator ?? throw new ArgumentNullException(nameof(Creator));

    public AccountId[]? Assignees { get; private set; } = Assignees;

    public TicketStakeholder ChangeAssignees(IEnumerable<AccountId> assignees)
    {
        if (assignees == null) throw new ArgumentNullException(nameof(assignees));

        Assignees = assignees.ToArray();
        return new TicketStakeholder(Creator, Assignees);
    }
}

public record TicketTimeInfo(DateTime CreateDate, DateTime? ModifiedDate = null, DateTime? EndDate = null,
    DateTime? LeadTime = null)
{
    public TicketTimeInfo Update()
    {
        return new(CreateDate, DateTimer.Now);
    }

    public TicketTimeInfo Finished()
    {
        var currentDate = DateTimer.Now;
        return new TicketTimeInfo(CreateDate, currentDate, currentDate, CreateDate.Subtract(CreateDate.TimeOfDay));
    }
}