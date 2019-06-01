using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScript : MonoBehaviour
{
    // Scroll main texture based on time
    [SerializeField]
    [Range (0.001f, 1f)]
    private float scrollSpeed = 0.5f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float fOffset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(fOffset, 0));
    }
}
