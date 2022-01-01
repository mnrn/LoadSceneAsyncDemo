using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Async
{
    public class LoadSceneAsync : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private Slider slider;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadScene(string scene)
        {
            StartCoroutine(LoadSceneCoro(scene));
        }

        IEnumerator LoadSceneCoro(string scene)
        {
            slider.value = 0.0f;
            canvas.SetActive(true);
            yield return new WaitForSeconds(1f);

            // ロードを開始します
            var asyncOp = SceneManager.LoadSceneAsync(scene);

            // OKするまでシーンをアクティブにしません
            asyncOp.allowSceneActivation = false;
            Debug.Log("Progress :" + asyncOp.progress);

            while (true)
            {
                yield return null;
                slider.value = asyncOp.progress;
                Debug.Log("Progress :" + asyncOp.progress);

                if (asyncOp.progress >= 0.9f)
                {
                    slider.value = 1.0f;
                    break;
                }
            }

            yield return new WaitForSeconds(1f);
            asyncOp.allowSceneActivation = true;
            canvas.SetActive(false);
        }
    }
}
