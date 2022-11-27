using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimation : MonoBehaviour
{
    public Texture2D[] texture2Ds;
    private int cursorIndex = 0;
    private float setCursorTimer;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - setCursorTimer >= 0.1f)
        {
            cursorIndex++;
            if (cursorIndex >= texture2Ds.Length) 
            {
                cursorIndex = 0;
            }
            UpdateCursor();
            setCursorTimer = Time.time;
        }
    }

    void UpdateCursor() {
        if (cursorIndex < texture2Ds.Length) {
            Cursor.SetCursor(texture2Ds[cursorIndex], Vector2.zero, CursorMode.Auto);
        }
    }
}
