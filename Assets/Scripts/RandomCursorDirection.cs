using UnityEngine;

public class RandomCursorDiretion : MonoBehaviour
{
    private Vector2 lastMousePosition;
    private Vector2 currentMousePosition;
    private Vector2 cursorVelocity;

    private float reverseChance = 1f; // Chance to reverse cursor movement

    private void Update()
    {
        // Get the current mouse position in screen space
        currentMousePosition = Input.mousePosition;

        // Calculate the direction vector of the movement (delta position)
        Vector2 movementDirection = currentMousePosition - lastMousePosition;

        // Check for random chance to reverse the movement direction
        if (Random.value < reverseChance)
        {
            movementDirection = -movementDirection; // Reverse the movement direction
            Debug.Log("Reversed cursor movement");
        }

        // Now, apply the reversed or original movement to the cursor's position
        Vector2 newCursorPosition = (Vector2)transform.position + movementDirection;

        // Apply the new cursor position to the heart outline's position
        transform.position = newCursorPosition;

        // Update the last position for the next frame
        lastMousePosition = currentMousePosition;
    }
}