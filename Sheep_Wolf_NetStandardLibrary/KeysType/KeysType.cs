namespace Sheep_Wolf_NetStandardLibrary
{
    public enum AnimalType
    {
        SHEEP,
        WOLF,
        DUCK,
        HUNTER
    }

    public enum AnimalState
    {
        DEAD,
        ALIVE,
        KILLER
    }

    public enum KillerAnnotation
    {
        WOLF_EAT_SHEEP,
        WOLF_EAT_HUNTER,
        HUNTER_KILL_WOLF
    }

    public static class Keys
    {
        public static string NAMEofANIMAL = "NAMEofANIMAL";
        public static string FOTOofANIMAL = "FOTOofANIMAL";
        public static string TYPEofANIMAL = "TYPEofANIMAL";
        public static string DEADofANIMAL = "DEADofANIMAL";
        public static string KILLERofANIMAL = "KILLERofANIMAL";
        public static string ANIMAL_ID = "ANIMAL_ID";
        public static string ENTERtheWOLF = "Волк выходит на тропу жратвы";
        public static string ENTERtheHUNTER = "Охотники рядом. Охотники здесь";
        public static string REPEATtheNAME = "Существо с таким именем уже существует.\n Измените имя";
        public static string ENTERtheNAME = "Укажите имя существа";
        public static string ENTERthePAW = "Жми ЛАПКУ и добавляй без ввода имени";
    }

    public static class Foto
    {
        public static string STAR = "star.png";
        public static string SHEEP = "sheep.png";
        public static string SHEEP_RIP = "rip.png";
        public static string WOLF = "wolf.png";
        public static string WOLF_RIP = "wolf-rip.png";
        public static string WOLF_KILLER = "killer.png";
        public static string WOLF_KILL = "wolf_kill.png";
        public static string WOLF_KILL_HUNTER = "wolf_kill_hunter.png";
        public static string HUNTER = "hunter.png";
        public static string HUNTER_RIP = "hunter-rip.png";
        public static string HUNTER_KILLER = "hunter_killer.png";
        public static string HUNTER_KILL_WOLF = "hunter_kill_wolf.png";
        public static string DUCK = "duck.png";
        public static string INFO = "INFORMATION.png";

    }
}
