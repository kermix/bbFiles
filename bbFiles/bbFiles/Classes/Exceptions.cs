using System;

namespace bbFiles
{
    [Serializable]
    public class UserNotLoggedException : Exception
    {
        public UserNotLoggedException(string msg) : base(msg) { }
    }
    [Serializable]
    public class UndefinedUserRole : Exception
    {
        public UndefinedUserRole(string msg) : base(msg) { }
    }
    [Serializable]
    public class InsufficientPrivilegesException : Exception
    {
        public InsufficientPrivilegesException(string msg) : base(msg) { }
    }
    [Serializable]
    public class UserEditException : Exception
    {
        public UserEditException(string msg) : base(msg) { }
    }

}
