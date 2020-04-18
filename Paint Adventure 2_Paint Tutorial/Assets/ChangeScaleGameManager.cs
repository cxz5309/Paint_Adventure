using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScaleGameManager : MonoBehaviour
{
    public GameObject mainSpriteObject;
    public GameObject dotPrefabs;

    private bool isDotDrag;

    private void Start()
    {
        mainSpriteObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/a");
        Sprite mainSp = mainSpriteObject.GetComponent<SpriteRenderer>().sprite;
        Vector2 spriteSize = mainSp.rect.size/100;

        mainSpriteObject.GetComponent<BoxCollider2D>().size = spriteSize;

        Vector2 v1 = ((new Vector2(mainSp.rect.xMin, mainSp.rect.yMin) - mainSp.rect.center) / 100);
        Vector2 v2 = ((new Vector2(mainSp.rect.xMax, mainSp.rect.yMin) - mainSp.rect.center) / 100);
        Vector2 v3 = ((new Vector2(mainSp.rect.xMin, mainSp.rect.yMax) - mainSp.rect.center) / 100);
        Vector2 v4 = ((new Vector2(mainSp.rect.xMax, mainSp.rect.yMax) - mainSp.rect.center) / 100);

        GameObject dot1 = Instantiate(dotPrefabs, mainSpriteObject.transform);
        dot1.transform.localPosition = v1;
        GameObject dot2 = Instantiate(dotPrefabs, mainSpriteObject.transform);
        dot2.transform.localPosition = v2;
        GameObject dot3 = Instantiate(dotPrefabs, mainSpriteObject.transform);
        dot3.transform.localPosition = v3;
        GameObject dot4 = Instantiate(dotPrefabs, mainSpriteObject.transform);
        dot4.transform.localPosition = v4;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isDotDrag)
            {
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Dot")
        {
            isDotDrag = true;
        }
    }
    
}
