using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemLabel : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public GameObject ast;

    public GameObject label;

    private float count;
    
    // Start is called before the first frame update
    void Start()
    {

        label = ItemLabelPreview.singleton;
    }

    void Update()
    {
        if ( count > 0 )
        {
            count -= Time.deltaTime;
            if ( count <= 0 )
            {
                label.SetActive(false);
            }
        }
        
    }
}
