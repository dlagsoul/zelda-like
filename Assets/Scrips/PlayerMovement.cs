using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState {
    walk,
    attack,
    interact
}
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    private Rigidbody2D myRigidbody;
    private Vector2 change;
    private Animator animator;

    public PlayerState currentState;

    void Start()
    {
        currentState = PlayerState.walk;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector2.zero;

        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        
    }

    void FixedUpdate()
    {
        if (currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(.2f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector2.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(
            myRigidbody.position + change.normalized * moveSpeed * Time.fixedDeltaTime
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUpPicker"))
        {
            other.gameObject.SetActive(false);
            float movementSpeed = other.GetComponent<SpeedForce>().speedForce;
            float duration = other.GetComponent<SpeedForce>().duration;

            StartCoroutine(BoostSpeed(movementSpeed, duration));
        }
    }

    private IEnumerator BoostSpeed(float movementSpeed, float duration)
    {
        moveSpeed += movementSpeed;
        yield return new WaitForSeconds(duration);
        moveSpeed -= movementSpeed;
    }
}
