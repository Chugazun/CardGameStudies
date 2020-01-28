namespace CardGameTest.Entities
{
    abstract class Entity
    {
        public int MaxHp { get; protected set; }
        public int CurrentHp { get; protected set; }

        protected Entity(int hp)
        {
            MaxHp = hp;
            CurrentHp = MaxHp;
        }

        public void TakeDamage(int dmgVal)
        {
            CurrentHp -= dmgVal;
        }

        public void TakeHealing(int healVal)
        {
            CurrentHp += healVal;
        }
    }
}
