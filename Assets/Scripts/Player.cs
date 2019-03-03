using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Atributos")]
    public float speed = 7.5f;
    public float step = 1f;

    [Header("Flags")]
    public bool isWalking = false;
    public bool isDead = false;

    private Dictionary<string, bool> validDirections;
    private Vector3 direction;
    private Vector3 facing;
    private Vector3 endPoint;

    /* Components */
    private Animator animator;
    private new Renderer renderer;
    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;

    public Item[] items = new Item[2];

    private Coroutine walkingRoutine;

    void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
        gameObject.tag = "Player";
    }

    void Start()
    {
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
        if(!isDead)
        {
            RaycastAtDirections();

            if(Input.GetKeyDown(KeyCode.Space))
            {
                InteractWith();
            }

            if (!isWalking)
            {
                VerifyWalkingInput();

                endPoint = transform.position + direction;
                if (isWalking)
                    walkingRoutine = StartCoroutine(Movement(transform.position, endPoint));
            }
        }
    }

    public void SetGameOver()
    {
        isDead = true;
        StartCoroutine(Dying());
    }

    IEnumerator Dying()
    {
        yield return null;
        ///
        /// Lógica para player morrendo
        ///
        Destroy(gameObject);
    }

    IEnumerator<float> Movement(Vector3 startPosition, Vector3 endPosition)
    {
        float i = 0f;
        while(i < 1f) {
            i += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, endPosition, i);
            yield return i;
        }
        isWalking = false;
    }

    void InteractWith() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, facing, step, LayerMask.GetMask("Parede"));
        if (hit && hit.transform.tag == "Interativo")
            hit.transform.GetComponent<Interativo>().Interagir();
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

        hit = Physics2D.Raycast(transform.position, Vector2.left, step, LayerMask.GetMask("Parede"));
        validDirections["left"] = hit.collider == null;

        hit = Physics2D.Raycast(transform.position, Vector2.up, step, LayerMask.GetMask("Parede"));
        validDirections["up"] = hit.collider == null;

        hit = Physics2D.Raycast(transform.position, Vector2.down, step, LayerMask.GetMask("Parede"));
        validDirections["down"] = hit.collider == null;
    }

    /// <summary>
    /// Verifica a entrada das teclas
    /// </summary>
    void VerifyWalkingInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            facing = Vector3.left;
            if (validDirections["left"])
            {
                direction = Vector3.left * step;
                isWalking = true;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            facing = Vector3.right;
            if (validDirections["right"])
            {
                direction = Vector3.right * step;
                isWalking = true;
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            facing = Vector3.up;
            if (validDirections["up"])
            {
                direction = Vector3.up * step;
                isWalking = true;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            facing = Vector3.down;
            if (validDirections["down"])
            {
                direction = Vector3.down * step;
                isWalking = true;
            }
        }
        else
        {
            direction = Vector3.zero;
        }

        animator.SetBool("isWalking", isWalking);
        if(direction != Vector3.zero)
        {
            animator.SetFloat("dirX", direction.x);
            animator.SetFloat("dirY", direction.y);
        }
    }
}
