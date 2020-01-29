
namespace CardGameTest.Utils
{
    public class CardName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CardName()
        {

        }

        public CardName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
