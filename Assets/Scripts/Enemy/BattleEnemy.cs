using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleEnemy : MonoBehaviour
{
    [Header("체력 설정")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("전투 설정")]
    public float moveSpeed = 2f;        // 이동 속도
    public float attackRange = 1.5f;    // 공격 사거리
    public float attackDamage = 15f;    // 공격력
    public float attackCooldown = 1.5f; // 공격 쿨타임
    private bool canAttack = true;

    [Header("UI 설정")]
    public GameObject healthBarPrefab;
    private Slider healthSlider;
    private Transform healthBarTransform;
    private Camera mainCamera;

    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        //  체력바 생성
        if (healthBarPrefab != null)
        {
            GameObject hb = Instantiate(healthBarPrefab);
            Transform canvas = GameObject.Find("EnemyCanvas").transform;
            hb.transform.SetParent(canvas, worldPositionStays: false);
            hb.transform.localScale = Vector3.one * 0.5f;
            hb.transform.position = transform.position + Vector3.up * 2f;

            healthSlider = hb.GetComponentInChildren<Slider>();
            healthBarTransform = hb.transform;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        if (player == null) return;

        //  플레이어 추적
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            // 이동
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.LookAt(player.position);
        }
        else
        {
            // 공격
            if (canAttack)
                StartCoroutine(Attack());
        }

        //  체력바 따라가기
        if (healthBarTransform != null)
        {
            healthBarTransform.position = transform.position + Vector3.up * 2f;
            healthBarTransform.LookAt(mainCamera.transform);
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;

        //  플레이어에게 데미지 주기
        BattlePlayerController playerHealth = player.GetComponent<BattlePlayerController>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log($"플레이어에게 {attackDamage} 데미지!");
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (healthBarTransform != null)
            Destroy(healthBarTransform.gameObject);

        BattlePlayerController player = FindObjectOfType<BattlePlayerController>();
        if (player != null)
        {
            BattleData.playerHP = player.currentHP;
            BattleData.playerMaxHP = player.maxHP;
        }

        //  여러 적을 저장
        BattleData.defeatedEnemies.Add(BattleData.enemyPrefabName);

        BattleData.enemyDefeated = true;
        BattleData.lastDefeatedEnemy = BattleData.enemyPrefabName;

        Debug.Log($"적 '{BattleData.enemyPrefabName}' 처치 완료! {BattleData.previousSceneName}으로 복귀합니다.");

        Destroy(gameObject);
        BattleReturnManager.ReturnToStage();
    }

}
