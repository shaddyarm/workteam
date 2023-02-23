using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
 
public class Hotspot : MonoBehaviour
{
    public GameObject ThisPanorama;
    public GameObject TargetPanorama;
	//public Material sky;
	public VideoPlayer player;
	public  string newUrl;
    
    // Update is called once per frame
    void Update () 
    {
        transform.Rotate(0, 0.5f, 0);
    }
 
	void OnMouseDown()
	{
		SetSkyBox();
	}
 
   
    private void SetSkyBox() 
    {
        if(TourManager.SetCameraPosition != null)
		{
            TourManager.SetCameraPosition(ThisPanorama.transform.position, TargetPanorama.transform.position);  
		}
        TargetPanorama.gameObject.SetActive(true);
        ThisPanorama.gameObject.SetActive(false);
		
		
		player.url = System.IO.Path.Combine (Application.streamingAssetsPath,newUrl);
		//RenderSettings.skybox=sky;
    }
}