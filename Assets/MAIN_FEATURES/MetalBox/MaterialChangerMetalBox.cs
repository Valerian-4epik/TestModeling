using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MaterialChangerMetalBox : MonoBehaviour
{
    public Material newMaterial; // Новый материал, на который будем менять
    public float changeDuration = 2f; // Время, на которое материал будет изменен
    public MeshRenderer meshRenderer;

    private Material originalMaterial; // Исходный материал

    void Start()
    {
        if (meshRenderer != null)
        {
            originalMaterial = meshRenderer.material; // Сохраняем исходный материал
        }
        
        ChangeMaterialTemporarily();
    }

    public void ChangeMaterialTemporarily()
    {
        if (meshRenderer != null && newMaterial != null)
        {
            meshRenderer.material = newMaterial; // Меняем материал на новый
            DOVirtual.DelayedCall(changeDuration, () => 
            {
                meshRenderer.material = originalMaterial; // Возвращаем исходный материал через changeDuration секунд
            });
        }
    }
}
