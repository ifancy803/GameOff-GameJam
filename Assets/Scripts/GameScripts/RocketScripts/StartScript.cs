using UnityEngine;

public class StartScript : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 100f; 
    public KeyCode startKey = KeyCode.S;

    public bool isMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMoving = false;
        Debug.Log("按 " + startKey + " 键开始移动火箭");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(startKey))
        {
            isMoving = true;
            Debug.Log("Start moving！");
        }
        
        if (isMoving)
        {
            MoveRocket();
        }
    }
    
    void MoveRocket()
    {
        // 向前移动
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        
    }
}
