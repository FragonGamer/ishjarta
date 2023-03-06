using UnityEngine;
using UnityEngine.UI;

public class PinUIElement : MonoBehaviour
{
    [SerializeField]
    public RectTransform uiElement;
    [SerializeField]
    public Transform targetObject;
    [SerializeField]
    float upLength;
    

    void Update()
    {

        // Get the position of the target object in world space
        Vector3 targetPosition = targetObject.position + new Vector3(0,upLength,0);

        // Convert the target position to screen space
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);

        // Set the position of the UI element to the screen position
        uiElement.position = screenPosition;
    }
}