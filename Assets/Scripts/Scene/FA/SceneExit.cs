using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class SceneExit : MonoBehaviour
{
    [SerializeField] private GameObject canvas = default;
    [SerializeField] private GameObject screen = default;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private Ease easeType = Ease.InCubic;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public DG.Tweening.Core.TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> Exec()
    {
        // まずはキャンバスを使えるようにする。
        canvas.SetActive(true);

        // 黒画面へフェードインします。
        return screen.GetComponent<Image>()
            .DOFade(endValue: 1.0f, duration: fadeDuration)
            .SetEase(easeType);
    }
}
