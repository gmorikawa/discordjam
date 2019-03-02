using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Atributos")]
    public float speed = 7.5f;
    public float step = 1f;
    public int lookingDistance = 4;

    [Header("Flags")]
    public bool isWalking = false;
    public bool isHunting = false;

    private Dictionary<string, bool> validDirections;
    private Vector3 direction;
    private Vector3 facing;
    private Vector3 endPoint;

    private Transform target;

    /* Components */
    private Animator animator;
    private new Renderer renderer;
    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;

    private Coroutine walkingRoutine;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Inimigo");

        direction = new Vector3();
        facing = Vector3.down;

        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        endPoint = new Vector3();

        InitValidDirectionsDictionary();
    }
    
    void Update()
    {
        RaycastAtDirections();
        RaycastVision();

        if (!isWalking)
        {
            VerifyWalkingInput();

            endPoint = transform.position + direction;
            if (isWalking)
                walkingRoutine = StartCoroutine(Movement(transform.position, endPoint));
        }
        else
        {
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            other.GetComponent<Player>().SetGameOver();
    }

    IEnumerator<float> Movement(Vector3 startPosition, Vector3 endPosition)
    {
        float i = 0f;
        while (i < 1f)
        {
            i += Time.deltaTime * speed * (isHunting ? 2f : 1f);
            transform.position = Vector3.Lerp(startPosition, endPosition, i);
            yield return i;
        }
        isWalking = false;
    }

    /// <summary>
    /// Popula o dictionary de direções válidas
    /// </summary>
    void InitValidDirectionsDictionary()
    {
        validDirections = new Dictionary<string, bool>();
        validDirections.Add("right", true);
        validDirections.Add("left", true);
        validDirections.Add("up", true);
        validDirections.Add("down", true);
    }

    /// <summary>
    /// Dispara raycasts para as quatro direções
    /// </summary>
    void RaycastAtDirections()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.right, step, LayerMask.GetMask("Parede"));
        validDirections["right"] = hit.collider == null;
        //if (hit.collider != null) Debug.Log("Parede: right");

        hit = Physics2D.Raycast(transform.position, Vector2.left, step, LayerMask.GetMask("Parede"));
        validDirections["left"] = hit.collider == null;
        //if (hit.collider != null) Debug.Log("Parede: left");

        hit = Physics2D.Raycast(transform.position, Vector2.up, step, LayerMask.GetMask("Parede"));
        validDirections["up"] = hit.collider == null;
        //if (hit.collider != null) Debug.Log("Parede: up");

        hit = Physics2D.Raycast(transform.position, Vector2.down, step, LayerMask.GetMask("Parede"));
        validDirections["down"] = hit.collider == null;
        //if (hit.collider != null) Debug.Log("Parede: down");
    }

    /// <summary>
    /// Dispara raycast na direção de visão para ver se enxerga o player
    /// </summary>
    void RaycastVision()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, facing, step * lookingDistance, LayerMask.GetMask("Player", "Parede"));
        Debug.DrawRay(transform.position, facing * lookingDistance, Color.red);
        target = hit.transform;
        isHunting = target != null && target.tag == "Player";
    }

    /// <summary>
    /// Verifica a entrada das teclas
    /// </summary>
    void VerifyWalkingInput()
    {
        if (!isHunting)
        {
            int value = Random.Range(0, 4);
            switch (value)
            {
                case 0:
                    if (validDirections["left"])
                    {
                        direction = Vector3.left * step;
                        isWalking = true;
                        //Debug.Log("left");
                    }
                    else
                    {
                        direction = Vector3.zero;
                    }
                    break;
                case 1:
                    if (validDirections["right"])
                    {
                        direction = Vector3.right * step;
                        isWalking = true;
                        //Debug.Log("right");
                    }
                    else
                    {
                        direction = Vector3.zero;
                    }
                    break;
                case 2:
                    if (validDirections["up"])
                    {
                        direction = Vector3.up * step;
                        isWalking = true;
                        //Debug.Log("up");
                    }
                    else
                    {
                        direction = Vector3.zero;
                    }
                    break;
                case 3:
                    if (validDirections["down"])
                    {
                        direction = Vector3.down * step;
                        isWalking = true;
                        //Debug.Log("down");
                    }
                    else
                    {
                        direction = Vector3.zero;
                    }
                    break;
            }
        } else
        {
            isWalking = true;
            // Debug.Log(direction);
        }

        animator.SetBool("isWalking", isWalking);
        if (direction != Vector3.zero)
        {
            facing = direction;
            //animator.SetFloat("dirX", direction.x);
            //animator.SetFloat("dirY", direction.y);
        }
    }
}
