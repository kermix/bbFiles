using System;

namespace bbFiles
{
    public class UserNotLoggedException : Exception
    {
        public UserNotLoggedException(string msg) : base(msg) { }
    }
    public class UndefinedUserRole : Exception
    {
        public UndefinedUserRole(string msg) : base(msg) { }
    }
    public class InsufficientPrivilegesException : Exception
    {
        public InsufficientPrivilegesException(string msg) : base(msg) { }
    }
    public class UserEditException : Exception
    {
        public UserEditException(string msg) : base(msg) { }
    }

}
