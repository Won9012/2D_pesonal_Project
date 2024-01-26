using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemForec : MonoBehaviour
{
    public float initialForce = 10f; // 초기 위로 가해지는 힘
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * initialForce, ForceMode2D.Impulse);
    }
}
