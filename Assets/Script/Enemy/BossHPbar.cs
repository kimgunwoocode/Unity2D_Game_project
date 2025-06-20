using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPbar : MonoBehaviour
{
    public Slider HP_slider;
    public EnemyData enemydata;
    private void Update()
    {
        HP_slider.value = (enemydata.Enemy_PresentHP / (float)enemydata.Enemy_MaxHP);
    }
}
