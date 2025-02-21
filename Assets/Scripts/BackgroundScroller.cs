using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] public float scrollSpeed = 0.5f;
    private Material _material;
    private Vector2 _offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _material = GetComponent<Renderer>().material;
        _offset = new Vector2(0, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        _material.mainTextureOffset += _offset * Time.deltaTime;
    }
}