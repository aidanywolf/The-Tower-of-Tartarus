using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3; // Set this to the maximum number of hearts.
    [SerializeField] public int currentHealth; // Player's current health.

    [SerializeField] Image heartPrefab;
    [SerializeField] Image deadHeartPrefab;
    [SerializeField] Transform heartsParent; // The parent object where hearts will be instantiated.
    [SerializeField] float heartSpacing = 10f; // Adjust this value for the spacing between hearts.

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        // Remove all existing hearts
        foreach (Transform child in heartsParent)
        {
            Destroy(child.gameObject);
        }

        // Calculate total width of hearts and spacing
        //float totalWidth = maxHealth * heartPrefab.rectTransform.rect.width + (maxHealth - 1) * heartSpacing;
        float startX = heartsParent.position.x;

        int i = 0;
        // Instantiate hearts based on current health
        for (i = 0; i < currentHealth; i++)
        {
            Image heart = Instantiate(heartPrefab, heartsParent);
            RectTransform rt = heart.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(startX + i * (rt.rect.width + heartSpacing), 0f);
        }
         for(i = i; i < maxHealth; i++){
            Image deadHeart = Instantiate(deadHeartPrefab, heartsParent);
            RectTransform rt = deadHeart.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(startX + i * (rt.rect.width + heartSpacing), 0f);
        }
    }
}