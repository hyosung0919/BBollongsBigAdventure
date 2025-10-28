using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PFEnemy : MonoBehaviour
{
    private bool isPlayerNearby = false;
    [SerializeField] private int stageNumber = 1;

    void Start()
    {
        //  전투에서 죽은 적 확인
        if (BattleData.enemyDefeated)
        {
            Debug.Log($"[Enemy2D] {gameObject.name} 검사 중 - Stage {stageNumber}");
            Debug.Log($"현재 BattleData: enemyName={BattleData.enemyName}, stageIndex={BattleData.stageIndex}");

            if (BattleData.enemyName == gameObject.name && BattleData.stageIndex == stageNumber)
            {
                Debug.Log($"[Enemy2D] {gameObject.name}은(는) 이미 죽은 적 → 삭제됨");
                Destroy(gameObject);
                return;
            }
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log($"{gameObject.name}과 전투 시작!");

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                BattleData.playerPosition = player.transform.position;

            BattleData.enemyName = gameObject.name;
            BattleData.stageIndex = stageNumber;
            BattleData.enemyDefeated = false; //  새로운 전투 시작 시 false로 초기화
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
