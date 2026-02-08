using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public HUD hud;
    
    Vector3 originalScale;
    Animator animator;
    int health = 100; // initialize health with 100 points
    public int MaxHealth = 100;

    public Collider2D NavArea;
    bool bJumpPressed = false;
    private Rigidbody2D rb;
    public int JumpForce = 1;

    [SerializeField]
    private Transform Feet;
    
    [SerializeField]
    private float distanceFromFeet = 0.1f;
    
    [SerializeField]
    private LayerMask groundMask;
    
    void Awake()
    {
        originalScale = transform.localScale;
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
        hud.UpdateHealthText(health);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            Feet.position,
            Vector2.down,
            distanceFromFeet, 
            groundMask);
        

        Debug.DrawRay(Feet.position, Vector2.down * distanceFromFeet,  Color.red);
        // if we hit something
        bool bHit = hit.collider != null;
      //  if (bHit)
       //     Debug.Log(hit.collider.name);
        
        return bHit;
    }

    [SerializeField]
    private AudioClip clip;
    // Update is called once per frame
    void Update()
    { 
        if (Time.timeScale == 0) return;
        
       // IsGrounded();
        if (!IsGrounded())
            Debug.Log("in air");
        
       float verticalValue = Input.GetAxisRaw("Vertical");
       float horizontalValue = Input.GetAxisRaw("Horizontal");

       bool rotatePressed = Input.GetButton("Rotate");
       if (rotatePressed)
            Debug.Log(rotatePressed);
       
       // check if move 
       if (horizontalValue != 0 || verticalValue != 0)
       {
           /*
           transform.position += new Vector3(
               speed * horizontalValue * Time.deltaTime, // x value
               speed * verticalValue * Time.deltaTime, // y value
               0); // z value*/
           
           Vector3 deltaX  = Vector3.right * (speed * horizontalValue * Time.deltaTime);
           Vector3 deltaY  = Vector3.up * (speed * verticalValue * Time.deltaTime);
           Vector3 target = transform.position +  deltaX + deltaY;
           
           // check if the next position is inside the walkable area
           if (NavArea.OverlapPoint(target))
                transform.position = target;

           

           float sign = 1;
           if (horizontalValue < 0)
               sign = -1;
           
           transform.localScale = new Vector3(
               sign * originalScale.x,
               originalScale.y,
               originalScale.z);
       }

       if (Input.GetButtonDown("Jump"))
       {
           bJumpPressed = true;
       }
    }

    private void FixedUpdate()
    {
        if (bJumpPressed)
        {
            bJumpPressed = false;
           // rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    public void DealDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, 100);
      
        hud.UpdateHealthText(health);
        
        hud.UpdateHealthBar(-damage, MaxHealth);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("enter trigger: " + other.name);
        if (other.gameObject.CompareTag("EnemyFeet"))
        {
          //  Debug.Log("Enter collider of enemy feet");
        }

        if (other.gameObject.CompareTag("EnemyPunch"))
        {
            DealDamage(100);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       // Debug.Log("exit trigger: " + other.name);
        
        if (other.gameObject.CompareTag("EnemyFeet"))
        {
          //  Debug.Log("Exit collider of enemy feet");
        }
    }
}
