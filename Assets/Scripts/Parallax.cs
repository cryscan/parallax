using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    public float smoothing;

    private Transform[] backgrounds;
    private Vector3 cameraInitialPosition;

    void Awake()
    {
        backgrounds = new Transform[transform.childCount];
        foreach (int index in Enumerable.Range(0, transform.childCount))
        {
            var child = transform.GetChild(index);
            backgrounds[index] = child;
        }
    }

    void Start()
    {
        cameraInitialPosition = cameraTransform.position;
    }

    void Update()
    {
        Vector3 cameraPosition = cameraTransform.position;

        foreach (var child in backgrounds)
        {
            float scale = 1 - 1 / child.position.z;
            Vector3 targetPosition = scale * (cameraPosition - cameraInitialPosition);
            targetPosition.z = child.position.z;
            child.position = Vector3.Lerp(child.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}
