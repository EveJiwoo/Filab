
namespace EnumDef
{
    public enum DBType
    {
        None,
        Client,
        Server,
        Both
    }

    public enum MoveDirection
    {
        None,
        Left,
        Right,
        Up,
        Down,
    }        

    public enum EventReactType
    {
        None,
        BankDepositPopup,
        BankCDAccountPopup,
        BankLoanPopup,
        ShopPopup,
    }

    public enum CityType
    {
        None = -1,

        City1,
        City2,
        City3,
        City4,
        City5,
        City6,
        City7,
        City8,
        City9,
        City10,
        City11,
        City12,
        City13,
        City14,
        City15,
        City16,
        City17,
        City18,
        City19,
        City20,

        Max
    }

    public enum LoadMap
    {
        None = -1,

        City1,
        City2,
        City3,
        City4,
        City5,
        City6,
        City7,
        City8,
        City9,
        City10,

        Bank,
        Shop,
        Home,

        World,

        Max
    }

    public enum MapPoint
    {
        None,

        Bank_Door,
        Shop_Door,
        Home_Door,

        City1_Seaport,
        City2_Seaport,
        City3_Seaport,
        City4_Seaport,
        City5_Seaport,
        City6_Seaport,
        City7_Seaport,
        City8_Seaport,
        City9_Seaport,
        City10_Seaport,
    }
}
