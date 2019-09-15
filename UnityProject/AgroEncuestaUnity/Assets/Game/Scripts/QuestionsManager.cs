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

    [Header("Popups Feedback")]
    public TextMeshProUGUI txtCorrectFeedback;

    public TextMeshProUGUI txtIncorrectFeedback;

    [SerializeField]
    private CategoryQuestions CurrentSurvey;

    [SerializeField]
    private int currentQuestion = 0;

    public void LoadAnimalSurvey(string Category)
    {
        currentQuestion = 0;
        var AnimalsSurvey = Cuestionarios.FirstOrDefault(s => s.SurveyName == "Animales");
        CurrentSurvey = AnimalsSurvey.Categories.FirstOrDefault(c => c.Category == Category);

        txtCorrectFeedback.text = AnimalsSurvey.CorrectFeedBack;
        txtIncorrectFeedback.text = AnimalsSurvey.wrongFeedback;
        UpdateQuestionUI();

        UIManager.Instance.ShowSurveyScreen();
    }

    public void LoadCultivosSurvey(string Category)
    {
        currentQuestion = 0;
        var cultivosSurvey = Cuestionarios.FirstOrDefault(s => s.SurveyName == "Cultivos");
        CurrentSurvey = cultivosSurvey.Categories.FirstOrDefault(c => c.Category == Category);

        txtCorrectFeedback.text = cultivosSurvey.CorrectFeedBack;
        txtIncorrectFeedback.text = cultivosSurvey.wrongFeedback;
        UpdateQuestionUI();

        UIManager.Instance.ShowSurveyScreen();
    }

    private void UpdateQuestionUI()
    {
        txtInitial.text = CurrentSurvey.CategoryInitial;
        txtTitle.text = CurrentSurvey.Category;
        txtQuestion.text = CurrentSurvey.questions[currentQuestion].question;
        txtAnswerA.text = CurrentSurvey.questions[currentQuestion].answerA;
        txtAnswerB.text = CurrentSurvey.questions[currentQuestion].answerB;
        txtAnswerC.text = CurrentSurvey.questions[currentQuestion].answerC;

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
            UIManager.Instance.ShowIncorrectPopUP();
        }
    }

    private void CorrectFeedback()
    {
        UIManager.Instance.ShowCorrectPopUP();
    }

    private void IncorrectFeedback()
    {
        UIManager.Instance.ShowIncorrectPopUP();
    }

    public void NextQuestion()
    {
        UIManager.Instance.HideCorrectPopUP();

        if (currentQuestion < CurrentSurvey.questions.Length -1 )
        {
            currentQuestion++;
            UpdateQuestionUI();
        }else
        {
            currentQuestion = 0;
            UIManager.Instance.HideQuestionScreen();
        }
    }

    public void CancelGameFromIncorrect ()
    {
        currentQuestion = 0;
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