using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpGM : MonoBehaviour
{
    Vector2 prevMousePosition;
    private GameObject mainObject;
    public float sizingFactor = 0.03f;
    Vector3 minimumScale;

    public Vector2 pointerStart;
    public Vector2 sizeStart;
    public Vector2 scaleStart;
    public Vector2 positionStart;
    public Vector2 pointerTravel;
    public float scaleX;
    public float positionX;

    private void Start()
    {
        // Parsing the object we want to modify to mainObject
        mainObject = GameObject.Find("Cube");
        // Setting the minimum scale of the mainObject
        minimumScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void Update()
    {
        OnMouseDrag();
    }

    void OnMouseDrag()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            // Set this object's position to where the mouse is being held down by the left click.
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

            // Change the scale of mainObject by comparing previous frame mousePosition with this frame's position, modified by sizingFactor.
            Vector3 scale = mainObject.transform.localScale;
            scale.x = scale.x + (mousePosition.x - prevMousePosition.x) * sizingFactor;
            scale.y = scale.x;
            scale.z = scale.x;
            mainObject.transform.localScale = scale;

            // Checking if current scale is less than minimumScale, if yes, mainObject scales takes value from minimumScale
            if (scale.x < minimumScale.x)
            {
                mainObject.transform.localScale = minimumScale;
            }
        }

        prevMousePosition = mousePosition;
    }

    //private void OnMouseDown()
    //{
    //    pointerStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    sizeStart = myTool.myEditingObject.GetComponent<SpriteRenderer>().bounds.size;
    //    scaleStart = myTool.myEditingObject.transform.localScale;
    //    positionStart = myTool.myEditingObject.transform.localPosition;
    //}

    //private void OnMouseDrag()
    //{
    //    if (scaleSide == ScaleSide.RIGHT)
    //    {
    //        pointerTravel = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - pointerStart;
    //        scaleX = ((sizeStart.x + pointerTravel.x) / sizeStart.x) * scaleStart.x;
    //        positionX = positionStart.x + (pointerTravel.x / 2);

    //        var scale = new Vector2(scaleX, myTool.myEditingObject.localScale.y);
    //        var pos = new Vector2(positionX, myTool.myEditingObject.localPosition.y);

    //        myTool.Scale(scale, pos); //Just applies the above math to the object the math is meant for.
    //    }
    //}
}
