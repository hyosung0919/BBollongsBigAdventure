using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleData
{
    public static string enemyName; // 싸운 적 이름
    public static bool enemyDefeated = false; // 전투 결과
    public static int stageIndex;        // 전투 전에 있던 스테이지 번호

    public static Vector3 playerPosition;
}
