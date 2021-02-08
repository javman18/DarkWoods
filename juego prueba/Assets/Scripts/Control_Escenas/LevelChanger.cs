
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator anim;
    public int levelToLoad;
    // Update is called once per frame
    void Update()
    {
        if (Puntos.puntos >= 4)
        {
            
            FadeToLevel(3);
            Puntos.puntos = 0;
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        anim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }


}
