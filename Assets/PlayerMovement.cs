using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public GameState gameState;
    public Rigidbody2D rigidBody;

    public float swimSpeed = 40f;
    float horizontal = 0f;
    float vertical = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * swimSpeed;
        vertical = Input.GetAxisRaw("Vertical") * swimSpeed;
    }

    private void FixedUpdate()
    {
        controller.Move2(horizontal * Time.fixedDeltaTime, vertical * Time.fixedDeltaTime);
        animator.SetBool("moving", horizontal != 0f || vertical != 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CollectibleItem"))
        {
            Destroy(collision.gameObject);
            gameState.addItemCount();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Baddie"))
        {
            Die();
        }
    }

    private void Die() {
        //rigidBody.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
