namespace bbFiles
{
    static class Privileges
    {
        public static void CheckAdmin(User user)
        {
            if (!user.IsInRole("Admin"))
                throw new InsufficientPrivilegesException(Properties.Strings.InsufficientPrivileges);
        }
        public static void CheckAdminOrEmployee(User user)
        {
            if (!user.IsInRole("Admin") && !user.IsInRole("Employee"))
                throw new InsufficientPrivilegesException(Properties.Strings.InsufficientPrivileges);
        }
    }
}