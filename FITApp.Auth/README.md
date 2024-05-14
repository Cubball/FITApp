# Auth Library
This library contains common auth-related functionality.
Primarily, it includes [extension methods](Extensions/IServiceCollectionExtensions.cs) for adding JWT authentication,
[permisison class](Data/Permissions.cs) that contains all of the available permissions in the system and
[attributes](Attributes/RequiresPermissionAttribute.cs) needed to implement permisison-based authorization.
