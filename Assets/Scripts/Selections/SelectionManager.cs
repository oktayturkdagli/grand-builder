using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : Singleton<SelectionManager>
{
    private Camera cam;
    [SerializeField] private RectTransform boxVisual;
    private Rect selectionBox;
    private Vector2 startPosition;
    private Vector2 endPosition;
    public LayerMask selectable;
    private ISelectable onHoveringObject = null;
    [SerializeField] public bool isDraggingSelectionBox;
    
    public readonly List<ISelectable> selectableObjects = new List<ISelectable>();
    public readonly List<ISelectable> selectedObjects = new List<ISelectable>();
    
    private void Start()
    {
        cam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }
    
    private void Update()
    {
        if (BuildingManager.Instance.isDraggingABuilding)
            return;
        
        IsMouseHovering();
        Drag();
    }
    
    private void IsMouseHovering()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue()); // Creates a Ray from the mouse position

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, selectable))
        {
            if (hit.collider.gameObject.TryGetComponent(out ISelectable selectableObject))
            {
                if (onHoveringObject == selectableObject)
                    return;
                
                IsHoveringEnd();
                onHoveringObject = selectableObject;
                selectableObject.OnHover();
            }
            else
            {
                IsHoveringEnd();
            }
        }
        else
        {
            IsHoveringEnd();
        }
    }
    
    private void IsHoveringEnd()
    {
        if (onHoveringObject == null)
            return;
        
        onHoveringObject.OnHoverEnd();
        onHoveringObject = null;
    }
    
    public void OnLeftMousePerform()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue()); // Creates a Ray from the mouse position

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, selectable))
        {
            if (Keyboard.current.leftShiftKey.isPressed)
            {
                // Debug.Log("Pressed left shift.");
                if (hit.collider.gameObject.TryGetComponent(out ISelectable selectableObject)) // Is this object selectable?
                {
                    ShiftClickSelect(selectableObject);
                }
            }
            else
            {
                if (hit.collider.gameObject.TryGetComponent(out ISelectable selectableObject))
                {
                    ClickSelect(selectableObject);
                }
            }
        }
        else
        {
            if (!Keyboard.current.leftShiftKey.isPressed)
            {
                DeselectAll();
            }
        }
    }

    private void Drag()
    {
        // When clicked
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isDraggingSelectionBox = true;
            startPosition = Input.mousePosition;
            selectionBox = new Rect();
        }

        // When release click
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDraggingSelectionBox = false;
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }
        
        // Dragging
        if (isDraggingSelectionBox)
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }
    }

    private void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;
        
        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        boxVisual.sizeDelta = boxSize;
    }

    private void DrawSelection()
    {
        // Do X Calculations
        if (Input.mousePosition.x < startPosition.x)
        {
            // Dragging left
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            // Dragging Right
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }
        
        // Do Y Calculations
        if (Input.mousePosition.y < startPosition.y)
        {
            // Dragging Down
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            // Dragging Up
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    private void SelectUnits()
    {
        // Loop all the selectable units
        foreach (var unit in selectableObjects)
        {
            // If unit is within the bounds of the selection rect
            if (selectionBox.Contains(cam.WorldToScreenPoint(unit.GameObjectReference.transform.position)))
            {
                // If any unit is within the selection add them to select
                DragSelect(unit);
            }
        }
    }

    private void ClickSelect(ISelectable unitToAdd)
    {
        DeselectAll();
        selectedObjects.Add(unitToAdd);
        unitToAdd.CircleBottom.SetActive(true);
        unitToAdd.OnSelect();
    }

    private void ShiftClickSelect(ISelectable unitToAdd)
    {
        if (!selectedObjects.Contains(unitToAdd))
        {
            selectedObjects.Add(unitToAdd);
            unitToAdd.OnSelect();
        }
        else
        {
            unitToAdd.OnDeSelect();
            selectedObjects.Remove(unitToAdd);
        }
    }

    private void DragSelect(ISelectable unitToAdd)
    {
        if (!unitToAdd.GameObjectReference.GetComponent<Character>())
            return;
        
        if (!selectedObjects.Contains(unitToAdd))
        {
            selectedObjects.Add(unitToAdd);
            unitToAdd.OnSelect();
        }
    }

    private void DeselectAll()
    {
        foreach (var unit in selectedObjects)
        {
            unit.CircleBottom.SetActive(false);
            unit.OnDeSelect();
        }
        selectedObjects.Clear();
    }
    
    public void Deselect(ISelectable unitToDeselect)
    {
        unitToDeselect.OnDeSelect();
        selectedObjects.Remove(unitToDeselect);
    }
}
