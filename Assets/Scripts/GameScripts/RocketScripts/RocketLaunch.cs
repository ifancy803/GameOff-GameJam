using UnityEngine;

public class  RocketLaunch : MonoBehaviour
{
    public float rocketMoveSpeed = 20f;
    public float rocketRotationSpeed = 100f; 
    public KeyCode startKey = KeyCode.S;

    public static bool isLaunched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isLaunched = false;
        Debug.Log("按 " + startKey + " 键开始移动火箭");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(startKey))
        {
            isLaunched = true;
            Debug.Log("Start moving！");
        }
        
        if (isLaunched)
        {
            MoveRocket();
        }
    
    }
    void MoveRocket()
    {
        // 向前移动
        transform.Translate(transform.right * (rocketMoveSpeed * Time.deltaTime));
        
        
        
        
    }
}
