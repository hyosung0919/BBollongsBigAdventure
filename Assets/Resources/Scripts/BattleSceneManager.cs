    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    [Header("전투 시작 시 적이 생성될 위치")]
    [SerializeField] private Transform enemySpawnPoint;

    void Start()
    {
        if (string.IsNullOrEmpty(BattleData.enemyPrefabName))
        {
            Debug.LogError("[BattleSceneManager] enemyPrefabName이 비어있습니다!");
            return;
        }

        // Resources 폴더 경로 기준으로 프리팹 로드
        string prefabPath = $"Prefabs/{BattleData.enemyPrefabName}";
        GameObject enemyPrefab = Resources.Load<GameObject>(prefabPath);

        if (enemyPrefab == null)
        {
            Debug.LogError($"[BattleSceneManager] '{prefabPath}' 경로에 해당하는 프리팹을 찾을 수 없습니다!");
            return;
        }

        Vector3 spawnPosition = enemySpawnPoint ? enemySpawnPoint.position : Vector3.zero;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Debug.Log($"[BattleSceneManager] '{BattleData.enemyPrefabName}' 프리팹 소환 완료 ");
    }
}
