using UnityEngine;
//This code is used to rotate the character within the character customization scene
public class RotateCharacter : MonoBehaviour
{
    // Creating speed reference which can be changed
    public float rotationSpeed = 100f;

    // Update is called once per frame, rotates player's character
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
