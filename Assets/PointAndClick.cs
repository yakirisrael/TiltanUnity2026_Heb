using UnityEngine;

public class PointAndClick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHover(out string objectName))
        {
         //   Debug.Log("Hover object: " +  objectName);
        }
    }

    public bool IsHover(out string objectName)
    {
        objectName = "";
        
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit =  Physics2D.Raycast(worldPos,
            Vector2.zero, 
            Mathf.Infinity, 
            LayerMask.GetMask("Hotspot"));

        if (hit == null || hit .collider == null)
            return false;
        
        objectName = hit.collider.name;
        return hit.collider != null;
    }
}
