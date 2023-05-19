using UnityEngine;

[RequireComponent(typeof(Outliner))]
public class Selectable : MonoBehaviour, ISelectable
{
    public long Id { get; set; }
    public GameObject GameObjectReference => gameObject;
    public Outliner OutlinerReference  => GetComponent<Outliner>();
    [field: SerializeField] public Outliner.Mode DefaultOutlinerMode { get; set; } = Outliner.Mode.OutlineVisible;
    [field: SerializeField] public Color DefaultOutlinerColor { get; set; } = Color.red;
    [field: SerializeField, Range(0f, 10f)] public float DefaultOutlinerWidth { get; set; } = 5f;
    [field: SerializeField] public GameObject CircleBottom { get; set; }
    [field: SerializeField] public bool CanUseOutliner { get; set; } = true;
    public bool IsSelected { get; set; } = false;
    [field: SerializeField] public bool CanSelect { get; set; } = true;


    public void Start()
    {
        OutlinerReference.OutlineMode = DefaultOutlinerMode;
        OutlinerReference.OutlineColor = DefaultOutlinerColor;
        OutlinerReference.OutlineWidth = DefaultOutlinerWidth;
        OutlinerReference.OnValidate();
        
        if (!GetComponent<Character>())
            OutlinerReference.enabled = false;
        
        SelectionManager.Instance.selectableObjects.Add(this);
    }

    public void OnDestroy()
    {
        if (SelectionManager.Instance)
            SelectionManager.Instance.selectableObjects.Remove(this);
    }

    // Selection
    public void OnHover()
    {
        if (OutlinerReference && CanUseOutliner)
            OutlinerReference.enabled = true;
    }
    
    public void OnHoverEnd()
    {
        if (OutlinerReference && CanUseOutliner)
            OutlinerReference.enabled = false;
    }
    
    public void OnSelect()
    {
        if (!CanSelect)
            return;
        
        if (CircleBottom)
            CircleBottom.SetActive(true);
        
        IsSelected = true;
    }
    
    public void OnDeSelect()
    {
        if (CircleBottom)
            CircleBottom.SetActive(false);
        
        IsSelected = false;
    }
}
