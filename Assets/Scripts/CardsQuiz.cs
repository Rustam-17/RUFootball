using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardsQuiz : MonoBehaviour
{
    [SerializeField] private List<Sprite> _images;
    [SerializeField] private List<string> _answers;
    [SerializeField] private List<Button> _answerButtons;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private TMP_Text _counter;
    [SerializeField] private Image _image;
    [SerializeField] private Transform _resultsScreen;
    [SerializeField] private TMP_Text _results;
    [SerializeField] private Sprite _buttonImage;
    [SerializeField] private Sprite _buttonPressedImage;

    private List<int> _questionsOrder;
    private int _count;
    private int _currentCount;
    private int _score;
    private int _neverClickedAnswerButtonIndex;
    private int _currentClickedAnswerButtonIndex;
    private int _lastClickedAnswerButtonIndex;
    private int _rightAnswerIndex;
    private bool _isAnswered;

    private void OnEnable()
    {
        for (int i = 0; i < _answerButtons.Count; i++)
        {
            int index = i;

            _answerButtons[i].onClick.AddListener(() => OnAnswerButtonClick(index));
        }

        _acceptButton.onClick.AddListener(OnAcceptButtonClick);
    }

    private void OnDisable()
    {
        for (int i = 0; i < _answerButtons.Count; i++)
        {
            _answerButtons[i].onClick.RemoveListener(() => OnAnswerButtonClick(i));
        }

        _acceptButton.onClick.RemoveListener(OnAcceptButtonClick);
    }

    private void Start()
    {
        _neverClickedAnswerButtonIndex = -1;
        _lastClickedAnswerButtonIndex = _neverClickedAnswerButtonIndex;
        _count = _images.Count;
        _currentCount = 1;
        _questionsOrder = new List<int>();

        int number = 0;

        for (int i = 0; i < _count; i++)
        {
            _questionsOrder.Add(number++);
        }

        Shuffle(_questionsOrder);
        CreateNewQuestion();
    }

    private void CreateNewQuestion()
    {
        int questionOrder = _questionsOrder[_currentCount - 1];
        Debug.Log(questionOrder);
        _counter.text = $"{_currentCount}/{_count}";

        _image.sprite = _images[questionOrder];

        SetAnswerButtons(questionOrder);

        _currentCount++;
    }

    private void Shuffle(List<int> numbers)
    {
        for (int i = numbers.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = numbers[i];

            numbers[i] = numbers[j];
            numbers[j] = temp;
        }
    }

    private void SetAnswerButtons(int questionOrder)
    {
        List<int> indices = new List<int>();

        for (int i = 0; i < _answers.Count; i++)
        {
            indices.Add(i);
        }

        Shuffle(indices);

        _rightAnswerIndex = Random.Range(0, _answerButtons.Count);
        _answerButtons[_rightAnswerIndex].GetComponentInChildren<TMP_Text>().text = _answers[questionOrder];

        indices.Remove(questionOrder);

        int currentAnswerIndex = 0;

        for (int i = 0; i < _answerButtons.Count; i++)
        {
            if (i == _rightAnswerIndex)
                continue;

            _answerButtons[i].GetComponentInChildren<TMP_Text>().text = _answers[indices[currentAnswerIndex]];
            currentAnswerIndex++;
        }
    }

    private void OnAnswerButtonClick(int buttonIndex)
    {
        _currentClickedAnswerButtonIndex = buttonIndex;

        _answerButtons[buttonIndex].GetComponent<Image>().sprite = _buttonPressedImage;

        if (_lastClickedAnswerButtonIndex != _neverClickedAnswerButtonIndex)
        {
            _answerButtons[_lastClickedAnswerButtonIndex].GetComponent<Image>().sprite = _buttonImage;
        }

        _lastClickedAnswerButtonIndex = buttonIndex;

        _isAnswered = true;
    }

    private void OnAcceptButtonClick()
    {
        if (_isAnswered)
        {
            if (_currentClickedAnswerButtonIndex == _rightAnswerIndex)
            {
                _score++;
            }

            if (_currentCount > _count)
            {
                FinishQuiz();
            }
            else
            {
                CreateNewQuestion();
            }
        }

    }

    private void FinishQuiz()
    {
        _resultsScreen.gameObject.SetActive(true);
        _results.text = $"{_score}/{_count}";

        gameObject.SetActive(false);
    }
}
