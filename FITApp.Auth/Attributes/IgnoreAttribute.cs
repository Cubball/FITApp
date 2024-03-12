namespace FITApp.Auth.Attributes;

/// <summary>
/// Attribute to ignore a field when extracting permissions from a class into the database.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class IgnoreAttribute : Attribute
{
}