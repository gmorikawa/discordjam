using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Atributos")]
    public float speed;
    public int step;

    [Header("Flags")]
    public bool isWalking = false;

    private int nextStep;

    private Vector3 direction;
    private Vector3 endPoint;

    private Animator animator;
    private new Renderer renderer;
    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;

    private Coroutine walkingRoutine;

    void Start()
    {
        direction = new Vector3();

        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        endPoint = new Vector3();
    }

    void Update()
    {
        if (!isWalking)
        {
            if (Input.GetKey(KeyCode.A))
            {
                direction = Vector3.left;
                isWalking = true;
                Debug.Log("left");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = Vector3.right;
                isWalking = true;
                Debug.Log("right");
            }
            else if (Input.GetKey(KeyCode.W))
            {
                direction = Vector3.up;
                isWalking = true;
                Debug.Log("up");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction = Vector3.down;
                isWalking = true;
                Debug.Log("down");
            }
            else
            {
                direction = Vector3.zero;
            }

            endPoint = transform.position + direction;
            if(isWalking)
                walkingRoutine = StartCoroutine(Movement(transform.position, endPoint));
        }
    }

    IEnumerator Movement(Vector3 startPosition, Vector3 endPosition)
    {
        float i = 0f;
        while(i < 1f) {
            i += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, endPosition, i);
            Debug.Log(i);
            yield return null;
        }
        isWalking = false;
    }
}
