using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenUIManager : MonoBehaviour
{
    [SerializeField] private Animator loadingScreenAnim;

    private LoadingMenuView mLoadingMenu;

    private void Awake()
    {
        mLoadingMenu = FindObjectOfType<LoadingMenuView>();
    }

    public void LoadLoadingScreen()
    {
        loadingScreenAnim.SetTrigger("Intro");
        mLoadingMenu.startLoading = true;
    }
}
