using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    [SerializeField] private Sprite spriteEmpty, spriteCrossed;
    private int x = 0, y = 0;
    private CellState state = CellState.Empty;
    private bool CanBeChanged = true;
    public void SetCoord(int w, int h)
    {
        x = w;
        y = h;
    }
    public CellInfo GetCell()
    {
        return new CellInfo(x, y, state);
    }
    public bool GetCanBeChanged()
    {
        return CanBeChanged;
    }
/// <summary> 
/// Apply new state to cell AND return new cellinfo
/// </summary>
    public CellInfo ApplyCellState(int v)
    {
        if (!CanBeChanged) return new CellInfo(x, y, state);
        
        if (v == 1 && state == CellState.Empty)
        {
            state = CellState.Filled;
        }
        else if (v == 1 && state == CellState.Filled)
        {
            state = CellState.Empty;
        }
        else if (v == 0 && state == CellState.Empty)
        {
            state = CellState.Crossed;
        }
        else if (v == 0 && state == CellState.Crossed)
        {
            state = CellState.Empty;
        }

        ChangeVisualState();

        return new CellInfo(x, y, state);
    }
    public void ApplyCellStateFromHelper(CellState s)
    {
        state = s;
        CanBeChanged = false;

        ChangeVisualState();
    }
    private void ChangeVisualState()
    {
        SpriteRenderer renderer = transform.GetComponent<SpriteRenderer>();
        switch (state)
        {
        case CellState.Empty :
            renderer.sprite = spriteEmpty;
            renderer.color = Color.white;
            break;
        case CellState.Filled :
            renderer.color = Color.gray;
            break;
        case CellState.Crossed :
            renderer.sprite = spriteCrossed;
            break;
        default :
            break;
        }
    }
}
