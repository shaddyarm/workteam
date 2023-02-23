using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentShowClass : MonoBehaviour 
{
    public GameObject contentPdfScrollView;    
    public GameObject livefeedItemPrefab;
    private List<GameObject> pdfItems = new List<GameObject>();
	
	
	
	public void Setup(ref List <Sprite > pages)
	{
		foreach (GameObject item in pdfItems)
		{
			Destroy(item);
		}
		
		bool first=true;
		
        foreach (Sprite f in pages)
        {
			if (first==true)
			{
				first=false;
				RawImage tmpItem = livefeedItemPrefab.GetComponent<RawImage>() as RawImage;
				tmpItem.texture = f.texture;
				
				tmpItem.SetNativeSize();
			}
			else
			{
				GameObject pdfItem = Instantiate(livefeedItemPrefab) as GameObject; 
				RawImage tmpItem = pdfItem.GetComponent<RawImage>() as RawImage;
				tmpItem.texture = f.texture;
				tmpItem.SetNativeSize();
				pdfItems.Add(pdfItem);
				pdfItem.transform.SetParent(contentPdfScrollView.transform, false);
			}
        }
		
		
		var itemWidth = 700f;
		var t = contentPdfScrollView.GetComponent<RectTransform>();
		t.sizeDelta = new Vector2(itemWidth, t.sizeDelta.y);
		t.anchoredPosition = new Vector3(itemWidth * 1f, t.anchoredPosition.y);
	}
	
	
	
	
	
}
