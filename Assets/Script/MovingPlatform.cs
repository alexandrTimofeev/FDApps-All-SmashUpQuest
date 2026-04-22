using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Точки между которыми будет двигаться платформа
    public Transform pointA;
    public Transform pointB;

    // Время в секундах, за которое платформа будет перемещаться от одной точки к другой
    public float travelTime = 2.0f;

    // Скорость вращения вокруг оси Y (в градусах в секунду)
    public float rotationSpeed = 90f;

    // Включение/выключение движения платформы
    public bool movePlatform = true;

    // Включение/выключение вращения платформы
    public bool rotatePlatform = true;

    private void Start()
    {
        // Запуск корутины для движения платформы
        if (movePlatform)
        {
            StartCoroutine(MovePlatform());
        }

        // Запуск корутины для вращения платформы
        if (rotatePlatform)
        {
            StartCoroutine(RotatePlatform());
        }
    }

    private IEnumerator MovePlatform()
    {
        while (movePlatform)
        {
            // Перемещение от точки A к точке B
            yield return StartCoroutine(MoveToPoint(pointA.position, pointB.position));

            // Перемещение от точки B обратно к точке A
            yield return StartCoroutine(MoveToPoint(pointB.position, pointA.position));
        }
    }

    private IEnumerator MoveToPoint(Vector3 startPoint, Vector3 endPoint)
    {
        float elapsedTime = 0f;

        while (elapsedTime < travelTime)
        {
            // Интерполяция позиции платформы
            transform.position = Vector3.Lerp(startPoint, endPoint, elapsedTime / travelTime);
            elapsedTime += Time.deltaTime;

            // Ожидание следующего кадра
            yield return null;
        }

        // Убедитесь, что платформа точно установлена в конечную точку
        transform.position = endPoint;
    }

    private IEnumerator RotatePlatform()
    {
        while (rotatePlatform)
        {
            // Вращение платформы вокруг оси Y
            float rotationY = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationY, 0);

            // Ожидание следующего кадра
            yield return null;
        }
    }
}

