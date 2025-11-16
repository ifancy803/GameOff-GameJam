using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public KeyCode restartKey=KeyCode.R;
    
    [Header("游戏对象")]
    public GameObject rocketPrefab;
    private GameObject currentRocket;
    
    [Header("游戏状态")]
    public int score = 0;
    public float gameTime = 0f;
    public bool isGameRunning = true;

    public SpawnPoint spawnPoint = new SpawnPoint();
    public static GameManager Instance;

    public ObjectEventSO rocketGeneratedEvent;
    
    private void Awake()
    {
        // 确保只有一个实例存在
        if (Instance == null)
        {
            Instance = this;        // 设置当前对象为实例
            DontDestroyOnLoad(gameObject); // 可选：跨场景不销毁
        }
        else
        {
            Destroy(gameObject);    // 如果已存在实例，销毁自己
        }
    }
    
    void Start()
    {
        StartNewGame();
    }
    
    void Update()
    {
        if (RocketLaunch.isLaunched)
        {
            gameTime += Time.deltaTime;
        }
        
        // 重新开始输入检测
        if (Input.GetKeyDown(restartKey))
        {
            RestartGame();
        }
        
        // 退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    
    void StartNewGame()
    {
        // 重置游戏状态
        Physics2D.gravity = new Vector2(0, 0);
        score = 0;
        gameTime = 0f;
        RocketLaunch.isLaunched = true;
        
        // 生成火箭
        SpawnRocket();
    }
    
    void SpawnRocket()
    {
        // 销毁旧火箭
        if (currentRocket != null)
        {
            Destroy(currentRocket);
        }

        spawnPoint.position = new Vector3(-45, 0, 0);
        spawnPoint.rotation = Quaternion.identity;
        
        // 生成新火箭
        currentRocket = Instantiate(rocketPrefab, spawnPoint.position, spawnPoint.rotation);
        
        // 广播
        rocketGeneratedEvent.RaiseEvent(currentRocket,this);
        
        
    }
    
    public void RestartGame()
    {
        Debug.Log("=== 游戏重新开始 ===");
        
        SpawnRocket();
        
        // 重置游戏状态
        score = 0;
        gameTime = 0f;
        RocketLaunch.isLaunched = true;
        
        // 触发重新开始事件（如果有其他系统需要知道）
        OnGameRestart?.Invoke();
    }
    
    // 重新开始事件（供其他系统订阅）
    public event System.Action OnGameRestart;
    
    // 游戏结束逻辑
    public void GameOver()
    {
        isGameRunning = false;
        Debug.Log($"游戏结束！得分: {score}, 时间: {gameTime:F1}秒");
    }
}

public class SpawnPoint
{
    public Vector3 position;
    public Quaternion rotation;
}