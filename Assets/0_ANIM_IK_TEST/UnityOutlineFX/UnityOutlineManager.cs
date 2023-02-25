//https://github.com/michaelcurtiss/UnityOutlineFX/tree/master/UnityOutlineFX


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xenu.Game {

	public class UnityOutlineManager : MonoBehaviour 
	{
		public UnityOutlineFX outlinePostEffect;
	
		//public List<GameObject> container;
		
		private void Start()
		{
		}
		
		public void AddSelect(GameObject go)
		{
			List<Renderer> rendererList = new List<Renderer>();
			foreach (Renderer objectRenderer in go.GetComponentsInChildren<Renderer>()) 
			{
				rendererList.Add(objectRenderer);
			}
		
			outlinePostEffect.AddRenderers (rendererList);
		}
		
		
		
		public void ClearSelection()
		{
			outlinePostEffect.ClearOutlineData();
		}
		
		
		//2й набор
		public void AddSelect2(GameObject go)
		{
			List<Renderer> rendererList = new List<Renderer>();
			foreach (Renderer objectRenderer in go.GetComponentsInChildren<Renderer>()) 
			{
				rendererList.Add(objectRenderer);
			}
		
			outlinePostEffect.AddRenderers2 (rendererList);
		}
		public void ClearSelection2()
		{
			outlinePostEffect.ClearOutlineData2();
		}
		
		
		
	}

}