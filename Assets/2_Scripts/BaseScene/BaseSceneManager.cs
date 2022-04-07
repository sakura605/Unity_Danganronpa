using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneManager : MonoBehaviour
{
    [SerializeField] GameObject LoadingWnd;
    static BaseSceneManager uniqueInstance;

    public static BaseSceneManager baseinstance
    {
        get
        {
            return uniqueInstance;
        }
    }

    void Awake()
    {
        uniqueInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartVideoScene();
    }

    IEnumerator LoadingScene(string remove, string load = "")
    {
        AsyncOperation AOper;
        GameObject go = Instantiate(LoadingWnd);

        if (remove != string.Empty)
        {
            AOper = SceneManager.UnloadSceneAsync(remove);

            while (!AOper.isDone)
            {
                yield return null;
            }
        }
        yield return new WaitForSeconds(1.5f);

        AOper = SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive);

        Destroy(go);
    }

    public void StartVideoScene()
    {
        SceneManager.LoadSceneAsync("StartVideoScene", LoadSceneMode.Additive);
    }
    public void StartTitleScene(string removeName = "")
    {
        StartCoroutine(LoadingScene(removeName, "TitleScene"));
    }

    public void StartPrologueScene(string removeName = "")
    {
        StartCoroutine(LoadingScene(removeName, "PrologueScene"));
    }

    public void StartClassRoomScene(string removeName = "")
    {
        StartCoroutine(LoadingScene(removeName, "ClassRoomScene"));
    }

}
