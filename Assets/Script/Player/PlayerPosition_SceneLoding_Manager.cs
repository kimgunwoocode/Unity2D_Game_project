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
        // ��������Ʈ ü�� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //�÷��̾ ���� ��ȯ�� �� ���� �̸� ���س��� ��ġ�� �̵���Ű��
        player.transform.position = new Vector3(PlayerPosition_NextScene.x, PlayerPosition_NextScene.y, -1);
        PlayerPosition_NextScene = new Vector2(0, 0);//�ӷ� �ʱ�ȭ
    }

    void OnDisable()
    {
        // ��������Ʈ ü�� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
