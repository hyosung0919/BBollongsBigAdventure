using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattlePlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;
    private Camera cam;
    [Header("플레이어 체력 설정")]
    public float maxHP = 100f;
    public float currentHP;

    [Header("UI 연결")]
    public Slider healthSlider;

    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // 커서 숨기기

        if (BattleData.playerHP > 0)
        {
            maxHP = BattleData.playerMaxHP;
            currentHP = BattleData.playerHP;
        }
        else
        {
            currentHP = maxHP;
        }

        // 슬라이더 초기화
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = currentHP;
        }
    }

    void Update()
    {
        Move();
        Look();

        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = transform.right * x + transform.forward * z;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
    }

    void Attack()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackRange))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                // 적에게 데미지 전달
                hit.collider.GetComponent<BattleEnemy>()?.TakeDamage(10);
            }
        }
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;
        if (healthSlider != null)
            healthSlider.value = currentHP;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
    }

    public void Heal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        if (healthSlider != null)
            healthSlider.value = currentHP;
    }
}
