using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleEnemy : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject healthBarPrefab;
    private Slider healthSlider;
    private Transform healthBarTransform;
    private Camera mainCamera;

    void Start()
    {
        currentHealth = maxHealth;
        mainCamera = Camera.main;

        if (healthBarPrefab != null)
        {
            GameObject hb = Instantiate(healthBarPrefab);
            Transform canvas = GameObject.Find("Canvas").transform;
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
        if (healthBarTransform != null)
        {
            healthBarTransform.position = transform.position + Vector3.up * 2f;
            healthBarTransform.LookAt(mainCamera.transform);
        }
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

        BattleData.enemyDefeated = true;

        Debug.Log($"적 처치 완료! Stage_{BattleData.stageIndex}로 복귀합니다.");
        Destroy(gameObject);
        string stageName = $"Stage_{BattleData.stageIndex}";
        SceneManager.LoadScene(stageName);
    }

}
