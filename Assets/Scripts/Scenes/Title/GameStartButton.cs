using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Title
{
    public class GameStartButton : MonoBehaviour
    {
        [SerializeField] private GameObject loadSceneAsync;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClicked()
        {
            loadSceneAsync.GetComponent<Async.LoadSceneAsync>()
                .LoadScene("SceneGameMain");
        }
    }
}
