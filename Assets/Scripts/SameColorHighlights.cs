using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameColorHighlights : MonoBehaviour
{
    public Color highlightColor;

    // Start is called before the first frame update
    void Start()
    {
        VRTK.VRTK_InteractObjectHighlighter[] highlights = FindObjectsOfType<VRTK.VRTK_InteractObjectHighlighter>();
        for(int i = 0; i < highlights.Length; i++) { highlights[i].touchHighlight = highlights[i].nearTouchHighlight = highlightColor; }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2)) { FindObjectOfType<Player>().TakeDamage(50); }
    }
}
