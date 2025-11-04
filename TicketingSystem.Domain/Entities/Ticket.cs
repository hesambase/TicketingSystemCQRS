using TicketingSystem.Domain.Enums;

public class Ticket
{
    public Ticket() { }
    public Guid Id { get; set; }
    public string Title { get;  set; }
    public string Description { get;  set; }
    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public TicketPriority Priority { get; set; }= TicketPriority.Medium;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public Guid? AssignedToUserId { get; set; }

    public void AssignTo(Guid adminId)
    {
        AssignedToUserId = adminId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(TicketStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
   
}


