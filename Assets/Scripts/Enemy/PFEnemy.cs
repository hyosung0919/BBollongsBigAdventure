using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PFEnemy : MonoBehaviour
{
    private bool isPlayerNearby = false;
    [SerializeField] private int stageNumber = 1;
    [SerializeField] private string enemyPrefabName;//  3D 전투에서 소환할 프리팹 이름
    public GameObject Wall;

    private void OnEnable()
    {
        //  전투 후 복귀 시, 이미 처치된 적만 제거
        if (BattleData.defeatedEnemies.Contains(enemyPrefabName))
        {
            if (Wall != null)
                Wall.SetActive(false);

            Debug.Log($"[Enemy2D] {enemyPrefabName} 이미 처치됨 → 제거 및 벽 해제");
            Destroy(gameObject);
        }
    }


    void Start()
    {
        // 죽은 적 제거
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
            Debug.Log($"{gameObject.name}과 전투 시작!");

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            BattlePlayerController playerHP = GetComponent<BattlePlayerController>();
            if (playerHP != null)
            {
                BattleData.playerHP = playerHP.currentHP;
                BattleData.playerMaxHP = playerHP.maxHP;
                BattleData.playerPosition = player.transform.position;
            }
            BattleData.enemyName = gameObject.name;
            BattleData.enemyPrefabName = enemyPrefabName; //  프리팹 이름 저장
            BattleData.stageIndex = stageNumber;
            BattleData.enemyDefeated = false;

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
