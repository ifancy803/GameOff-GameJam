using System;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [Header("引力属性")]
    public float mass = 1000f;
    public float gravityRadius = 50f;
    public float gravitationalConstant = 6.674f;
    
    [Header("目标设置")]
    public string targetTag = "Rocket";  // 火箭的标签
    private Rigidbody2D RocketRb;

    private void Awake()
    {
        GameObject rocketObject = GameObject.Find(targetTag);
        if (rocketObject != null)
        {
            RocketRb = rocketObject.GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        if(RocketLaunch.isLaunched)
        {
            ApplyGravityToObject(RocketRb);
        }
    }
    
    void ApplyGravityToObject(Rigidbody2D targetRb)
    {
        Vector2 direction = transform.position - targetRb.gameObject.transform.position;
        float distance = direction.magnitude;
        
        if (distance > 0.1f)
        {
            float forceMagnitude = gravitationalConstant * (mass * targetRb.mass) / (distance * distance);
            Vector3 gravityForce = direction.normalized * forceMagnitude;
            targetRb.AddForce(gravityForce, ForceMode2D.Force);
        }
    }

    public void RocketGeneratedEvent(object value)
    {
        var rb = value as GameObject;

        if (rb != null) RocketRb = rb.GetComponent<Rigidbody2D>();
    }
    
}