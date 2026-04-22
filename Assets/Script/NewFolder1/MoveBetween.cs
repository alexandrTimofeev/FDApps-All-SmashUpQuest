using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MoveBetween : MonoBehaviour
{
    [SerializeField] private Vector3 offsetA;
    [SerializeField] private Vector3 offsetB;
    [SerializeField] private float moveDuration = 1f;       // длительность одного перемещения
    [SerializeField] private Ease ease = Ease.InOutQuad;   // кривая анимации
    [SerializeField] private bool loop = true;              // бесконечное повторение

    private Vector3 startPos;
    private Sequence _sequence;

    void Start()
    {
        startPos = transform.position;
        Vector3 p1 = startPos + offsetA;
        Vector3 p2 = startPos + offsetB;

        _sequence = DOTween.Sequence();

        // Добавляем движение в p1, затем в p2
        _sequence.Append(transform.DOMove(p1, moveDuration).SetEase(ease));
        _sequence.Append(transform.DOMove(p2, moveDuration).SetEase(ease));

        if (loop)
        {
            // Зацикливаем с чередованием направления (p1->p2->p1->p2...)
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }
    }

    void OnDestroy()
    {
        // Останавливаем анимацию при уничтожении объекта, чтобы избежать ошибок
        _sequence?.Kill();
    }
}