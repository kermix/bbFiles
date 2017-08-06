namespace bbFiles
{
    /// <summary>
    /// Represents user's role and privileges.
    /// </summary>
    public enum Role : byte
    {
        /// <summary>
        /// The role is not proper. 
        /// </summary>
        Wrong,
        /// <summary>
        /// User is acceptor.
        /// </summary>
        Acceptor,
        /// <summary>
        /// User is worker.
        /// </summary>
        Worker,
        /// <summary>
        /// User is admin.
        /// </summary>
        Admin,

    }
    /// <summary>
    /// Marks blood type.
    /// </summary>
    public enum BloodType : byte
    {
        /// <summary>
        /// The O blood type
        /// </summary>
        O = 0,
        /// <summary>
        /// The A blood type
        /// </summary>
        A,
        /// <summary>
        /// The B blood type
        /// </summary>
        B,
        /// <summary>
        /// The AB blood type
        /// </summary>
        AB
    }
    /// <summary>
    /// Marks blood type and indicates if Rh marker is present.
    /// </summary>
    public enum BloodTypeMarker : byte
    {
        /// <summary>
        /// The O blood type. Rh marker is not present.
        /// </summary>
        O = 0,
        /// <summary>
        /// The O blood type. Rh marker is present.
        /// </summary>
        ORh,
        /// <summary>
        /// The A blood type. Rh marker is not present.
        /// </summary>
        A,
        /// <summary>
        /// The A blood type. Rh marker is present.
        /// </summary>
        ARh,
        /// <summary>
        /// The B blood type. Rh marker is not present.
        /// </summary>
        B,
        /// <summary>
        /// The B blood type. Rh marker is present.
        /// </summary>
        BRh,
        /// <summary>
        /// The AB blood type. Rh marker is not present.
        /// </summary>
        AB,
        /// <summary>
        /// The AB blood type. Rh marker is present.
        /// </summary>
        ABRh
    }
}