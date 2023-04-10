using System;
using System.Collections.Generic;

namespace CI_platform.Models;

public partial class Registration
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int PhoneNumber { get; set; }
}
