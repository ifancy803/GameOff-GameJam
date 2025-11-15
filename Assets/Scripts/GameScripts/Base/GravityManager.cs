using System;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void Start()
    {
        Physics2D.gravity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
