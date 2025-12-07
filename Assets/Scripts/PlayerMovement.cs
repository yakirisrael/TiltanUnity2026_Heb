using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;

    Vector3 originalScale;
    void Awake()
    {
        originalScale = transform.localScale;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
       float verticalValue = Input.GetAxis("Vertical");
       float horizontalValue = Input.GetAxis("Horizontal");
       
       Debug.Log(Time.deltaTime);
       
       // check if move 
       if (horizontalValue != 0 || verticalValue != 0)
       {
           
       transform.position += new Vector3(
           speed * horizontalValue * Time.deltaTime, // x value
           speed * verticalValue * Time.deltaTime, // y value
           0); // z value


       float sign = 1;
       if (horizontalValue < 0)
           sign = -1;
       
           transform.localScale = new Vector3(
               sign * originalScale.x,
               originalScale.y,
               originalScale.z);
       }
    } 
}
