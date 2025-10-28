using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PFEnemy : MonoBehaviour
{
    private bool isPlayerNearby = false;
    [SerializeField] private int stageNumber = 1; //  이 적이 속한 스테이지 번호 (1~5)

    void Start()
    {
        // 전투 후 돌아올 때 적이 죽은 상태면 제거
        if (BattleData.enemyDefeated && BattleData.enemyName == gameObject.name && BattleData.stageIndex == stageNumber)
        {
            Destroy(gameObject);
            BattleData.enemyDefeated = false;
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log($"{gameObject.name}과(와) 전투 시작!");

            BattleData.enemyName = gameObject.name;
            BattleData.stageIndex = stageNumber; //  현재 스테이지 번호 저장
            SceneManager.LoadScene("BattleScene");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = false;
    }
}
