
namespace LearnOfClassesRPG
{
    public class Unit
    {
        // п.1 Поля и свойства
        private float _health;
        public float Armor { get; }
        public string Name { get; }
        // public int Damage { get; }
        public Interval Interval { get; }
        public float Health => _health;

        // п.2 Конструкторы
        public Unit()
        {
            Name = "Unknown Unit";
            Armor = 0.6f;
        }
        public Unit(string name)
        {
            Name = name;
            //Damage = damage;
        }
        public Unit(string name, int maxDamage, int minDamage = 0) : this(name)
        {
            Interval = new Interval(minDamage, maxDamage);
        }

        // п.3 Релизация расчетов здоровья
        public float RealHealth()
        {
            return Health * (1 + Armor);
        }

        public bool SetDamage(int damage)
        {
            _health -= (damage * Armor);
            //if (newHealth <= 0f)
            //{
            //    return true;
            //}
            //_health = newHealth;

            return _health <= 0f;
        }
    }

}

