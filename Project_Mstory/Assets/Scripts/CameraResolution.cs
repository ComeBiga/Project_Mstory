using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private float widthRatio = 9f;
    [SerializeField]
    private float heightRatio = 19.5f;

    // Start is called before the first frame update
    private void Start()
    {
        Rect rect = camera.rect;
        float scaleHeight = ((float)Screen.width / Screen.height) / (widthRatio / heightRatio);
        float scaleWidth = 1f / scaleHeight;

        if (scaleHeight < 1f)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }

        camera.rect = rect;
    }

    private void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }
}
