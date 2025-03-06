
namespace LearnOfClassesRPG
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Подготовка к бою:\r\nВведите имя бойца:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите начальное здоровье бойца (10-100):");
            float.TryParse(Console.ReadLine(), out float health);

            Console.WriteLine("Введите значение брони шлема от 0, до 1");
            float.TryParse(Console.ReadLine(), out float armorOfHelm);
            Helm.Armor = armorOfHelm;

            Console.WriteLine("Введите значение брони кирасы от 0, до 1");
            float.TryParse(Console.ReadLine(), out float armorOfShell);
            Shell.Armor = armorOfShell;

            Console.WriteLine("Введите значение брони сапог от 0, до 1");
            float.TryParse(Console.ReadLine(), out float armorOfBoots);
            Boots.Armor = armorOfBoots;

            Unit unit = new Unit(name, health);

            Console.WriteLine($"Общий показатель брони бойца {unit.Name} равен: {unit.Armor}");
            Console.WriteLine($"Фактическое значение здоровья бойца {unit.Name} равен: {unit.RealHealth()}");
            Console.ReadKey();
        }
    }

    public class Unit
    {
        private float _health;
        private float _armor;
        public string Name { get; }
        public bool IsHaveWeapon { get; } // добавил свойство о наличии у юнита оружия для проверки в п.3.3
        public float Health => _health;
        public float Armor
        {
            get
            {
                return (float)Math.Round(_armor, 2);
            }
        }

        public Unit()
        {
            Name = "Unkniwn Unit";
            IsHaveWeapon = true;
            _armor = Helm.Armor + Shell.Armor + Boots.Armor;
        }

        public Unit(string name, float health) : this()
        {
            Name = name;
            _health = health;
        }

        public float RealHealth()
        {
            return _health * (1f + Armor);
        }

        public bool SetDamage(float damage) //добавил вместо "value" в задании, аргумент damage
        {
            _health -= (damage * Armor);
            if (_health <= 0f)
            {
                return true;
            }
            return false;
        }

        public void EquipWeapon(Weapon weapon) { }
        public void EquipHelm(Helm helm) { }
        public void EquipShell(Shell shell) { }
        public void EquipBoots(Boots boots) { }


        public float Damage
        {
            get
            {
                if (IsHaveWeapon)
                {
                    return Weapon.GetDamage() + Damage;
                }
                return Damage;
            }
            set
            {
                Damage = 5;
            }
        }

    }
    public class Weapon
    {
        public string Name { get; }

        //сделал поля статичными, чтобы GetDamage() стал доступным в классе Unit
        public static float MinDamage { get; private set; }
        public static float MaxDamage { get; private set; }
        //public float Durability { get; }

        // п.1 конструкторы

        public Weapon(string name)
        {
            Name = name;
            //Durability = 1;
        }

        public Weapon(string name, float minDamage, float maxDamage) : this(name)
        {
            SetDamageParams(minDamage, maxDamage);
        }

        // п.2 логика для проверки и установки заданных параметорв урона

        public void SetDamageParams(float minDamage, float maxDamage)
        {
            if (minDamage > maxDamage)
            {
                float tmp = minDamage;
                minDamage = maxDamage;
                maxDamage = tmp;
                Console.WriteLine($"Minimal Damage of {Name} is incorrect");
            }
            if (minDamage < 1f)
            {
                minDamage = 1f; //??? минимальный урон оружия задается значением f ???
                Console.WriteLine("Getting forcing install value of MinDamage");
            }
            if (maxDamage <= 1f)
            {
                maxDamage = 10f;
            }

            MinDamage = minDamage;
            MaxDamage = maxDamage;

        }

        // п3. логика возврата урона
        public static float GetDamage()
        {
            return (MinDamage + MaxDamage) / 2;
        }
    }    
}
