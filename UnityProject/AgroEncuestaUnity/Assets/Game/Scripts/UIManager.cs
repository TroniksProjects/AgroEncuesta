using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [SerializeField]
    private EasyTween mainScreen;
    [SerializeField]
    private EasyTween categoryMenu;
    [SerializeField]
    private EasyTween questionsScreen;
    [SerializeField]
    private EasyTween correctPopUp;
    [SerializeField]
    private EasyTween incorrectPopUp;


    private bool bEnableButtons = true;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        mainScreen.OpenCloseObjectAnimation();
    }

    public void ShowCategoryMenu ()
    {
        if(bEnableButtons)
        {
            bEnableButtons = false;
            mainScreen.OpenCloseObjectAnimation();
            categoryMenu.OpenCloseObjectAnimation();
            Invoke("EnableButtons", 2f);
        }
    }

    public void ShowSurveyScreen ()
    {
        if (bEnableButtons)
        {
            bEnableButtons = false;
            categoryMenu.OpenCloseObjectAnimation();
            questionsScreen.OpenCloseObjectAnimation();
            Invoke("EnableButtons", 2f);
        }
    }

    public void ShowCorrectPopUP()
    {
        if (bEnableButtons)
        {
            bEnableButtons = false;
            correctPopUp.OpenCloseObjectAnimation();
            Invoke("EnableButtons", 2f);
        }
    }

    public void ShowIncorrectPopUP()
    {
        if (bEnableButtons)
        {
            bEnableButtons = false;
            incorrectPopUp.OpenCloseObjectAnimation();
            Invoke("EnableButtons", 2f);
        }
    }

    public void HideCorrectPopUP()
    {
        if (bEnableButtons)
        {
            bEnableButtons = false;
            correctPopUp.OpenCloseObjectAnimation();
            Invoke("EnableButtons", 2f);
        }
    }

    public void HideIncorrectPopUP()
    {
        if (bEnableButtons)
        {
            incorrectPopUp.OpenCloseObjectAnimation();
            Invoke("EnableButtons", 2f);
        }
    }

    public void HideQuestionScreen ()
    {
        if(bEnableButtons)
        {
            bEnableButtons = false;
            questionsScreen.OpenCloseObjectAnimation();
            mainScreen.OpenCloseObjectAnimation();
            Invoke("EnableButtons", 2f);
        }
    }


    private void EnableButtons ()
    {
        bEnableButtons = true;
    }
}
