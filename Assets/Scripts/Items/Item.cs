namespace Items
{
    public abstract class Item
    {
        public int Weight { get; set; }
        public int Price { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }

        public virtual double GetValue()
        {
            return GetPriority() * ( ( CategoryMaxPrice() - CategoryMinPrice() ) * 1.0 / ( CategoryMaxPrice() * 1.0 - GetPrice() + 1 ) );
        }

        protected int GetPrice()
        {
            return Price;
        }

        public virtual int GetWeight()
        {
            return Weight;
        }

        public abstract int GetPriority();

        public abstract int CategoryMinPrice();

        public abstract int CategoryMaxPrice();

        public virtual string GetName()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return GetName().GetHashCode();
        }

        public override bool Equals( object obj )
        {
            return GetName().Equals( ( (Item) obj )?.GetName() );
        }

        public override string ToString()
        {
            return GetName() + "[Price=" + GetPrice() + ", Priority=" + GetPriority() + ", Weight=" + GetWeight() + ", Value=" + GetValue() + "]";
        }
    }
}
