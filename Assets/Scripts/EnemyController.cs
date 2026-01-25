using System;
using System.Collections;
using UnityEngine;


public enum enemyState
{
    Unaware,
    ChasePlayer,
    Attack,
    WaitForAttack,
    ReturnToPosition,
    Dead
}

public class EnemyController : MonoBehaviour
{
    private enemyState state = enemyState.Unaware;
    public PlayerMovement player;

    public float deadZone = 0.35f;
    public float delta = 0.05f;
    
    private Animator animator;
    private bool FirstPunch = true;

    public int damage = 10;

    public float delayBetweenAttacks = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        GetReferenceToPlayer();
    }

    void GetReferenceToPlayer()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        // found the player
        if (obj != null)
            player = obj.GetComponent<PlayerMovement>();
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
           
           // not walking
            animator.SetBool("IsWalking", false);

            if (state == enemyState.ChasePlayer)
                StartCoroutine(Attack());
        }
        else
        {
           // Debug.Log("chasing");
            transform.position += diff.normalized* delta *Time.deltaTime;
            animator.SetBool("IsWalking", true);
            
            state = enemyState.ChasePlayer;
        }

    }

    IEnumerator Attack()
    {
        state = enemyState.Attack;

        while (true)
        {
            animator.SetTrigger("Punch");
            
            while (!IsAnimationFinished("EnemyPunch"))
            {
                yield return null;
            }
            // Debug.Log("IsAnimationFinished");

           // player.GetComponent<PlayerMovement>().Hit();

                // call the function that deals damage on hud
             player.GetComponent<PlayerMovement>().DealDamage(damage);
            state = enemyState.WaitForAttack;
            yield return new WaitForSeconds(delayBetweenAttacks);
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
