using UnityEngine;

public class RocketThruster : MonoBehaviour
{
    [Header("推力设置")]
    public float thrustForce = 100f;           // 推力大小
    public KeyCode thrustKey = KeyCode.Space;  // 推力按键
    
    [Header("燃料系统")]
    public float maxFuel = 100f;              // 最大燃料量
    public float fuelConsumptionRate = 10f;   // 每秒消耗燃料量
    public float currentFuel;                 // 当前燃料量
    
    [Header("视觉效果")]
    public ParticleSystem thrustParticles;    // 推力粒子效果
    public AudioClip thrustSound;            // 推力音效
    
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool isThrusting = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        currentFuel = maxFuel;  // 初始化燃料
    }
    
    void Update()
    {
        HandleThrustInput();
        UpdateThrustEffects();
    }
    
    void FixedUpdate()
    {
        if (isThrusting && currentFuel > 0)
        {
            ApplyThrust();
            ConsumeFuel();
        }
    }
    
    void HandleThrustInput()
    {
        // 按下按键开始推力
        if (Input.GetKeyDown(thrustKey) && currentFuel > 0)
        {
            StartThrust();
        }
        
        // 松开按键停止推力
        if (Input.GetKeyUp(thrustKey))
        {
            StopThrust();
        }
    }
    
    void StartThrust()
    {
        isThrusting = true;
        Debug.Log("火箭推力启动！");
    }
    
    void StopThrust()
    {
        isThrusting = false;
        Debug.Log("火箭推力停止");
    }
    
    void ApplyThrust()
    {
        // 沿着火箭前方施加推力（使用 Acceleration 模式模拟真实火箭）
        rb.AddForce(transform.forward * thrustForce, ForceMode2D.Force);
    }
    
    void ConsumeFuel()
    {
        // 消耗燃料（基于时间）
        currentFuel -= fuelConsumptionRate * Time.deltaTime;
        currentFuel = Mathf.Max(0, currentFuel);  // 确保不为负
        
        // 燃料耗尽时自动停止
        if (currentFuel <= 0)
        {
            currentFuel = 0;
            StopThrust();
            Debug.Log("燃料耗尽！");
        }
    }
    
    void UpdateThrustEffects()
    {
        // 控制粒子效果
        if (thrustParticles != null)
        {
            if (isThrusting && currentFuel > 0)
            {
                if (!thrustParticles.isPlaying)
                    thrustParticles.Play();
            }
            else
            {
                if (thrustParticles.isPlaying)
                    thrustParticles.Stop();
            }
        }
        
        // 控制音效
        if (audioSource != null && thrustSound != null)
        {
            if (isThrusting && currentFuel > 0)
            {
                if (!audioSource.isPlaying)
                    audioSource.PlayOneShot(thrustSound);
            }
            else
            {
                audioSource.Stop();
            }
        }
    }
    
    // 公共方法：添加燃料
    public void AddFuel(float amount)
    {
        currentFuel = Mathf.Min(maxFuel, currentFuel + amount);
        Debug.Log($"添加燃料: {amount}, 当前燃料: {currentFuel}");
    }
    
    // 公共方法：获取燃料百分比
    public float GetFuelPercentage()
    {
        return currentFuel / maxFuel;
    }
}