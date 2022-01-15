using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMenuView : MonoBehaviour
{
    [SerializeField] private Image loadingBar;

    private float loadingMultiplier;

    public bool startLoading = false;

    private void Awake()
    {
        loadingMultiplier = Random.Range(0.1f, 0.45f);
    }

    private void Update()
    {
        if (startLoading)
            loadingBar.fillAmount += Time.deltaTime * loadingMultiplier;

        if (loadingBar.fillAmount >= 1)
            SceneHandler.Instance.LoadNextScene();
    }
}
