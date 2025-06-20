using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeed_Manager : MonoBehaviour
{
    /*
    public Text speedtext;
    public Text speedtimetext;
    private void Update()
    {
        speedtext.text = PlayerData_Manager.moveSpeed.ToString();
        speedtimetext.text = cool.ToString("F2");
    }
    */

    float cool;

    public float present_EffectTime = 0;
    public float present_SpeedEffect = 0;
    Coroutine running = null;

    public void SpeedEffect_cool(float speed, float time)
    {
        if (speed >= present_SpeedEffect)
        {
            if (running != null)
            {
                StopCoroutine(running);
                PlayerData_Manager.moveSpeed += present_SpeedEffect;
                //print("코루틴 종료");
            }

            present_SpeedEffect = speed;
            present_EffectTime = time;

            running = StartCoroutine(Effect_cool(speed, time));
        }
        else
        {
            //print("버프중첩 불가");
        }
    }

    public IEnumerator Effect_cool(float speed, float time)
    {

        //print("speed : " + speed + "  time : " + time);

        PlayerData_Manager.moveSpeed -= speed;

        //yield return new WaitForSeconds(time);

        cool = time;
        while(true)
        {
            cool -= Time.deltaTime;
            if (cool <= 0)
            {
                break;
            }
            yield return null;
        }

        PlayerData_Manager.moveSpeed += speed;
        present_SpeedEffect = 0;
        //print("시간 끝 (" + time + "s)");

        yield break;
    }
}
