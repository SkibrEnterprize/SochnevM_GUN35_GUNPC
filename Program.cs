
namespace LearnOfClassesRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    /// 
    /// Класс 1. Реализовать класс Unit
    /// 
    
    public class Unit
    {
        // п.1 Поля и свойства
        private float _health;
        public float Armor;
        public string Name { get; }
        public int Damage { get; }
        public float Health { get; }

        // п.2 Конструкторы
        public Unit() : this("Unknown Unit", 5, 0.6f)
        {
        }
        public Unit(string name, int damage, float armor)
        {
            Name = name;
            Damage = damage;
            Armor = armor;
        }

        // п.3 Релизация расчетов здоровья
        public float RealHealth()
        {
            return Health * (1 + Armor);
        }

        public bool SetDamage(int damage)
        {
            float newHealth = _health - (damage * Armor); // на сколько важна разница с чем работать? Со свойством Health или сразу с полем _health? 
            if (newHealth <= 0f)
            {
                return true;
            }
            _health = newHealth;
            return false;
        }
    }

    /// 
    /// Класс 2. Реализовать класс Weapon
    /// 
    
    public class Weapon
    {
        // п.1 Свойства
        public string Name { get; }
        public int MinDamage { get; private set; }
        public int MaxDamage { get; private set; }
        public float Durability { get; }

        // п.2 Конструкторы

        public Weapon(string name)
        {
            Name = name;
            Durability = 1;
        }

        public Weapon(string name, int minDamage, int maxDamage) : this(name)
        {
            SetDamageParams(minDamage, maxDamage);
        }

        // п.3 Логика для проверки, установки и возврата заданных параметорв урона

        public void SetDamageParams(int minDamage, int maxDamage)
        {
            if (minDamage > maxDamage)
            {
                int tmp = minDamage;
                minDamage = maxDamage;
                maxDamage = tmp;
                Console.WriteLine($"Minimal Damage of {Name} is incorrect");
            }
            if (minDamage < 1)
            {
                minDamage = 1; //??? минимальный урон оружия задается по условию значением f ???
                Console.WriteLine("Getting forcing install value of MinDamage");
            }
            if (maxDamage <= 1)
            {
                maxDamage = 10;
            }

            MinDamage = minDamage;
            MaxDamage = maxDamage;

        }
        public int GetDamage()
        {
            return (MinDamage + MaxDamage) / 2;
        }

    }
}


