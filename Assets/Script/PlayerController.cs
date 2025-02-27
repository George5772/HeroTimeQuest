using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed; //user speed
    private bool isMoving; //check if user are moving?
    private Vector2 input; //hold x value and y value
    private Animator animator;
    public LayerMask fenchLayer;
    // as soon as load player, proggram will run this
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Debug.Log("Animator found: " + (animator != null));
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // when player is not moving
        if (!isMoving)
        {
            //get user input
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            // update position
            if(input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));

                }
           
            }
        }
        animator.SetBool("isMoving", isMoving);
    }
    // coroutines are used to perform actions over a period of time.
    // They are particularly useful for animations, waiting for a certain condition, or handling timed events.
    // Coroutines are methods that can pause their execution and return control to Unity,
    // and then continue from where they left off in the following frame.
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        //any movement bigger than zero = user move
        // sqrMagnitude return square of the magnitude (length of vector)
        //Mathf.Epsilon is a very small positive number (approximately 1.401298E-45).
        //It is used here to determine if the player has reached the target position.
        //If the squared distance between the current position and the target position is greater than Mathf.Epsilon,
        //it means the player has not yet reached the target position.
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            // Time.deltaTime: This represents the time that has passed since the last frame. Multiplying moveSpeed by Time.deltaTime ensures that the movement is frame rate independent,
            // meaning the player will move at a consistent speed regardless of the frame rate.
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
            // yield return null;: Pauses the coroutine execution until the next frame,
            // allowing the Move method to update the player's position incrementally over time.
        }
        transform.position = targetPos;
        isMoving = false;
    }
    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, fenchLayer) != null)
        {
            return false;
        }
        return true;
    }
}
