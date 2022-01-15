using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private MainScreenUIManager mUIManager;

    private void Awake()
    {
        mUIManager = FindObjectOfType<MainScreenUIManager>();
    }

    public void OnPlayClicked()
    {
        Debug.Log("Play Clicked");
        anim.SetTrigger("Outro");
        StartCoroutine(DisableMainMenu());
    }

    IEnumerator DisableMainMenu()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        mUIManager.LoadLoadingScreen();
    }
}
