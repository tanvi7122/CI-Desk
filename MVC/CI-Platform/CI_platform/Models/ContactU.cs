using System;
using System.Collections.Generic;

namespace CI_platform.Models;

public partial class ContactU
{
    public long UserId { get; set; }

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
