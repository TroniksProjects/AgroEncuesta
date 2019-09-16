using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsManager : MonoBehaviour
{
    public QuestionaryClass[] Cuestionarios;

    [Header("UI Elements")]
    public Image questionPic;

    public TextMeshProUGUI txtInitial;
    public TextMeshProUGUI txtQuestion;
    public TextMeshProUGUI txtTitle;
    public TextMeshProUGUI txtAnswerA;
    public TextMeshProUGUI txtAnswerB;
    public TextMeshProUGUI txtAnswerC;

    private Button OptionA;
    private Button OptionB;
    private Button OptionC;

    public ParticleSystem Confeti;

    [Header("Popups Feedback")]
    public TextMeshProUGUI txtCorrectFeedback;

    public TextMeshProUGUI txtIncorrectFeedback;

    [SerializeField]
    private CategoryQuestions CurrentSurvey;

    [SerializeField]
    private int currentQuestion = 0;

    [SerializeField]
    private int playerAttempts = 1;

    private void Start()
    {
        Confeti.Stop();
        OptionA = txtAnswerA.transform.GetComponentsInParent<Button>(true)[0];
        OptionB = txtAnswerB.transform.GetComponentsInParent<Button>(true)[0];
        OptionC = txtAnswerC.transform.GetComponentsInParent<Button>(true)[0];
    }

    public void LoadAnimalSurvey(string Category)
    {
        UIManager.Instance.EnableBlockPanel();
        currentQuestion = 0;
        playerAttempts = 1;
        var AnimalsSurvey = Cuestionarios.FirstOrDefault(s => s.SurveyName == "Animales");
        CurrentSurvey = AnimalsSurvey.Categories.FirstOrDefault(c => c.Category == Category);

        txtCorrectFeedback.text = AnimalsSurvey.CorrectFeedBack;
        txtIncorrectFeedback.text = AnimalsSurvey.wrongFeedback;

        Randomizer.Randomize(CurrentSurvey.questions);
        UpdateQuestionUI();

        UIManager.Instance.ShowSurveyScreen();
    }

    public void LoadCultivosSurvey(string Category)
    {
        currentQuestion = 0;
        playerAttempts = 1;
        var cultivosSurvey = Cuestionarios.FirstOrDefault(s => s.SurveyName == "Cultivos");
        CurrentSurvey = cultivosSurvey.Categories.FirstOrDefault(c => c.Category == Category);

        txtCorrectFeedback.text = cultivosSurvey.CorrectFeedBack;
        txtIncorrectFeedback.text = cultivosSurvey.wrongFeedback;
        Randomizer.Randomize(CurrentSurvey.questions);
        UpdateQuestionUI();

        UIManager.Instance.ShowSurveyScreen();
    }

    private void UpdateQuestionUI()
    {
        OptionA.onClick.RemoveAllListeners();
        OptionB.onClick.RemoveAllListeners();
        OptionC.onClick.RemoveAllListeners();

        string[] Answers = new string[3];

        Answers[0] = (CurrentSurvey.questions[currentQuestion].answerA) + "|A";
        Answers[1] = (CurrentSurvey.questions[currentQuestion].answerB) + "|B";
        Answers[2] = (CurrentSurvey.questions[currentQuestion].answerC) + "|C";

        Randomizer.Randomize(Answers);

        for (int i = 0; i < Answers.Length; i++)
        {
            string[] AnswerRandom = Answers[i].Split('|');
            int AnswerIndex = (AnswerRandom[1] == "A") ? 1 : ((AnswerRandom[1] == "B") ? 2 : 3);

            switch (i)
            {
                case 0:
                    OptionA.onClick.AddListener(() => { SelectAnswer(AnswerIndex); });
                    txtAnswerA.text = AnswerRandom[0];
                    break;

                case 1:
                    OptionB.onClick.AddListener(() => { SelectAnswer(AnswerIndex); });
                    txtAnswerB.text = AnswerRandom[0];
                    break;

                case 2:
                    OptionC.onClick.AddListener(() => { SelectAnswer(AnswerIndex); });
                    txtAnswerC.text = AnswerRandom[0];
                    break;
            }
        }

        txtInitial.text = CurrentSurvey.CategoryInitial;
        txtTitle.text = CurrentSurvey.Category;

        txtQuestion.text = CurrentSurvey.questions[currentQuestion].question;

        if (CurrentSurvey.questions[currentQuestion].QuestionSprite)
        {
            questionPic.sprite = CurrentSurvey.questions[currentQuestion].QuestionSprite;
        }
    }

    /// <summary>
    /// The answer index start in 1 and finish in 3, 1= a, 2= b, 3 = c
    /// </summary>
    /// <param name="answerIndex"></param>
    public void SelectAnswer(int answerIndex)
    {
        Debug.LogError(answerIndex);
        AudioManager.Instance.PlayUIButton();
        UIManager.Instance.EnableBlockPanel();

        if (CurrentSurvey.questions[currentQuestion].correctAnswer == answerIndex)
        {
            if (CurrentSurvey.questions[currentQuestion].QuestionSprite)
            {
                questionPic.sprite = CurrentSurvey.questions[currentQuestion].CorrectSprite;
            }

            Invoke("CorrectFeedback", 2);
        }
        else
        {
            if (playerAttempts > 0)
            {
                playerAttempts--;
                UIManager.Instance.ShowIncorrectPopUP();
            }
            else
            {
                AudioManager.Instance.StopGameMusic();
                UIManager.Instance.HideRetryButton();
                IncorrectFeedback();
            }
        }
    }

    private void CorrectFeedback()
    {
        Confeti.Play();
        UIManager.Instance.ShowCorrectPopUP();
    }

    private void IncorrectFeedback()
    {
        AudioManager.Instance.PlayLoseMusic();
        UIManager.Instance.ShowIncorrectPopUP();
    }

    public void NextQuestion()
    {
        UIManager.Instance.HideCorrectPopUP();
        Confeti.Stop();
        if (currentQuestion < CurrentSurvey.questions.Length - 1)
        {
            currentQuestion++;
            UpdateQuestionUI();
        }
        else
        {
            currentQuestion = 0;
            playerAttempts = 1;

            AudioManager.Instance.StopGameMusic();
            AudioManager.Instance.PlayWinMusic();

            UIManager.Instance.HideQuestionScreen();
        }
    }

    public void CancelGameFromIncorrect()
    {
        currentQuestion = 0;
        AudioManager.Instance.StopLoseMusic();
        AudioManager.Instance.PlayLobbyMusic();
        UIManager.Instance.HideIncorrectPopUP();
        UIManager.Instance.HideQuestionScreen();
    }
}

[System.Serializable]
public struct QuestionaryClass
{
    public string SurveyName;
    public CategoryQuestions[] Categories;

    [TextArea]
    public string CorrectFeedBack;

    [TextArea]
    public string wrongFeedback;
}

[System.Serializable]
public struct CategoryQuestions
{
    public string Category;
    public string CategoryInitial;
    public Question[] questions;
}

[System.Serializable]
public struct Question
{
    [TextArea]
    public string question;

    public string answerA;
    public string answerB;
    public string answerC;
    public Sprite QuestionSprite;
    public Sprite CorrectSprite;
    public int correctAnswer;
}

public class Randomizer
{
    public static void Randomize<T>(T[] items)
    {
        System.Random rand = new System.Random();

        // For each spot in the array, pick
        // a random item to swap into that spot.
        for (int i = 0; i < items.Length - 1; i++)
        {
            int j = rand.Next(i, items.Length);
            T temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
    }
}