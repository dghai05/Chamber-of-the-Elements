using UnityEngine;
using UnityEngine.UI;
/* This class controls the customization
 * of character hair colour
 */
public class HairCustomization : MonoBehaviour
{
    // Reference to prefab material and buttons
    public Material characterMaterial;
    public Button hairButton1, hairButton2, hairButton3, hairButton4;

    // Calling change colour method when button is clicked (based on assigned hexadecimal code) 
    void Start()
    {
        ChangeColor("#8A250E");
        hairButton1.onClick.AddListener(() => ChangeColor("#8A250E")); // Red
        hairButton2.onClick.AddListener(() => ChangeColor("#FBDD83")); // Blond
        hairButton3.onClick.AddListener(() => ChangeColor("#382417")); // Brown
        hairButton4.onClick.AddListener(() => ChangeColor("#0000")); // Black
    }

    // Method for changing character hair colour 
    void ChangeColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color newColor))
        {
            characterMaterial.SetColor("_HAIRCOLOR", newColor); // Changing hair customization
            CustomizationManager.instance.hairColor = newColor; // Storing customization

            CustomizationManager.instance.SaveCustomizationData(
                GenderManager.GetGender(),
                CustomizationManager.instance.skinColor,
                newColor,
                CustomizationManager.instance.clothingColor
            ); // Storing customization
        }
    }
}