using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintCanvas2UGUIController : MonoBehaviour {

    [Header("Setting")]
    public ToggleGroup leftBar;
    public ToggleGroup rightBar;
    public Slider penScaleBar;
    public Toggle isEarseBar;
    public Toggle isColorfulBar;

    [Header("Paint Canvas")]
    public Painter painterCanvas;

    private bool _isMouseDown =false;

    // Update is called once per frame
    void FixedUpdate () {
		bool isRawImg = false;
		foreach(Toggle t in leftBar.ActiveToggles()){

			//Set pen texture.
			painterCanvas.penMat.mainTexture = t.GetComponent<Image>().sprite.texture;


			PenSetting ps = t.GetComponent<PenSetting>();

			//save raw img flag.
			isRawImg = ps.isRawImg;
			break;
		}

		if(!isColorfulBar.isOn){
			//Set the pen color if is not colorfull stuatus.
			if(isRawImg || isColorfulBar.isOn){
				painterCanvas.penColor = Color.white;
			}else{
				foreach(Toggle t in rightBar.ActiveToggles()){
					painterCanvas.penColor = t.GetComponent<Image>().color;
					break;
				}
			}
		}

		//pen size.
		painterCanvas.brushScale = penScaleBar.value;

		//is erase.
		painterCanvas.isErase = isEarseBar.isOn;

		//draw line or draw colorful line.
		painterCanvas.paintType = isColorfulBar.isOn? Painter.PaintType.DrawColorfulLine: Painter.PaintType.DrawLine;
	}

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			_isMouseDown = true;
            //Draw once when mouse down.

            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(painterCanvas.transform as RectTransform, Input.mousePosition, null, out pos))
            {
                if (painterCanvas.GetComponent<RectTransform>().rect.Contains(pos))
                {
                    painterCanvas.CopyTexture(true);
                     
                    painterCanvas.ClickDraw(pos, null, painterCanvas.penMat.mainTexture, painterCanvas.brushScale,
                       painterCanvas.penMat, painterCanvas.renderTexture, true);
                }
            }
        }
		else if(Input.GetMouseButton(0)){
			if(_isMouseDown)
			{
				//draw on mouse drag.
                Vector2 pos;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(painterCanvas.transform as RectTransform, Input.mousePosition, null, out pos))
                {
                    if (painterCanvas.GetComponent<RectTransform>().rect.Contains(pos))
                    {
                        painterCanvas.Drawing(pos, null, painterCanvas.renderTexture, false, true);
                    }
                }
			}
		}
		else if(Input.GetMouseButtonUp(0) && _isMouseDown)
		{
            painterCanvas.EndDraw();
			_isMouseDown = false;
		}
	}

	public void OnClickClearCanvas(){
		//clear the paint canvas.
		painterCanvas.ClearCanvas();
	}

    public void OnClickSaveTexture()
    {
        painterCanvas.SaveTexture();
    }

    public void OnClickUndoTexture()
    {
        painterCanvas.UndoTexture();
    }

    public void OnClickRedoTexture()
    {
        painterCanvas.RedoTexture();
    }
}
