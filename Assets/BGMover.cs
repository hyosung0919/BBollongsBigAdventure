using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMover : MonoBehaviour
{
    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput < 0)
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);

        if (moveInput > 0)
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
