using System;
using System.Collections.Generic;

namespace CI_platfom.Entity.Models;

public partial class ContactU
{
    public long UserId { get; set; }

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }
}
