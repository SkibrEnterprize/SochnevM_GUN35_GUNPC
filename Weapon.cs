
namespace LearnOfClassesRPG
{
    public class Weapon
    {

        // п.1 Свойства
        public string Name { get; }
        public Interval Interval { get; }

        //public int MinDamage { get; private set; }
        //public int MaxDamage { get; private set; }
        public float Durability { get; }

        // п.2 Конструкторы

        public Weapon(string name)
        {
            Name = name;
            Durability = 1;
        }

        public Weapon(string name, int minDamage, int maxDamage) : this(name)
        {
            Interval = new Interval(minDamage, maxDamage);

            //SetDamageParams(minDamage, maxDamage);
        }

        // п.3 Логика для проверки, установки и возврата заданных параметорв урона

        //public void SetDamageParams(int minDamage, int maxDamage)
        //{
        //    if (minDamage > maxDamage)
        //    {
        //        (minDamage, maxDamage) = (maxDamage, minDamage);
        //        Console.WriteLine($"Minimal Damage of {Name} is incorrect");
        //    }
        //    if (minDamage < 1)
        //    {
        //        minDamage = 1;
        //        Console.WriteLine("Getting forcing install value of MinDamage");
        //    }
        //    if (maxDamage <= 1)
        //    {
        //        maxDamage = 10;
        //    }

        //    MinDamage = minDamage;
        //    MaxDamage = maxDamage;

        //}
        public int GetDamage()
        {
            return (Interval.Min + Interval.Max) / 2;
            //return (MinDamage + MaxDamage) / 2;
        }

    }

}


