using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField]
    private GameObject boundaryObject;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    Camera cameraComponent;

    // Start is called before the first frame update
    void Start()
    {
        cameraComponent = GetComponent<Camera>();
        playerTransform = GameObject.Find("Player").transform;
        CalculateCameraBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

        float cameraHalfHeight = cameraComponent.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * cameraComponent.aspect;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x + cameraHalfWidth, maxBounds.x - cameraHalfWidth);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y + cameraHalfHeight, maxBounds.y - cameraHalfHeight);

        transform.position = targetPosition;
    }

    void CalculateCameraBounds()
    {
        Bounds bounds = boundaryObject.GetComponent<Renderer>().bounds;

        minBounds = new Vector2(bounds.min.x, bounds.min.y);
        maxBounds = new Vector2(bounds.max.x, bounds.max.y);
    }
}
