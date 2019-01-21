namespace Items
{
    public class ExoticItem : Item
    {
        public override int GetPriority()
        {
            return 2211;
        }

        public override int CategoryMinPrice()
        {
            return 150;
        }

        public override int CategoryMaxPrice()
        {
            return 600;
        }
    }
}