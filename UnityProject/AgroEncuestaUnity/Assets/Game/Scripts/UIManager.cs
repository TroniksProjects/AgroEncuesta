using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject blockPanel;

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

    [SerializeField]
    private Button RetryButton;

    private bool bEnableButtons = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainScreen.OpenCloseObjectAnimation();
    }

    public void ShowCategoryMenu()
    {
        EnableBlockPanel();
        AudioManager.Instance.PlayUIButton();
        bEnableButtons = false;
        mainScreen.OpenCloseObjectAnimation();
        categoryMenu.OpenCloseObjectAnimation();
        Invoke("EnableButtons", 2f);
    }

    public void ShowSurveyScreen()
    {
        EnableBlockPanel();
        RetryButton.gameObject.SetActive(true);
        AudioManager.Instance.PlayUIButton();
        AudioManager.Instance.PlayGameMusic();
        bEnableButtons = false;
        categoryMenu.OpenCloseObjectAnimation();
        questionsScreen.OpenCloseObjectAnimation();
        Invoke("EnableButtons", 2f);
    }

    public void ShowCorrectPopUP()
    {

        EnableBlockPanel();
        AudioManager.Instance.PlayUIButton();
        bEnableButtons = false;
        correctPopUp.OpenCloseObjectAnimation();
        Invoke("EnableButtons", 2f);

    }

    public void ShowIncorrectPopUP()
    {
        EnableBlockPanel();
        AudioManager.Instance.PlayUIButton();
        bEnableButtons = false;
        incorrectPopUp.OpenCloseObjectAnimation();
        Invoke("EnableButtons", 2f);

    }

    public void HideCorrectPopUP()
    {
        EnableBlockPanel();
        AudioManager.Instance.PlayUIButton();
        bEnableButtons = false;
        correctPopUp.OpenCloseObjectAnimation();
        Invoke("EnableButtons", 2f);

    }

    public void HideIncorrectPopUP()
    {
        EnableBlockPanel();
        AudioManager.Instance.PlayUIButton();
        incorrectPopUp.OpenCloseObjectAnimation();
        Invoke("EnableButtons", 2f);

    }

    public void HideQuestionScreen()
    {
        EnableBlockPanel();
        AudioManager.Instance.PlayUIButton();
        bEnableButtons = false;
        questionsScreen.OpenCloseObjectAnimation();
        mainScreen.OpenCloseObjectAnimation();
        Invoke("EnableButtons", 2f);

    }

    public void HideRetryButton()

    {
        RetryButton.gameObject.SetActive(false);
    }

    public void EnableBlockPanel()
    {
        blockPanel.gameObject.SetActive(true);
        Invoke("DisableBlockPanel", 2);
    }

    public void DisableBlockPanel ()
    {
        blockPanel.gameObject.SetActive(false);
    }

    private void EnableButtons()
    {
        bEnableButtons = true;
    }
}