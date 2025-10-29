using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleData
{
    public static HashSet<string> defeatedEnemies = new HashSet<string>(); // 여러 적 저장

    public static string enemyName;
    public static string enemyPrefabName;
    public static bool enemyDefeated;
    public static int stageIndex;
    public static Vector3 playerPosition;
    public static string lastDefeatedEnemy;
    public static float playerHP = -1f;
    public static float playerMaxHP = 100f;
    public static string previousSceneName;
}

