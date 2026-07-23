using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform pl;
    void Update()
    {
        float x = pl.position.x;
        if(x < 0)
        {
            x = 0;
        }
        else if(x > 43.5)
        {
            x = 43.5f;
        }
        transform.position = new Vector3(x,0,-10);
    }
}
