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

    Dictionary<string, bool> validDirections;

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

        InitValidDirectionsDictionary();
    }

    void Update()
    {
        RaycastAtDirections();

        direction.x = (validDirections["left"] && Input.GetKeyDown(KeyCode.A) ? -1f : 0f)
                    + (validDirections["right"] && Input.GetKeyDown(KeyCode.D) ? 1f : 0f);
        direction.y = (validDirections["down"] && Input.GetKeyDown(KeyCode.S) ? -1f : 0f)
                    + (validDirections["up"] && Input.GetKeyDown(KeyCode.W) ? 1f : 0f);

        transform.position += direction;// * speed * Time.deltaTime;
        //if (!isWalking)
        //{
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        direction = Vector3.left;
        //        isWalking = true;
        //        Debug.Log("left");
        //    }
        //    else if (Input.GetKey(KeyCode.D))
        //    {
        //        direction = Vector3.right;
        //        isWalking = true;
        //        Debug.Log("right");
        //    }
        //    else if (Input.GetKey(KeyCode.W))
        //    {
        //        direction = Vector3.up;
        //        isWalking = true;
        //        Debug.Log("up");
        //    }
        //    else if (Input.GetKey(KeyCode.S))
        //    {
        //        direction = Vector3.down;
        //        isWalking = true;
        //        Debug.Log("down");
        //    }
        //    else
        //    {
        //        direction = Vector3.zero;
        //    }

        //    endPoint = transform.position + direction;
        //    if(isWalking)
        //        walkingRoutine = StartCoroutine(Movement(transform.position, endPoint));
        //}
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

    void InitValidDirectionsDictionary()
    {
        validDirections = new Dictionary<string, bool>();
        validDirections.Add("right", true);
        validDirections.Add("left", true);
        validDirections.Add("up", true);
        validDirections.Add("down", true);
    }

    void RaycastAtDirections()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.right, 1f, LayerMask.GetMask("Parede"));
        validDirections["right"] = hit.collider == null;
        if (hit.collider != null) Debug.Log("Parede: right");

        hit = Physics2D.Raycast(transform.position, Vector2.left, 1f, LayerMask.GetMask("Parede"));
        validDirections["left"] = hit.collider == null;
        if (hit.collider != null) Debug.Log("Parede: left");

        hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, LayerMask.GetMask("Parede"));
        validDirections["up"] = hit.collider == null;
        if (hit.collider != null) Debug.Log("Parede: up");

        hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Parede"));
        validDirections["down"] = hit.collider == null;
        if (hit.collider != null) Debug.Log("Parede: down");
    }
}
