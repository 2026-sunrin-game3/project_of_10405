using System.Drawing;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float si = 0.5f;
    public float time = 0.2f;
    void Update()
    {
        if(time <= 0)
        {
            Destroy(gameObject);
        }
        si += 15f * Time.deltaTime;
        time -= Time.deltaTime;
        transform.localScale = new Vector2(1,1.5f)*si;
    }
}
