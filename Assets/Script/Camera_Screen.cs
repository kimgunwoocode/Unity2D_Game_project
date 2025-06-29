using UnityEngine;

public class Camera_Screen : MonoBehaviour
{
    public static float x_camera, y_camera;

    public void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (���� / ����)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;

        x_camera = Screen.width;
        y_camera = Screen.height;
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);
}
