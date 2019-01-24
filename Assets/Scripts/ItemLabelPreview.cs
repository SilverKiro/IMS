using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLabelPreview : MonoBehaviour
{
    public Text Name;
    public Text Weight;
    public Text Price;
    public Text Tier;

    public static GameObject singleton;
    public GameObject ast;

    void Awake()
    {
        Debug.Log("wut");
        singleton = ast;
        singleton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
