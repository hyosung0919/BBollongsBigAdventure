using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        // 이전 전투에서 돌아온 경우만 적용
        if (BattleData.enemyDefeated)
        {
            player.transform.position = BattleData.playerPosition;
            Debug.Log($"플레이어를 전투 시작 위치로 이동: {BattleData.playerPosition}");
        }
    }
}
