using UnityEngine;
using UnityEngine.SceneManagement;

public class Goscene : MonoBehaviour
{
    public void Re()
    {
        SceneManager.LoadScene("Phase1");
    }
    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
}
