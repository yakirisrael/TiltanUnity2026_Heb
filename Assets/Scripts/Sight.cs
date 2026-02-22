using UnityEngine;

public class Sight : MonoBehaviour
{

    [SerializeField]
    private float sightDistance = 5f;
    
    [SerializeField]
    private LayerMask sightMask;
    public bool IsSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, 
            Vector2.left, 
            sightDistance, 
            sightMask);
       
        Debug.DrawRay(transform.position, Vector2.left * sightDistance, Color.red);
        if (hit.collider != null)
        {
          //  Debug.Log("Collider " + hit.collider.name + " distance: " + hit.distance);
            Debug.DrawRay(transform.position, Vector2.left * hit.distance, Color.green);
            if (hit.collider.CompareTag("PlayerHitBox"))
                return true;
        }
        return false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
