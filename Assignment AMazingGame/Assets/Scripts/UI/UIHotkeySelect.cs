using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <seealso>
/// https://forum.unity.com/threads/tab-between-input-fields.263779/page-2#post-3160808
/// </seealso> 
public class UIHotkeySelect : MonoBehaviour {
    private List<Selectable> orderedSelectables;
 
    private void Awake() {
        orderedSelectables = new List<Selectable>();
    }
 
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            HandleHotkeySelect(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift), true, false); // Navigate backward when holding shift, else navigate forward.
        }
 
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            HandleHotkeySelect(false, false, true);
        }
    }
 
    private void HandleHotkeySelect(bool isNavigateBackward, bool isWrapAround, bool isEnterSelect) {
        SortSelectables();
 
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject != null && selectedObject.activeInHierarchy) { // Ensure a selection exists and is not an inactive object.
            Selectable currentSelection = selectedObject.GetComponent<Selectable>();
            if (currentSelection != null) {
                if (isEnterSelect) {
                    if (currentSelection.GetComponent<InputField>() != null) {
                        ApplyEnterSelect(FindNextSelectable(orderedSelectables.IndexOf(currentSelection), isNavigateBackward, isWrapAround));
                    }
                } else { // Tab select.
                    Selectable nextSelection = FindNextSelectable(orderedSelectables.IndexOf(currentSelection), isNavigateBackward, isWrapAround);
                    if (nextSelection != null) {
                        nextSelection.Select();
                    }
                }
            } else {
                SelectFirstSelectable(isEnterSelect);
            }
        } else {
            SelectFirstSelectable(isEnterSelect);
        }
    }
 
    ///<summary> Selects an input field or button, activating the button if one is found. </summary>
    private void ApplyEnterSelect(Selectable selectionToApply) {
        if (selectionToApply != null) {
            if (selectionToApply.GetComponent<InputField>() != null) {
                selectionToApply.Select();
            } else {
                Button selectedButton = selectionToApply.GetComponent<Button>();
                if (selectedButton != null) {
                    selectionToApply.Select();
                    selectedButton.OnPointerClick(new PointerEventData(EventSystem.current));
                }
            }
        }
    }
 
    private void SelectFirstSelectable(bool isEnterSelect) {
        if (orderedSelectables.Count > 0) {
            Selectable firstSelectable = orderedSelectables[0];
            if (isEnterSelect) {
                ApplyEnterSelect(firstSelectable);
            } else {
                firstSelectable.Select();
            }
        }
    }
 
    private Selectable FindNextSelectable(int currentSelectableIndex, bool isNavigateBackward, bool isWrapAround) {
        Selectable nextSelection = null;
 
        int totalSelectables = orderedSelectables.Count;
        if (totalSelectables > 1) {
            if (isNavigateBackward) {
                if (currentSelectableIndex == 0) {
                    nextSelection = (isWrapAround) ? orderedSelectables[totalSelectables - 1] : null;
                } else {
                    nextSelection = orderedSelectables[currentSelectableIndex - 1];
                }
            } else { // Navigate forward.
                if (currentSelectableIndex == (totalSelectables - 1)) {
                    nextSelection = (isWrapAround) ? orderedSelectables[0] : null;
                } else {
                    nextSelection = orderedSelectables[currentSelectableIndex + 1];
                }
            }
        }
 
        return (nextSelection);
    }
 
    private void SortSelectables() {
		int totalSelectables = Selectable.allSelectablesArray.Length;
        orderedSelectables = new List<Selectable>(totalSelectables);
        for (int index = 0; index < totalSelectables; ++index) {
            Selectable selectable = Selectable.allSelectablesArray[index];
            orderedSelectables.Insert(FindSortedIndexForSelectable(index, selectable), selectable);
        }
    }
 
    ///<summary> Recursively finds the sorted index by positional order within m_orderedSelectables (positional order is determined from left-to-right followed by top-to-bottom). </summary>
    private int FindSortedIndexForSelectable(int _selectableIndex, Selectable _selectableToSort) {
        int sortedIndex = _selectableIndex;
        if (_selectableIndex > 0) {
            int previousIndex = _selectableIndex - 1;
            Vector3 previousSelectablePosition = orderedSelectables[previousIndex].transform.position;
            Vector3 selectablePositionToSort = _selectableToSort.transform.position;
 
            if (previousSelectablePosition.y == selectablePositionToSort.y) {
                if (previousSelectablePosition.x > selectablePositionToSort.x) {     // Previous selectable is in front, try the previous index:
                    sortedIndex = FindSortedIndexForSelectable(previousIndex, _selectableToSort);
                }
            } else if (previousSelectablePosition.y < selectablePositionToSort.y) {  // Previous selectable is in front, try the previous index:
                sortedIndex = FindSortedIndexForSelectable(previousIndex, _selectableToSort);
            }
        }
        return (sortedIndex);
    }
}
