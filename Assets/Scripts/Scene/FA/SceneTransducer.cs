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

    public void Exec(string nextScene)
    {
        sceneExit.GetComponent<SceneExit>()
            .Exec()
            .OnComplete(() => {
                loadSceneAsync.GetComponent<LoadSceneAsync>()
                    .LoadScene(nextScene);
            });
    }
}
