using Unity.Cinemachine;
using UnityEngine;
/* Class for overall customization
 managing to allow for use through different scenes */
public class CustomizationManager : MonoBehaviour
{
    // Creating needed instance/storing vars
    public static CustomizationManager instance;
    public GameObject malePrefab;
    public GameObject femalePrefab;
    public int gender;
    public Color skinColor;
    public Color hairColor;
    public Color clothingColor;
    private GameObject activePlayer;
    public CinemachineCamera camera;
    private GameObject prefab;

    public GameObject Player
    {
        get { return activePlayer; }
        set { activePlayer = value; }
    }
    
    // Ensureing player character does not get destroyed when loading new scene
    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    
    // Method saves character customization data 
    public void SaveCustomizationData(int gender, Color skin, Color hair, Color clothing)
    {
        this.gender = gender;
        this.skinColor = skin;
        this.hairColor = hair;
        
        this.clothingColor = clothing;
        
        if (activePlayer != null)
        {
            Destroy(activePlayer);
        }
        
        if (gender == 0)
        {
            prefab = malePrefab;
        }
        else
        {
            prefab = femalePrefab;
        }
        
        
    }
    
    // Method for applying stored customizations to prefab
    void ApplyCustomizations(GameObject player, Color skinColor, Color hairColor, Color clothingColor)
    {
        Renderer[] playerRenderers = player.GetComponentsInChildren<Renderer>();

        foreach (Renderer playerRenderer in playerRenderers)
        { 
            playerRenderer.material.SetColor("_SKINCOLOR", skinColor);
            playerRenderer.material.SetColor("_HAIRCOLOR", hairColor);
            playerRenderer.material.SetColor("_CLOTH3COLOR", clothingColor);
        }
    }

    public void CreatePlayer()
    {
        Player = Instantiate(prefab);
        ApplyCustomizations(Player, skinColor, hairColor, clothingColor);
        Player.transform.SetParent(transform);
        Player.SetActive(true);
        camera.Follow = Player.transform;
    }
}
