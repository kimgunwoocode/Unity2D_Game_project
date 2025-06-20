using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition_SceneLoding_Manager : MonoBehaviour
{
    public static Vector2 PlayerPosition_NextScene = new Vector2(0, 0);

    public GameObject player;

    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //플레이어가 씬을 전환할 때 마다 미리 정해놓은 위치로 이동시키기
        player.transform.position = new Vector3(PlayerPosition_NextScene.x, PlayerPosition_NextScene.y, -1);
        PlayerPosition_NextScene = new Vector2(0, 0);//속력 초기화
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
