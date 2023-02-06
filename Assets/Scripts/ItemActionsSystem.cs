using UnityEngine;

public class ItemActionsSystem : MonoBehaviour
{

  [Header("OTHER SCRIPTS REFERENCES")]

    [SerializeField]
    private Equipment equipment;

    [SerializeField]
    private PlayerStats playerStats;
  
  [Header("ITEMS ACTIONS SYSTEM VARIABLES")]

    [SerializeField]
    public GameObject actionPanel;

    [SerializeField]
    private Transform dropPoint;

    [SerializeField]
    private GameObject useItemButton;

    [SerializeField]
    private GameObject equipItemButton;

    [SerializeField]
    private GameObject dropItemButton;

    [SerializeField]
    private GameObject destroyItemButton;

    [HideInInspector]
    public ItemData itemCurrentlySelected;

  

    public void OpenActionPanel(ItemData item)
    {
        itemCurrentlySelected = item;

        if(item == null)
        {
            actionPanel.SetActive(false);
            return;
        }

        switch(item.itemType)
        {
            case ItemType.Ressource:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(false);
                break;
            case ItemType.Equipment:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(true);
                break;
            case ItemType.Consumable:
                useItemButton.SetActive(true);
                equipItemButton.SetActive(false);
                break;
        }

       
        actionPanel.SetActive(true);
    }

    public void CloseActionPanel()
    {
        actionPanel.SetActive(false);
        itemCurrentlySelected = null;
    }

    public void UseActionButton()
    {
        // chercher dans le script playerStats la fonction ConsumeItem (pour que l'item selectionner . applique l'effet de heal creer dans le script itemData).
        playerStats.ConsumeItem(itemCurrentlySelected.healthEffect);
        // chercher dans le script Inventory . la variable instance en public . pour utiliser la fonction RemoveItem pour supprimer (l'item selectionner').
        Inventory.instance.RemoveItem(itemCurrentlySelected);

        CloseActionPanel();
    }

    public void EquipActionButton()
    {
        equipment.EquipAction();
    }
    

    public void DropActionButton()
    {
        GameObject instantiatedItem = Instantiate(itemCurrentlySelected.prefab);
        instantiatedItem.transform.position = dropPoint.position;
        Inventory.instance.RemoveItem(itemCurrentlySelected);
        Inventory.instance.RefreshContent();
        CloseActionPanel();
    }

    public void DestroyActionButton()
    {
        Inventory.instance.RemoveItem(itemCurrentlySelected);
        Inventory.instance.RefreshContent();
        CloseActionPanel();
    }


}
