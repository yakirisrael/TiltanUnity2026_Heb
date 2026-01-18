using System;
using UnityEngine;


public enum enemyState
{
    Unaware,
    ChasePlayer,
    Attack,
    ReturnToPosition,
    Dead
}

public class EnemyController : MonoBehaviour
{
    private enemyState state = enemyState.Unaware;
    public GameObject player;

    public float deadZone = 0.35f;
    public float delta = 0.05f;
    
    private Animator animator;
    private bool FirstPunch = true;

    public int damage = 10;
   
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 enemyPosition = transform.position;

        Vector3 playerPosition = player.transform.position;

        Vector3 diff = playerPosition - enemyPosition;
        float distance = diff.magnitude;
       // Debug.Log(distance);

        if (distance < deadZone)
        {
           // Debug.Log("Stop chasing");
            animator.SetBool("IsWalking", false);

            if (FirstPunch)
            {
                animator.SetTrigger("Punch");

               if (IsAnimationFinished("EnemyPunch"))
               {
                  // Debug.Log("IsAnimationFinished");
                   
                   player.GetComponent<PlayerMovement>().Hit();
                   FirstPunch = false;
                   
                   // call the function that deals damage on hud
                //   player.GetComponent<PlayerMovement>().DealDamage(damage);
               }
            }
        }
        else
        {
           // Debug.Log("chasing");
            transform.position += diff.normalized* delta *Time.deltaTime;
            animator.SetBool("IsWalking", true);
        }

    }

    bool IsAnimationFinished(string animationName)
    { 
       Animator anim = GetComponent<Animator>();
       AnimatorStateInfo info =  anim.GetCurrentAnimatorStateInfo(0);
       if (info.IsName(animationName))
       {
          // Debug.Log(info.normalizedTime);
           if (info.normalizedTime >= 0.95f)
           {
               return true;
           }

       }
       return false;
    }


}
