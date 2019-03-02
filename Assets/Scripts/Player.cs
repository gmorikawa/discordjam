using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Atributos")]
    public float speed;

    private Vector3 direction;
    private Animator animator;
    private new Renderer renderer;
    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;

    void Start()
    {
        direction = new Vector3();

        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        /* direção direita ou esquerda */
        direction.x = (Input.GetKey(KeyCode.A) ? -1f : 0f) + (Input.GetKey(KeyCode.D) ? 1f : 0f);
        direction.y = (Input.GetKey(KeyCode.S) ? -1f : 0f) + (Input.GetKey(KeyCode.W) ? 1f : 0f);

        transform.position += direction * speed * Time.deltaTime;
    }
}
