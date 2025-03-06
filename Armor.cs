namespace LearnOfClassesRPG
{
    public class Helm
    {
        public readonly string Name;
        private static float _armor;
        public static float Armor
        {
            get
            {
                return _armor;
            }
            set
            {
                if (value > 0 && value < 1)
                {
                    _armor = value;
                }
                else
                {
                    Console.WriteLine("Incorrectly set property for object Helm");
                }
            }
        }

        public Helm()
        {
            Name = "Helm";
        }
    }

    public class Shell
    {
        public readonly string Name;
        private static float _armor;
        public static float Armor
        {
            get
            {
                return _armor;
            }
            set
            {
                if (value > 0 && value < 1)
                {
                    _armor = value;
                }
                else
                {
                    Console.WriteLine("Incorrectly set property for object Helm");
                }
            }
        }

        public Shell()
        {
            Name = "Shell";
        }
    }
    public class Boots
    {
        public readonly string Name;
        private static float _armor;
        public static float Armor
        {
            get
            {
                return _armor;
            }
            set
            {
                if (value > 0 && value < 1)
                {
                    _armor = value;
                }
                else
                {
                    Console.WriteLine("Incorrectly set property for object Helm");
                }
            }
        }
        public Boots()
        {
            Name = "Boots";
        }
    }
}