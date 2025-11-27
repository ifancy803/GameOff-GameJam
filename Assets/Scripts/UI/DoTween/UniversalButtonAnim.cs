using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

//公有组件，用以自定义参数
[RequireComponent(typeof(Button), typeof(RectTransform), typeof(Image))]
public class UniversalButtonAnim : MonoBehaviour
{
    [Header("点击动效参数")]
    [Tooltip("点击缩放幅度")]
    public Vector3 ClickScale = new Vector3(0.9f, 0.9f, 1f);
    [Tooltip("点击动画时长")]
    public float ClickDuration = 0.1f;
    [Tooltip("点击后的高亮颜色")]
    public Color clickColor = Color.gray;
    [Tooltip("颜色动画时长")]
    public float colorDuration = 0.1f;

    [Header("悬停动效参数")]
    [Tooltip("悬停放大比例")]
    public Vector3 hoverScale = new Vector3(1.05f, 1.05f, 1f);
    [Tooltip("悬停动画时长")]
    public float hoverDuration = 0.2f;

    // 私有组件
    private RectTransform _btnRect;
    private Image _btnImage;
    private Color _defaultColor;
    private Button _btn;

    void Awake()
    {
        // 获取组件
        _btnRect = GetComponent<RectTransform>();
        _btnImage = GetComponent<Image>();
        _btn = GetComponent<Button>();
        _defaultColor = _btnImage.color;

        // 绑定事件
        BindEvents();
    }

    // 绑定点击、悬停事件
    private void BindEvents()
    {
        // 点击事件
        _btn.onClick.AddListener(OnButtonClick);

        // 动态绑定悬停事件
        EventTrigger trigger = GetComponent<EventTrigger>() ?? gameObject.AddComponent<EventTrigger>();
        trigger.triggers.Clear();

        // 鼠标移入
        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => OnButtonEnter());
        trigger.triggers.Add(enterEntry);

        // 鼠标移出
        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => OnButtonExit());
        trigger.triggers.Add(exitEntry);
    }

    // 点击动效
    public void OnButtonClick()
    {
        // 重置状态（防止点击太快导致的无法回正）
        _btnRect.DOKill();
        _btnImage.DOKill();
        //_btnRect.localScale = Vector3.one;
        _btnImage.color = _defaultColor;

        // 使用配置的参数执行动画
        _btnRect.DOScale(ClickScale, ClickDuration)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.OutSine);
        _btnImage.DOColor(clickColor, colorDuration).SetLoops(2, LoopType.Yoyo);
    }

    // 悬停移入
    public void OnButtonEnter()
    {
        _btnRect.DOScale(hoverScale, hoverDuration)
        .SetEase(Ease.OutSine);
    }

    // 悬停移出
    public void OnButtonExit()
    {
        _btnRect.DOScale(Vector3.one, hoverDuration)
            .SetEase(Ease.OutSine);
    }
}