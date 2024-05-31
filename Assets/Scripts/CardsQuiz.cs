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

    private void Start()
    {
        _questionsOrder = new List<int>();
        _count = _images.Count;
        _currentCount = 1;

        Shuffle();
    }

    private void CreateQuestion()
    {
        int questionOrder = _questionsOrder[_currentCount - 1];

        _counter.text = $"{_currentCount}/{_count}";

        _image.sprite = _images[questionOrder];


    }

    private void Shuffle()
    {
        int number = 1;

        for (int i = 0; i < _count; i++)
        {
            _questionsOrder.Add(number++);
        }

        for (int i = _count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = _questionsOrder[i];

            _questionsOrder[i] = _questionsOrder[j];
            _questionsOrder[j] = temp;
        }
    }

    private void SetAnswerButtons(int questionOrder)
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < _answers.Count; i++)
        {
            indices.Add(i);
        }

        // Перемешиваем индексы
        Shuffle(indices);

        // Назначаем правильный ответ случайной кнопке
        int randomButtonIndex = Random.Range(0, answerButtons.Count);
        answerButtons[randomButtonIndex].GetComponentInChildren<Text>().text = answerOptions[correctAnswerIndex];

        // Удаляем использованный индекс из списка перемешанных индексов
        indices.Remove(correctAnswerIndex);

        // Назначаем оставшиеся ответы остальным кнопкам
        int currentAnswerIndex = 0;
        for (int i = 0; i < answerButtons.Count; i++)
        {
            if (i == randomButtonIndex)
                continue;  // Пропускаем кнопку с правильным ответом

            answerButtons[i].GetComponentInChildren<Text>().text = answerOptions[indices[currentAnswerIndex]];
            currentAnswerIndex++;
        }
    }
}
