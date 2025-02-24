using UnityEngine;
using System.Linq;

public class BackgroundLoop : MonoBehaviour
{
    public GameObject[] backgrounds;
    public float scrollSpeed = 2f;
    private float backgroundWidth;

    void Start()
    {
        if (backgrounds == null || backgrounds.Length < 2)
        {
            Debug.LogError("Assign at least 2 backgrounds in the array!");
            return;
        }

        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position = new Vector3(i * backgroundWidth, 0, 0);
        }
    }

    void Update()
    {
        foreach (GameObject bg in backgrounds)
        {
            bg.transform.position += Vector3.left * (scrollSpeed * Time.deltaTime);
        }

        foreach (GameObject bg in backgrounds)
        {
            if (bg.transform.position.x <= -backgroundWidth)
            {
                RepositionBackground(bg);
            }
        }
    }

    void RepositionBackground(GameObject bg)
    {
        GameObject lastBg = backgrounds.OrderBy(b => b.transform.position.x).Last();
        bg.transform.position = new Vector3(lastBg.transform.position.x + backgroundWidth, bg.transform.position.y, bg.transform.position.z);
    }
}