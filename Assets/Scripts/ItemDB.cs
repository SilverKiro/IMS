using System;
using Items;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    [SerializeField] private Category _category;
    [SerializeField] private int _price;
    [SerializeField] private int _weight;
    [SerializeField] private string _name;

    public Inventory Inventory { get; set; }
    public Item Item { get; set; }
    public int Loc { get; set; }


    private void Awake()
    {
        switch ( _category )
        {
            case Category.MONEY:
                Item = new Money { Name = _name };
                break;
            case Category.COMMON:
                Item = new CommonItem { Name = _name, Price = _price, Weight = _weight };
                break;
            case Category.EXOTIC:
                Item = new ExoticItem { Name = _name, Price = _price, Weight = _weight };
                break;
            case Category.LEGENDARY:
                Item = new LegendaryItem { Name = _name, Price = _price, Weight = _weight };
                break;
            case Category.QUEST_ITEM:
                Item = new QuestItem { Name = _name, Price = _price, Weight = _weight };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Item.Parent = gameObject;
    }


    private enum Category
    {
        MONEY,
        QUEST_ITEM,
        COMMON,
        EXOTIC,
        LEGENDARY
    }
}
