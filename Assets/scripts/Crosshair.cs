using UnityEngine;
using UnityEngine.UI;

public class CrosshairDirection : MonoBehaviour
{
    public Image left, right, up, down; // Assign in Inspector (or find in Start)
    public Color activeColor = Color.red;
    public Color defaultColor = Color.white;

    void Start()
    {
        // Auto-assign images if not manually assigned
        if (left == null) left = GameObject.Find("Left").GetComponent<Image>();
        if (right == null) right = GameObject.Find("Right").GetComponent<Image>();
        if (up == null) up = GameObject.Find("Up").GetComponent<Image>();
        if (down == null) down = GameObject.Find("Down").GetComponent<Image>();

        // Check if any are still missing
        if (left == null || right == null || up == null || down == null)
        {
            Debug.LogError("One or more crosshair UI elements are missing! Check their names and hierarchy.");
        }
    }

    void Update()
    {
        if (left == null || right == null || up == null || down == null) return; // Prevent errors

        DirectionCheck();
    }

    void DirectionCheck()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Reset all colors first
        left.color = defaultColor;
        right.color = defaultColor;
        up.color = defaultColor;
        down.color = defaultColor;

        if (mouseX < 0 && Mathf.Abs(mouseY) < 0.15f)
            left.color = activeColor;
        else if (mouseX > 0 && Mathf.Abs(mouseY) < 0.15f)
            right.color = activeColor;
        else if (mouseY > 0 && Mathf.Abs(mouseX) < 0.15f)
            up.color = activeColor;
        else if (mouseY < 0 && Mathf.Abs(mouseX) < 0.15f)
            down.color = activeColor;
    }
}
