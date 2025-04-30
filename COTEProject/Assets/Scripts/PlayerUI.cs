using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace DefaultNamespace
{
    public class PlayerUI:MonoBehaviour
    {
        private readonly string[] scenesToHideUI = { "CharacterCustomization" };
        
        public Slider healthSlider;
        public TextMeshProUGUI coinText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI shieldPotionCount;
        public TextMeshProUGUI healthPotionCount;
        public TextMeshProUGUI speedPotionCount;
        public GameObject armorPanel;
        
        [Header("Shield UI")]
        [SerializeField] private Slider shieldSlider;
        [SerializeField] private GameObject shieldUIHolder;
        [SerializeField] private TextMeshProUGUI shieldStatusText;
        
        [Header("Shield Status")]
        [SerializeField] private TextMeshProUGUI shieldText;

        public void UpdateShieldStatus(ShieldState state, bool isUnlocked)
        {
            // Only update if shield is unlocked
            if (!isUnlocked)
            {
                shieldText.gameObject.SetActive(false);
                return;
            }
            // Show the text and update based on state
            shieldText.gameObject.SetActive(true);
            switch(state)
            {
                case ShieldState.Ready:
                    shieldText.text = "SHIELD READY";
                    shieldText.color = Color.green;
                    break;
                case ShieldState.Active:
                    shieldText.text = "SHIELD ACTIVATED";
                    shieldText.color = Color.blue;
                    break;
                case ShieldState.Cooldown:
                    shieldText.text = "SHIELD NOT READY";
                    shieldText.color = Color.red;
                    break;
            }
        }
        public enum ShieldState { Ready, Active, Cooldown }
        public void SetHealth(float currentHealth, float maxHealth)
        {
            healthSlider.value = currentHealth / maxHealth;
            healthText.text = $"HP: {currentHealth}/{maxHealth}";
        }
        
        public void SetCoins(int coins)
        {
            coinText.text = $"Coins: {coins}";
        }

        public void SetShieldPotions(int shieldPotions)
        {
            shieldPotionCount.text = shieldPotions.ToString();
        }

        public void SetHealthPotions(int healthPotions)
        {
            healthPotionCount.text = healthPotions.ToString();
        }

        public void SetSpeedPotions(int speedPotions)
        {
            speedPotionCount.text = speedPotions.ToString();
        }
        void Awake()
        {
            if (Player.User != null)
            {
                Player.User.PlayerUI = this;
                Player.User.UpdateUI();
            }
        }
        
        void Start()
        {
            HandleSceneVisibility(SceneManager.GetActiveScene().name);
            SceneManager.sceneLoaded += OnSceneLoaded;
            shieldUIHolder.SetActive(false);
            shieldText.gameObject.SetActive(false);
        }



        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            HandleSceneVisibility(scene.name);
        }

        private void HandleSceneVisibility(string sceneName)
        {
            bool shouldBeVisible = !System.Array.Exists(scenesToHideUI, s => s == sceneName);
            gameObject.SetActive(shouldBeVisible);
        }
        public void CheckArmorVisibility()
        {
            if (armorPanel != null)
            {
                armorPanel.SetActive(Player.User != null && Player.User.HasArmor);
            }
        }
        public void ShowShieldUI(float duration)
        {
           Debug.Log($"ShowShieldUI called. ShieldPanel active: {shieldUIHolder.activeSelf}");
          
           shieldUIHolder.SetActive(true); 
           shieldSlider.maxValue = duration;
           shieldSlider.value = duration;
           shieldStatusText.text = "SHIELD ACTIVE";
          
           // Force Canvas update
           Canvas.ForceUpdateCanvases();
           Debug.Log($"Slider value set to: {shieldSlider.value}/{shieldSlider.maxValue}");
        }
        public void HideShieldUI()
        {
           shieldUIHolder.SetActive(false);
        }
        public void UpdateShieldUI(float remainingTime)
        {
           shieldSlider.value = remainingTime;
           shieldStatusText.text = $"SHIELD: {remainingTime:F1}s";
        }

    }
}