using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputHandler : MonoBehaviour
{
    [SerializeField] Transform levelsContainer;
    private float minX = 0, maxX = 0;
    private void Start()
    {
        maxX = levelsContainer.GetChild(levelsContainer.childCount - 1).position.x - 5;
        minX = levelsContainer.GetChild(0).position.x + 5;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0.1f)
        {
            this.transform.position += new Vector3(Input.mouseScrollDelta.y, 0, 0);
            if (this.transform.position.x < minX)
            {
                this.transform.position = new Vector3(minX, 0, 0);
            }
            if (this.transform.position.x > maxX)
            {
                this.transform.position = new Vector3(maxX, 0, 0);
            }
        }
    }
    private void CastRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        LoadLevelScript level;
        if (hit && hit.transform.TryGetComponent<LoadLevelScript>(out level))
        {
            level.LoadLevel();
        }
    }
}