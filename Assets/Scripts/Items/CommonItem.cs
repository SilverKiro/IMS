namespace Items
{
    public class CommonItem : Item
    {
        public override int GetPriority()
        {
            return 9;
        }

        public override int CategoryMinPrice()
        {
            return 5;
        }

        public override int CategoryMaxPrice()
        {
            return 250;
        }
    }
}
