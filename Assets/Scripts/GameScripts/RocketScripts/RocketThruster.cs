using UnityEngine;

public class RocketThruster : MonoBehaviour
{
    [Header("推进设置")]
    public float thrustPower = 80f;
    public float thrustTime = 0.1f;
    public int maxThrusts = 8;
    public KeyCode thrustKey = KeyCode.Space;
    
    [Header("视觉效果")]
    public ParticleSystem thrustParticle;
    public float particleDuration = 0.5f;
    
    private Rigidbody2D rb;
    private int thrustCount;
    private bool isThrusting;
    private ParticleSystem currentParticle;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thrustCount = maxThrusts;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(thrustKey) && thrustCount > 0 && !isThrusting)
        {
            SingleThrust();
        }
    }
    
    void SingleThrust()
    {
        // 消耗次数
        thrustCount--;
        isThrusting = true;
        
        // 创建粒子效果
        if (thrustParticle != null)
        {
            currentParticle = Instantiate(thrustParticle, transform.position, transform.rotation);
            currentParticle.transform.SetParent(transform);
            Destroy(currentParticle.gameObject, particleDuration);
        }
        
        Debug.Log($"推进！剩余次数: {thrustCount}");
        
        // 0.1秒后结束推进状态
        Invoke(nameof(EndThrust), thrustTime);
    }
    
    void EndThrust()
    {
        isThrusting = false;
    }
    
    void FixedUpdate()
    {
        if (isThrusting)
        {
            // 短时间施加推力
            rb.AddForce(transform.right * thrustPower, ForceMode2D.Force);
        }
    }
    
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 250, 120));
        GUILayout.Label($"🚀 推进次数: {thrustCount}/{maxThrusts}");
        GUILayout.Label($"状态: {(isThrusting ? "推进中" : "准备就绪")}");
        GUILayout.Label("按空格键单次推进");
        GUILayout.EndArea();
    }
}