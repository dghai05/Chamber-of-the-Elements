using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 2.3f , 0); // Adjust height above head
    [SerializeField] private Image healthBar;
    
    private void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            //transform.forward = Camera.main.transform.forward; // Face the camera
        }
    }

    public void SetHealth(float current, float max)
    {
        Debug.Log($"SetHealth called: {current}/{max}");

        if (slider != null)
        {
            slider.value = current / max;
        }
        else
        {
            Debug.LogWarning("Slider is null on health bar!");
        }
    }

    
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}