using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] int itemCount; // Player's current health.

    [SerializeField] Image[] itemPrefabs;
    [SerializeField] Transform itemsParent; // The parent object where hearts will be instantiated.
    [SerializeField] float itemSpacing = 10f; // Adjust this value for the spacing between hearts.

    public void UpdateItemUI(int itemIndex)
    {
        itemCount ++;
        

        // Calculate total width of hearts and spacing
        //float totalWidth = maxHealth * heartPrefab.rectTransform.rect.width + (maxHealth - 1) * heartSpacing;
        float startX = itemsParent.position.x;

        int i = 0;
        // Instantiate hearts based on current health
        for (i = 0; i < itemCount; i++)
        {
            if(i > itemCount - 2){
                Image item = Instantiate(itemPrefabs[itemIndex], itemsParent);
                RectTransform rt = item.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(startX + i * (rt.rect.width + itemSpacing), 0f);
            }
        }
    }
}
