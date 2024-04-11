using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject objectToMove;
    private Camera mainCamera;
    private bool isLocked = false;
    

    void Start()
    {
        mainCamera = Camera.main;

        // Create a new game object under the mouse cursor
        objectToMove = Instantiate(objectToMove);
        UpdateObjectPosition();
    }

    void Update()
    {
        // If not locked, update the object's position to follow the mouse cursor
        if (!isLocked)
        {
            UpdateObjectPosition();
        }

        // Check for mouse click to lock/unlock the object
        if (Input.GetMouseButtonDown(0))
        {
            isLocked = !isLocked;
        }

        // If locked, keep the object's position fixed at the mouse cursor position
        if (isLocked)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Distance from the camera to the object
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.y = 10; // Force the Y position to be 10 while locked
            objectToMove.transform.position = worldPosition;
        }
    }

    void UpdateObjectPosition()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10; // Distance from the camera to the object

        // Convert the mouse position from screen space to world space
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Update the object's position
        objectToMove.transform.position = worldPosition;
    }
}