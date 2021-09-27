using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0.1f)
        {
            this.transform.position += new Vector3(Input.mouseScrollDelta.y, 0, 0);
            if (this.transform.position.x < 0f)
            {
                this.transform.position = new Vector3(0, 0, 0);
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