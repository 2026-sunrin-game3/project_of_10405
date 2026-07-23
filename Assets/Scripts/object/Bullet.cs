using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float y = 0f;
    public float x = 1f;
    float time = 0.1f;
    void Update()
    {
        if(time <= 0)
        {
            Destroy(gameObject);
        }
        time -= Time.deltaTime;
        transform.position += new Vector3(45 * Time.deltaTime * x, y * Time.deltaTime,0);
    }
}
