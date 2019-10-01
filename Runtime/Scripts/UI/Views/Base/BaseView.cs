using Toastapp.MVVM;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class BaseView<T> : UnityView
{
    [Space(10)]

    [SerializeField]
    private AppearanceType appearanceType = AppearanceType.Default;

    [SerializeField]
    private float duration = 0.2f;

    protected CanvasGroup CanvasGroup
    {
        get;
        set;
    }

    protected RectTransform RectTransform
    {
        get;
        set;
    }

    protected T ViewModel
    {
        get;
        set;
    }

    public override void Awake()
    {
        base.Awake();

        this.CanvasGroup = this.GetComponent<CanvasGroup>();
        this.RectTransform = this.GetComponent<RectTransform>();
        this.ViewModel = this.GetComponent<T>();
    }

    public override void Start()
    {
        base.Start();

        this.ShowView();
    }

    public virtual void ShowView()
    {
        this.CanvasGroup.alpha = 1;
    }
}
