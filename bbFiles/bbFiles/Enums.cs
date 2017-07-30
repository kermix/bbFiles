namespace bbFiles
{
    public enum Role : byte
    {
        Wrong,
        Acceptor,
        Worker,
        Admin,

    }
    public enum BloodType : byte
    {
        O = 0,
        A,
        B,
        AB
    }
    public enum BloodTypeMarker : byte
    {
        O = 0,
        ORh,
        A,
        ARh,
        B,
        BRh,
        AB,
        ABRh
    }
}