namespace Items
{
    public class QuestItem : Item
    {
        public override double GetValue()
        {
            return GetPriority() * 1.0;
        }

        public override int GetPriority()
        {
            return int.MaxValue;
        }

        public override int CategoryMinPrice()
        {
            return 0;
        }

        public override int CategoryMaxPrice()
        {
            return GetPriority();
        }
    }
}
