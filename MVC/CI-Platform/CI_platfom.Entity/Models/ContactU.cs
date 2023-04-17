using System;
using System.Collections.Generic;

namespace CI_platfom.Entity.Models;

public partial class ContactU
{
    public long ContactId { get; set; }

    public long UserId { get; set; }

    public string? Message { get; set; }

    public string? Subject { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
