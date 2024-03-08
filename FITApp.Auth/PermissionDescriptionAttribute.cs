namespace FITApp.Auth;

/// <summary>
/// Provides a user-friendly description for a permission.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class PermissionDescriptionAttribute : Attribute
{
    public PermissionDescriptionAttribute(string description)
    {
        Description = description;
    }

    public string Description { get; }
}