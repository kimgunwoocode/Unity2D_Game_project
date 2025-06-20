using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    public RectTransform[] cloud = new RectTransform[3];
    public RectTransform[] ground_1 = new RectTransform[3];
    public RectTransform[] ground_2 = new RectTransform[3];

    private float cloud_speed = -100f;
    private float ground1_speed = 15f;
    private float ground2_speed = 50f;

    private int cloud_pos = 0;
    private int ground1_pos = 0;
    private int ground2_pos = 0;

    void Start()
    {
        
    }

    void Update()
    {
        cloud[0].anchoredPosition -= new Vector2(cloud_speed, 0) * Time.deltaTime;
        cloud[1].anchoredPosition -= new Vector2(cloud_speed, 0) * Time.deltaTime;
        cloud[2].anchoredPosition -= new Vector2(cloud_speed, 0) * Time.deltaTime;
        if (cloud[cloud_pos].anchoredPosition.x >= 2880)
        {
            cloud[cloud_pos].anchoredPosition = new Vector2(-2880, 0);
            if (cloud_pos == 0)
                cloud_pos = 1;
            else if (cloud_pos == 1)
                cloud_pos = 2;
            else if (cloud_pos == 2)
                cloud_pos = 0;
        }

        ground_1[0].anchoredPosition -= new Vector2(ground1_speed, 0) * Time.deltaTime;
        ground_1[1].anchoredPosition -= new Vector2(ground1_speed, 0) * Time.deltaTime;
        ground_1[2].anchoredPosition -= new Vector2(ground1_speed, 0) * Time.deltaTime;
        if (ground_1[ground1_pos].anchoredPosition.x <= -2880)
        {
            ground_1[ground1_pos].anchoredPosition = new Vector2(2880, 0);
            if (ground1_pos == 0)
                ground1_pos = 1;
            else if (ground1_pos == 1)
                ground1_pos = 2;
            else if (ground1_pos == 2)
                ground1_pos = 0;
        }

        ground_2[0].anchoredPosition -= new Vector2(ground2_speed, 0) * Time.deltaTime;
        ground_2[1].anchoredPosition -= new Vector2(ground2_speed, 0) * Time.deltaTime;
        ground_2[2].anchoredPosition -= new Vector2(ground2_speed, 0) * Time.deltaTime;
        if (ground_2[ground2_pos].anchoredPosition.x <= -2880)
        {
            ground_2[ground2_pos].anchoredPosition = new Vector2(2880, 0);
            if (ground2_pos == 0)
                ground2_pos = 1;
            else if (ground2_pos == 1)
                ground2_pos = 2;
            else if (ground2_pos == 2)
                ground2_pos = 0;
        }
    }
}
