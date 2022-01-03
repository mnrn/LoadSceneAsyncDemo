using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SceneTransducer : MonoBehaviour
{
    [SerializeField] private GameObject sceneExit = default;
    [SerializeField] private LoadSceneAsync loadSceneAsync = default;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// シーンの遷移を行います。
    /// </summary>
    /// <param name="nextScene">遷移先のシーン名</param>
    public void Exec(string nextScene)
    {
        // シーンはSceneExit状態となりシーンのロードを行います。
        sceneExit.GetComponent<SceneExit>()
            .Exec()
            .OnComplete(() => {
                loadSceneAsync.GetComponent<LoadSceneAsync>()
                    .Exec(nextScene);
            });
    }
}
