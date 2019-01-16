using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Item woodenSword = new NormalItem { Name = "Wooden Sword", Price = 100, Weight = 40 };
            Item woodenHelmet = new NormalItem { Name = "Wooden Helmet", Price = 150, Weight = 50 };
            Item witchEyes = new QuestItem { Name = "Witch Eyes", Price = 4, Weight = 60 };
            Inventory chest = new Inventory();
            chest.AddItem(new Money { Price = 300 });
            chest.RemoveItem(new Money { Price = 150 });
            chest.AddItem(woodenSword);
            chest.AddItem(woodenSword);
            chest.AddItem(witchEyes);


            Inventory inventory = new Inventory();
            inventory.AddItem(new Money { Price = 900 });
            inventory.RemoveItem(new Money { Price = 63 });
            inventory.AddItem(woodenHelmet);


            MaximizeTotalValue(chest, inventory);
            Console.ReadKey();
        }

        public static void MaximizeTotalValue(Inventory chest, Inventory inventory)
        {
            Console.WriteLine(inventory);
            Console.WriteLine(chest);

            Item[] items = new Item[chest.TotalItems];
            int globCounter = 0;
            foreach (KeyValuePair<Item, int> kvp in chest.Items)
            {
                int counter = kvp.Value;
                while (counter-- > 0)
                {
                    items[globCounter++] = kvp.Key;
                }
            }

            int capacity = inventory.Capacity - inventory.TotalWeight;
            long[,] knapsack = new long[items.Length + 1, capacity + 1];
            int[,] keep = new int[items.Length + 1, capacity + 1];
            for (int itemIdx = 0; itemIdx <= items.Length; itemIdx++)
            {
                var currentItem = itemIdx == 0 ? null : items[itemIdx - 1];
                for (int currentCapacity = 0; currentCapacity <= capacity; currentCapacity++)
                {
                    if (currentItem == null)
                    {
                        knapsack[itemIdx, currentCapacity] = 0;
                        keep[itemIdx, currentCapacity] = 0;
                    }
                    else if (currentItem.GetWeight() <= currentCapacity)
                    {
                        long memoized = currentItem.GetValue() + knapsack[itemIdx - 1, currentCapacity - currentItem.GetWeight()];
                        knapsack[itemIdx, currentCapacity] = Math.Max(
                            memoized,
                            knapsack[itemIdx - 1, currentCapacity]);
                        if (knapsack[itemIdx, currentCapacity] == memoized)
                        {
                            keep[itemIdx, currentCapacity] = 1;
                        }
                        else
                        {
                            keep[itemIdx, currentCapacity] = 0;
                        }
                    }
                    else
                    {
                        knapsack[itemIdx, currentCapacity] = knapsack[itemIdx - 1, currentCapacity];
                        keep[itemIdx, currentCapacity] = 0;
                    }
                }
            }

            int itemIdx_ = items.Length;
            List<Item> gathered = new List<Item>();
            while (capacity >= 0 && itemIdx_ > 0)
            {
                if (keep[itemIdx_, capacity] == 1)
                {
                    Item item = items[itemIdx_ - 1];
                    inventory.AddItem(item);
                    chest.RemoveItem(item);
                    capacity -= item.Weight;
                }
                itemIdx_--;
            }

            Console.WriteLine(inventory);
            Console.WriteLine(chest);
        }
    }

    class Inventory
    {
        public int Capacity = 150;
        public int TotalWeight = 0;
        public int TotalItems = 0;
        public Money Money;
        public Dictionary<Item, int> Items = new Dictionary<Item, int>();

        public Inventory()
        {
            Money money = new Money { Price = 0 };
            this.Money = money;
            this.Items.Add(money, 1);
            this.TotalItems += 1;
        }


        public void AddItem(Item item)
        {
            int defVal = 0;
            if (item.GetWeight() + this.TotalWeight > this.Capacity)
            {
                throw new Exception("Inventory is full!");
            }
            else if (item is Money)
            {
                this.Money.Price += item.Price;
            }
            else if (this.Items.TryGetValue(item, out defVal))
            {
                this.Items.Remove(item);
                this.Items.Add(item, defVal + 1);
                this.TotalItems += 1;
            }
            else
            {
                this.Items.Add(item, 1);
                this.TotalItems += 1;
            }

            this.TotalWeight += item.GetWeight();
        }

        public void RemoveItem(Item item)
        {
            int defVal = 0;
            if (item is Money)
            {
                this.Money.Price -= item.Price;
            }
            else if (this.Items.TryGetValue(item, out defVal) && defVal.CompareTo(1) >= 0)
            {
                this.Items[item] -= 1;
                this.TotalItems -= 1;
            }

            if (this.Items.TryGetValue(item, out defVal) && defVal.CompareTo(0) <= 0)
            {
                this.Items.Remove(item);
            }

            this.TotalWeight -= item.GetWeight();
        }

        public override string ToString()
        {
            String toReturn = "[";
            foreach (KeyValuePair<Item, int> kvp in this.Items)
            {
                toReturn += "(" + kvp.Key + ", " + kvp.Value + "), ";
            }

            return toReturn + "]";

        }
    }

    abstract class Item
    {
        public int Weight { get; set; }
        public int Price { get; set; }
        public int Priority { get; set; }
        public String Name { get; set; }

        public virtual int GetValue()
        {
            return this.Priority * ((CategoryMaxPrice() - CategoryMinPrice()) / (CategoryMaxPrice() - this.Price + 1));
        }

        protected int GetPrice()
        {
            return this.Price;
        }

        public virtual int GetWeight()
        {
            return this.Weight;
        }

        public abstract int GetPriority();

        public abstract int CategoryMinPrice();

        public abstract int CategoryMaxPrice();

        public virtual String GetName()
        {
            return this.Name;
        }

        public override int GetHashCode()
        {
            return GetName().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.GetName().Equals(((Item)obj).GetName());
        }

        public override string ToString()
        {
            return this.GetName() + "[Price=" + this.GetPrice() + ", Priority=" + this.GetPriority() + ", Weight=" + this.GetWeight() + "]";
        }

    }

    class Money : QuestItem
    {

        public override String GetName()
        {
            return "Money";
        }

        public override int GetWeight()
        {
            return 0;
        }
    }

    class QuestItem : Item
    {
        public override int GetValue()
        {
            return this.GetPriority();
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
            return this.GetPriority();
        }
    }

    class NormalItem : Item
    {
        public override int GetValue()
        {
            return this.Price;
        }

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