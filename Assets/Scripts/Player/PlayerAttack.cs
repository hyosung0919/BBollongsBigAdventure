using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Punch Settings")]
    public float punchDistance = 0.5f;
    public float punchSpeed = 10f;
    public float returnSpeed = 7f;
    public int damage = 10;

    [Header("Hitbox Settings")]
    public Vector3 hitboxHalfExtents = new Vector3(0.2f, 0.2f, 0.2f); // 크기
    public Vector3 hitboxOffset = new Vector3(0f, 0f, 0.3f);          // 위치 오프셋

    private Vector3 startLocalPos;
    private bool isPunching = false;
    private bool hasHitEnemy = false; // 펀치 1회당 1회만 데미지

    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPunching)
        {
            StartCoroutine(PunchRoutine());
        }
    }

    IEnumerator PunchRoutine()
    {
        isPunching = true;
        hasHitEnemy = false; // 새 펀치 시작 시 초기화

        Vector3 targetPos = startLocalPos + Vector3.forward * punchDistance;

        // 앞으로 나가기
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * punchSpeed;
            transform.localPosition = Vector3.Lerp(startLocalPos, targetPos, t);
            TryDealDamage();
            yield return null;
        }

        // 돌아오기
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * returnSpeed;
            transform.localPosition = Vector3.Lerp(targetPos, startLocalPos, t);
            yield return null;
        }

        isPunching = false;
    }

    void TryDealDamage()
    {
        if (hasHitEnemy) return;

        // 주먹의 로컬 기준 오프셋 위치 계산
        Vector3 hitboxCenter = transform.TransformPoint(hitboxOffset);

        // OverlapBox로 히트박스 감지
        Collider[] hits = Physics.OverlapBox(
            hitboxCenter,
            hitboxHalfExtents,
            transform.rotation
        );

        foreach (Collider col in hits)
        {
            if (col.CompareTag("Enemy"))
            {
                BattleEnemy enemy = col.GetComponent<BattleEnemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    hasHitEnemy = true;
                    break;
                }
            }
        }
    }

    // 에디터에서 히트박스 시각화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (Application.isPlaying)
            Gizmos.matrix = transform.localToWorldMatrix;
        else
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        Vector3 center = hitboxOffset;
        Gizmos.DrawWireCube(center, hitboxHalfExtents * 2);
    }
}
