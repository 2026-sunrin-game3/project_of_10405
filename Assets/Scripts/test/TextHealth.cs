using TMPro;
using UnityEngine;

public class TextHealth : MonoBehaviour
{
    public EntityHealth p;
    public EntityHealth e;

    private TextMeshProUGUI t;

    void Start()
    {
        t = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        float pl = p.health;
        float en = e.health;
        t.text = $"player : {pl}, enemy : {en}";
    }
}
