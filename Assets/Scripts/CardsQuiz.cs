using System.Collections;
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

    private List<int> _questionsOrder;
    private TMP_Text _answerButtonText;
    private int _count;
    private int _currentCount;
    private bool _isAnswered;

    private void OnEnable()
    {
        foreach(Button button in _answerButtons)
        {
            button.onClick.AddListeners();
        }
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        _questionsOrder = new List<int>();

        int number = 1;

        for (int i = 0; i < _count; i++)
        {
            _questionsOrder.Add(number++);
        }

        _count = _images.Count;
        _currentCount = 1;

        Shuffle(_questionsOrder);
    }

    private void CreateQuestion()
    {
        int questionOrder = _questionsOrder[_currentCount - 1];

        _counter.text = $"{_currentCount}/{_count}";

        _image.sprite = _images[questionOrder];

        SetAnswerButtons(questionOrder);

    }

    private void Shuffle(List<int> numbers)
    {
        for (int i = _count - 1; i > 0; i--)
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

        int randomButtonIndex = Random.Range(0, _answerButtons.Count);
        _answerButtons[randomButtonIndex].GetComponentInChildren<Text>().text = _answers[questionOrder];

        indices.Remove(questionOrder);

        int currentAnswerIndex = 0;
        for (int i = 0; i < _answerButtons.Count; i++)
        {
            if (i == randomButtonIndex)
                continue;

            _answerButtons[i].GetComponentInChildren<Text>().text = _answers[indices[currentAnswerIndex]];
            currentAnswerIndex++;
        }
    }
}
