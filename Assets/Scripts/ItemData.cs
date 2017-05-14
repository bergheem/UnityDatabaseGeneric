using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Item item;
	public int amount;

	private Transform originaParent;
	private Vector2 offset;

	#region IBeginDragHandler implementation

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (item != null)
		{
			offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
			originaParent = this.transform.parent;
			this.transform.SetParent(this.transform.parent.parent);
			this.transform.position = eventData.position - offset;
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
		this.transform.SetParent(originaParent);
	}

	#endregion
}
