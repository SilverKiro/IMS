using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    [SerializeField] private Category _category;
    [SerializeField] private int price;
    [SerializeField] private int weight;
    [SerializeField] private string name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
