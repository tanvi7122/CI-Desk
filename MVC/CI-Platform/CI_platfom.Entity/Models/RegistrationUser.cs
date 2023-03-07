using System;
using System.Collections.Generic;

namespace CI_platfom.Entity.Models;

public partial class RegistrationUser
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int PhoneNumber { get; set; }
}
