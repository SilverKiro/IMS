using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemLabel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ast;

    public GameObject label;

    // Start is called before the first frame update
    void Start()
    {

        label = ItemLabelPreview.singleton;
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemDB other = ast.GetComponent<ItemDB>();
        Debug.Log(other.Price);

        ItemLabelPreview ilp = label.GetComponent<ItemLabelPreview>();
        ilp.Price.text = "Price: " + other.Price.ToString();
        ilp.Name.text = "Name: " + other.Name.ToString();
        ilp.Weight.text = "Weight: " + other.Weight.ToString();
        ilp.Tier.text = "Tier: " + other.Category1.ToString();

        /*Debug.Log(label.transform.position);
        Debug.Log(ast.transform.position);*/

        label.transform.position = ast.transform.position;

        /*Debug.Log(label.transform.position);
        Debug.Log(ast.transform.position);*/

        label.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        label.SetActive(false);
    }
       
}
