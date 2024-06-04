using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solid : MonoBehaviour
{

    private SpriteRenderer myRenderer;
    private Shader myMaterial;
    public Color _color;
    // Start is called before the first frame update
    void ColorSprite()
    {
        myRenderer.material.shader = myMaterial;
        myRenderer.color = _color;
    }

    public void Finish()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myMaterial = Shader.Find("GUI/Text Shader");
    }

    // Update is called once per frame
    void Update()
    {
        ColorSprite();
    }
}
