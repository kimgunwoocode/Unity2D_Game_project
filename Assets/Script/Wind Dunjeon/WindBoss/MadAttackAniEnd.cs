using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadAttackAniEnd : MonoBehaviour
{
    void AnimationEnd()//애니메이션 이후 오브젝트 파괴
    {
        Destroy(gameObject);
    }
}
