namespace FITApp.Auth.Attributes;

/// <summary>
/// Provides a user-friendly description for a permission. Is used to write a description for a permission in the database.
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