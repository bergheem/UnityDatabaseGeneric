using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
	[HideInInspector] public int id;

	private Inventory inv;

	void Start()
	{
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
	}

	#region IDropHandler implementation

	public void OnDrop(PointerEventData eventData)
	{
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
		if (inv.items[id].ID == -1)
		{
			inv.items[droppedItem.slot] = new Item();
			inv.items[id] = droppedItem.item;
			droppedItem.slot = id;
		}
		else if (droppedItem.slot != id)
		{
			Transform itemToSwap = this.transform.GetChild(0);
			itemToSwap.GetComponent<ItemData>().slot = droppedItem.slot;
			itemToSwap.transform.SetParent(inv.slots[droppedItem.slot].transform);
			itemToSwap.transform.position = inv.slots[droppedItem.slot].transform.position;

			droppedItem.slot = id;
			droppedItem.transform.SetParent(this.transform);
			droppedItem.transform.position = this.transform.position;

			inv.items[droppedItem.slot] = itemToSwap.GetComponent<ItemData>().item;
			inv.items[id] = droppedItem.item;
		}
	}

	#endregion
}
