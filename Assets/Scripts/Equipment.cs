using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Equipment : MonoBehaviour
{

 [Header("OTHER SCRIPTS REFERENCES")]

   [SerializeField]
    public ItemActionsSystem itemActionsSystem;

 [Header("EQUIPMENT SYSTEM VARIABLE")]
    
    [SerializeField]
    private EquipmentLibrary equipmentLibrary;

    [SerializeField]
    private Image headSlotImage;

    [SerializeField]
    private Image chestSlotImage;

    [SerializeField]
    private Image handsSlotImage;

    [SerializeField]
    private Image legsSlotImage;

    [SerializeField]
    private Image feetSlotImage;

    // Garde une trace des equipements actuels
    private ItemData equipedHeadItem;
    private ItemData equipedChestItem;
    private ItemData equipedHandsItem;
    private ItemData equipedLegsItem;
    private ItemData equipedFeetItem;

    [SerializeField]
    private Button headSlotDesequipButton;

    [SerializeField]
    private Button chestSlotDesequipButton;

    [SerializeField]
    private Button handsSlotDesequipButton;
    
    [SerializeField]
    private Button legsSlotDesequipButton;

    [SerializeField]
    private Button feetSlotDesequipButton;

    private void DisablePreviousEquipedEquipment(ItemData itemToDisable)
   {
         if(itemToDisable == null)
       {
             return;
       }

         // 1. enlever le visuel de lequip sur le perso. reactive les partie visuel quon avait desactiver dans le body du perso.
            EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemToDisable).First();

        if(equipmentLibraryItem != null)
           {

        for (int i = 0; i < equipmentLibraryItem.elementsToDisable.Length; i++)
        {
            equipmentLibraryItem.elementsToDisable[i].SetActive(true);
        }

            equipmentLibraryItem.itemPrefab.SetActive(false);

           }//......................................................................

        Inventory.instance.AddItem(itemToDisable);
     
}


    public void DesequipEquipment(EquipmentType equipmentType)
        {
            
            // 3. renvoyer lequip dans linventaire du perso
            // 4. refreshcontent a la fin pour actualiser les differents panels

            if(Inventory.instance.IsFull())
            {
                Debug.Log("Inventaire plein impossible de desequipe");
                return;
            }

            ItemData currentItem = null;

            switch(equipmentType)
            {
                case EquipmentType.Head:
                    currentItem = equipedHeadItem;
                    equipedHeadItem = null;
                    // 2. enlever le visuel de lequip de la colonne quip de linventaire.
                    headSlotImage.sprite = Inventory.instance.emptySlotVisual;
                    break;

                case EquipmentType.Chest:
                    currentItem = equipedChestItem;
                    equipedChestItem = null;
                    // 2. enlever le visuel de lequip de la colonne quip de linventaire.
                    chestSlotImage.sprite = Inventory.instance.emptySlotVisual;
                    break;

                case EquipmentType.Hands:
                    currentItem = equipedHandsItem;
                    equipedHandsItem = null;
                    // 2. enlever le visuel de lequip de la colonne quip de linventaire.
                    handsSlotImage.sprite = Inventory.instance.emptySlotVisual;
                    break;

                case EquipmentType.Legs:
                    currentItem = equipedLegsItem;
                    equipedLegsItem = null;
                    // 2. enlever le visuel de lequip de la colonne quip de linventaire.
                    legsSlotImage.sprite = Inventory.instance.emptySlotVisual;
                    break;

                case EquipmentType.Feet:
                    currentItem = equipedFeetItem;
                    equipedFeetItem = null;
                    // 2. enlever le visuel de lequip de la colonne quip de linventaire.
                    feetSlotImage.sprite = Inventory.instance.emptySlotVisual;
                    break;

            }
            // 1. enlever le visuel de lequip sur le perso. reactive les partie visuel quon avait desactiver dans le body du perso.
            EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == currentItem).First();

        if(equipmentLibraryItem != null)
        {

        for (int i = 0; i < equipmentLibraryItem.elementsToDisable.Length; i++)
        {
            equipmentLibraryItem.elementsToDisable[i].SetActive(true);
        }

            equipmentLibraryItem.itemPrefab.SetActive(false);

        }//......................................................................

        Inventory.instance.AddItem(currentItem);

        // 4. refreshcontent a la fin pour actualiser les differents panels
        Inventory.instance.RefreshContent();
     }

     public void UpdateEquipmentsDesequipButtons()
    {
        headSlotDesequipButton.onClick.RemoveAllListeners();
        headSlotDesequipButton.onClick.AddListener(delegate {DesequipEquipment(EquipmentType.Head); });
        headSlotDesequipButton.gameObject.SetActive(equipedHeadItem);

        chestSlotDesequipButton.onClick.RemoveAllListeners();
        chestSlotDesequipButton.onClick.AddListener(delegate {DesequipEquipment(EquipmentType.Chest); });
        chestSlotDesequipButton.gameObject.SetActive(equipedChestItem);

        handsSlotDesequipButton.onClick.RemoveAllListeners();
        handsSlotDesequipButton.onClick.AddListener(delegate {DesequipEquipment(EquipmentType.Hands); });
        handsSlotDesequipButton.gameObject.SetActive(equipedHandsItem);

        legsSlotDesequipButton.onClick.RemoveAllListeners();
        legsSlotDesequipButton.onClick.AddListener(delegate {DesequipEquipment(EquipmentType.Legs); });
        legsSlotDesequipButton.gameObject.SetActive(equipedLegsItem);

        feetSlotDesequipButton.onClick.RemoveAllListeners();
        feetSlotDesequipButton.onClick.AddListener(delegate {DesequipEquipment(EquipmentType.Feet); });
        feetSlotDesequipButton.gameObject.SetActive(equipedFeetItem);

    }

    public void EquipAction()
    {
        print("Equip item : " + itemActionsSystem.itemCurrentlySelected.name);

        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemActionsSystem.itemCurrentlySelected).First();

        if(equipmentLibraryItem != null)
        {
             switch(itemActionsSystem.itemCurrentlySelected.equipmentType)
            {
                case EquipmentType.Head:
                    DisablePreviousEquipedEquipment(equipedHeadItem);
                    headSlotImage.sprite = itemActionsSystem.itemCurrentlySelected.visual;
                    equipedHeadItem = itemActionsSystem.itemCurrentlySelected;
                    break;

                case EquipmentType.Chest:
                    DisablePreviousEquipedEquipment(equipedChestItem);
                    chestSlotImage.sprite = itemActionsSystem.itemCurrentlySelected.visual;
                    equipedChestItem = itemActionsSystem.itemCurrentlySelected;
                    break;

                 case EquipmentType.Hands:
                    DisablePreviousEquipedEquipment(equipedHandsItem);
                    handsSlotImage.sprite = itemActionsSystem.itemCurrentlySelected.visual;
                    equipedHandsItem = itemActionsSystem.itemCurrentlySelected;
                    break;

                case EquipmentType.Legs:
                    DisablePreviousEquipedEquipment(equipedLegsItem);
                    legsSlotImage.sprite = itemActionsSystem.itemCurrentlySelected.visual;
                    equipedLegsItem = itemActionsSystem.itemCurrentlySelected;
                    break;

                case EquipmentType.Feet:
                    DisablePreviousEquipedEquipment(equipedFeetItem);
                    feetSlotImage.sprite = itemActionsSystem.itemCurrentlySelected.visual;
                    equipedFeetItem = itemActionsSystem.itemCurrentlySelected;
                    break;

            }

            for (int i = 0; i < equipmentLibraryItem.elementsToDisable.Length; i++)
        {
            equipmentLibraryItem.elementsToDisable[i].SetActive(false);
        }

            equipmentLibraryItem.itemPrefab.SetActive(true);

            Inventory.instance.RemoveItem(itemActionsSystem.itemCurrentlySelected);

        }
        else
        {
            Debug.LogError("Equipment : " + itemActionsSystem.itemCurrentlySelected.name + "non existant dans la librarie des equipements");
        }

        itemActionsSystem.CloseActionPanel();
    }






}
