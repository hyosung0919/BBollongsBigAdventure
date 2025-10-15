using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PFPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;                   //캐릭터 이동속도를 조절   
    public float jumpForce = 5f;                   //캐릭터 점프힘 조절
    public Transform groundcheck;                  //캐릭터가 땅에 닿았는지 확인
    public LayerMask groundlayer;                  //땅의 레이러를 나타냄    
    public float fallSpeed = 0f;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            pAni.SetBool("RunAction", true);
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            pAni.SetBool("RunAction", true);
        }
        else
        {
            pAni.SetBool("RunAction", false);
        }

        isGrounded = Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundlayer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                pAni.SetTrigger("JumpAction");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead"))
        {
            SceneManager.LoadScene("DeadScene");

        }
        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<Scene>().MovetoNextLevel();
        }
    }
}
