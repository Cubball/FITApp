namespace FITApp.Auth;

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