using UnityEngine;
using UnityEngine.UI;
/* This class controls the customization
 * of character skin colour 
 */
public class SkinCustomization : MonoBehaviour
{
    // Reference to prefab material and buttons
    public Material characterMaterial;
    public Button skinButton1, skinButton2, skinButton3, skinButton4;

  	// Calling change colour method when button is clicked (based on assigned hexadecimal code) 
    void Start()
    {
        ChangeColor("#F8B374");
        skinButton1.onClick.AddListener(() => ChangeColor("#F8B374"));
        skinButton2.onClick.AddListener(() => ChangeColor("#8C643A"));
        skinButton3.onClick.AddListener(() => ChangeColor("#50362A"));
        skinButton4.onClick.AddListener(() => ChangeColor("#351A0B"));
    }

	 // Method for changing character skin colour 
    void ChangeColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color newColor))
        {
            characterMaterial.SetColor("_SKINCOLOR", newColor); // Changing skin customization
            CustomizationManager.instance.skinColor = newColor; // Storing customization
            CustomizationManager.instance.SaveCustomizationData(
                GenderManager.GetGender(),
                newColor, 
                CustomizationManager.instance.hairColor,
                CustomizationManager.instance.clothingColor
            ); // Storing customization
        }
    }
}
