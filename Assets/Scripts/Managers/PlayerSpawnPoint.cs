using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        // 전투에서 돌아온 경우만 위치 복원
        if (BattleData.enemyDefeated)
        {
            player.transform.position = BattleData.playerPosition;
            Debug.Log($"플레이어 전투 복귀 위치: {BattleData.playerPosition}");

            // 다음 복귀 때 혼동 방지
            BattleData.enemyDefeated = false;
        }
    }
}
