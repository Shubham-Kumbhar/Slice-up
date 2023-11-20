using UnityEngine;

public class RotateConsecutively : MonoBehaviour
{
    public float rotationSpeed = 5f; // Adjust the rotation speed as needed
    public GameObject[] piecesToRotate; // Assign your environmental pieces to this array

    private int currentIndex = 0;

    void Update()
    {
        // Check if there are pieces to rotate
        if (piecesToRotate.Length == 0)
        {
            Debug.LogError("No pieces to rotate. Please assign pieces to the 'piecesToRotate' array.");
            return;
        }

        RotatePiece();
    }

    void RotatePiece()
    {
        // Rotate the current piece
        piecesToRotate[currentIndex].transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);

        // Check if the rotation is complete (you can adjust the rotation threshold as needed)
        if (piecesToRotate[currentIndex].transform.rotation.eulerAngles.z >= 180f)
        {
            // Move to the next piece
            currentIndex = (currentIndex + 1) % piecesToRotate.Length;

            // Optionally, you can add a delay before rotating the next piece
            // StartCoroutine(DelayBeforeRotation());
        }
    }

    // Optionally, you can add a delay before rotating the next piece
    /*IEnumerator DelayBeforeRotation()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay time as needed
        RotatePiece();
    }*/
}
