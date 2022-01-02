using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.GameOver
{
    public class TitleButton : MonoBehaviour
    {
        [SerializeField] private GameObject loadSceneAsync = default;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClick()
        {
            loadSceneAsync.GetComponent<Async.LoadSceneAsync>()
                .LoadScene("SceneTitle");
        }
    }
}
