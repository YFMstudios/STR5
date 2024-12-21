using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardCanvas : MonoBehaviour
{
    Transform cameraTransform; // Kameranýn transform bilgisi

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform; // Sahnedeki ana kameranýn transformunu al
    }

    // Update is called once per frame
    void Update()
    {
        // Canvas'in her zaman kameraya doðru bakmasýný saðlar
        // transform.LookAt ile Canvas'in z ekseni kameraya doðru yönlendirilir
        // cameraTransform.rotation * -Vector3.forward, kameranýn bakýþ yönünün tersi yönde bir vektör oluþturur
        // cameraTransform.rotation * Vector3.up, kameranýn yukarý doðru bakýþ yönünü belirler
        transform.LookAt(transform.position + cameraTransform.rotation * -Vector3.forward, cameraTransform.rotation * Vector3.up);
    }
}
