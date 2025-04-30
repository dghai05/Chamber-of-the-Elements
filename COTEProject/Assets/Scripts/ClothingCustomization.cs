using UnityEngine;
using UnityEngine.UI; 
/* This class controls the customization
 * of character clothing colour
 */
public class ClothingCustomization : MonoBehaviour
{
    // Reference to prefab material and buttons
    public Material characterMaterial;
    public Button clothingButton1, clothingButton2, clothingButton3, clothingButton4;

    // Calling change colour method when button is clicked (based on assigned hexadecimal code) 
    void Start()
    {
        ChangeColor("#21468C");
        clothingButton1.onClick.AddListener(() => ChangeColor("#21468C")); // Blue
        clothingButton2.onClick.AddListener(() => ChangeColor("#6D0B0B")); // Red
        clothingButton3.onClick.AddListener(() => ChangeColor("#1E7B4F")); // Green
        clothingButton4.onClick.AddListener(() => ChangeColor("#7E1C66")); // Pink
    }

    // Method for changing character clothing colour
    void ChangeColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color newColor))
        {
            characterMaterial.SetColor("_CLOTH3COLOR", newColor); // Changing clothing customization
            CustomizationManager.instance.clothingColor = newColor; // Storing customization

            CustomizationManager.instance.SaveCustomizationData(
                GenderManager.GetGender(), 
                CustomizationManager.instance.skinColor, 
                CustomizationManager.instance.hairColor, 
                newColor 
            ); // Storing customization
        }
    }
}
