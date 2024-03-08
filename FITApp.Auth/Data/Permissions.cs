using FITApp.Auth.Attributes;

namespace FITApp.Auth.Data;

/// <summary>
/// Contains constants that represent permissions that are used in the application.
/// Each permission that is written to the database should be declared as a constant in this class and have a description. See <see cref="PermissionDescriptionAttribute"/>
/// If the permission is used but should not be written to the database, it should be declared as a constant in this class and have the <see cref="IgnoreAttribute"/> attribute.
/// </summary>
public static class Permissions
{
    [Ignore]
    public const string All = "all";

    [PermissionDescription("Створення користувачів")]
    public const string UsersCreate = "users_create";

    [PermissionDescription("Перегляд інформації про користувачів")]
    public const string UsersRead = "users_read";

    [PermissionDescription("Редагування інформації про користувачів")]
    public const string UsersUpdate = "users_update";

    [PermissionDescription("Видалення користувачів")]
    public const string UsersDelete = "users_delete";

    [PermissionDescription("Створення ролей")]
    public const string RolesCreate = "roles_create";

    [PermissionDescription("Перегляд інформації про ролі")]
    public const string RolesRead = "roles_read";

    [PermissionDescription("Редагування інформації про ролі")]
    public const string RolesUpdate = "roles_update";

    [PermissionDescription("Видалення ролей")]
    public const string RolesDelete = "roles_delete";
}