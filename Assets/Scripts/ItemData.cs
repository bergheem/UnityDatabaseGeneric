using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Item item;
	public int amount;
	public int slot;

	private Inventory inv;
	private Tooltip tooltip;
	private Vector2 offset;

	void Start()
	{
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
		tooltip = inv.GetComponent<Tooltip>();
	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (item != null)
		{
			this.transform.SetParent(this.transform.parent.parent);
			this.transform.position = eventData.position - offset;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag(PointerEventData eventData)
	{
		if (item != null)
		{
			this.transform.position = eventData.position - offset;
		}
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag(PointerEventData eventData)
	{
		this.transform.SetParent(inv.slots[slot].transform);
		this.transform.position = inv.slots[slot].transform.position;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	#endregion

	#region IPointerDownHandler implementation

	public void OnPointerDown(PointerEventData eventData)
	{
		if (item != null)
		{
			offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
		}
	}

	#endregion

	#region IPointerEnterHandler implementation

	public void OnPointerEnter(PointerEventData eventData)
	{
		tooltip.Activate(item);
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.Deactivate();
	}

	#endregion
}
