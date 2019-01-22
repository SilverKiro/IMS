using System;
using Items;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    [SerializeField] private Category _category;
    [SerializeField] private int _price;
    [SerializeField] private int _weight;
    [SerializeField] private string _name;

    public Item Item;

    public int Price { get => _price; set => _price = value; }
    public Category Category1 { get => _category; set => _category = value; }
    public int Weight { get => _weight; set => _weight = value; }
    public string Name { get => _name; set => _name = value; }

    private void Awake()
    {
        switch ( Category1 )
        {
            case Category.MONEY:
                Item = new Money { Name = Name };
                break;
            case Category.COMMON:
                Item = new CommonItem { Name = Name, Price = Price, Weight = Weight };
                break;
            case Category.EXOTIC:
                Item = new ExoticItem { Name = Name, Price = Price, Weight = Weight };
                break;
            case Category.LEGENDARY:
                Item = new LegendaryItem { Name = Name, Price = Price, Weight = Weight };
                break;
            case Category.QUEST_ITEM:
                Item = new QuestItem { Name = Name, Price = Price, Weight = Weight };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    public enum Category
    {
        MONEY,
        QUEST_ITEM,
        COMMON,
        EXOTIC,
        LEGENDARY
    }
}
