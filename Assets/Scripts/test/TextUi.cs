using TMPro;
using UnityEngine;

public class TextUi : MonoBehaviour  
{
    public EntityHealth p_health, e_health;

    private TextMeshProUGUI t;
    void Start()
    {
        t = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        t.text = $"player = {p_health.health}, enemy = {e_health.health}";
    }
}
