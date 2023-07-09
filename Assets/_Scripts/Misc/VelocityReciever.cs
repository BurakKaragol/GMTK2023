using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityReciever : MonoBehaviour
{
    public float multiplier = 0.2f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
        {
            rb.velocity += rigidbody.velocity * multiplier;
        }
    }
}
