using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ExplosionCrushBox : MonoBehaviour
{
    public List<GameObject> objects; // Список объектов, которые будут анимироваться
    public Transform target; // Цель, от которой объекты будут разлетаться
    public float scatterDistance = 5f; // Расстояние, на которое объекты будут разбросаны
    public float duration = 0.5f; // Продолжительность анимации
    public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f); // Минимальный размер частиц
    public Vector3 maxScale = new Vector3(1.5f, 1.5f, 1.5f); // Максимальный размер частиц
    public float spreadAngle = 120f; // Угол разлета частиц
    public float maxDelay = 0.2f; // Максимальная случайная задержка перед началом анимации
    public float rotationSpeed = 360f; // Скорость вращения объектов

    void Start()
    {
        ScatterObjects();
    }

    void ScatterObjects()
    {
        foreach (GameObject obj in objects)
        {
            // Направление от цели к объекту
            Vector3 direction = (obj.transform.position - target.position).normalized;

            // Случайный угол внутри угла разлета
            float angle = Random.Range(-spreadAngle / 2, spreadAngle / 2);

            // Поворот направления на случайный угол
            Vector3 randomDirection = Quaternion.Euler(0, angle, 0) * direction;

            // Рассчитываем конечную позицию с Y = 0
            Vector3 targetPosition = obj.transform.position + randomDirection * scatterDistance;
            targetPosition.y = 0;

            // Выбираем случайный размер
            Vector3 randomScale = new Vector3(
                Random.Range(minScale.x, maxScale.x),
                Random.Range(minScale.y, maxScale.y),
                Random.Range(minScale.z, maxScale.z)
            );

            // Выбираем случайную задержку
            float randomDelay = Random.Range(0, maxDelay);

            // Создаем последовательность анимаций для объекта
            Sequence sequence = DOTween.Sequence();

            // Анимация движения и изменения размера
            sequence.Append(obj.transform.DOMove(targetPosition, duration).SetEase(Ease.InOutBack).SetDelay(randomDelay));
            sequence.Join(obj.transform.DOScale(randomScale, duration).SetEase(Ease.InOutBack).SetDelay(randomDelay));

            // Анимация вращения
            sequence.Join(obj.transform.DORotate(new Vector3(0, Random.Range(0, 360), 0), duration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutBack).SetDelay(randomDelay));

            // Остановка вращения после завершения полета
            sequence.AppendCallback(() => obj.transform.DOKill());
        }
    }
}
