namespace Items
{
    public class LegendaryItem : Item
    {
        public override int GetPriority()
        {
            return 995780;
        }

        public override int CategoryMinPrice()
        {
            return 300;
        }

        public override int CategoryMaxPrice()
        {
            return 1500;
        }
    }
}
