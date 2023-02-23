using UnityEngine;
using System.Collections;
 
public class BlendShapeControl : MonoBehaviour
{

    public SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
	
	public float value=0;
    public int blendShapeNumber=0;

    void Awake ()
    {
        //skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
        skinnedMesh = skinnedMeshRenderer.sharedMesh;
		
    }

    void Start ()
    {
        int blendShapeCount = skinnedMesh.blendShapeCount; 
		Debug.Log ("blendShapeCount=" + blendShapeCount);
    }


    public void SetNumber(int value)
    {
        blendShapeNumber = value;
    }

    public void SetValue ( float value)
   {
	   skinnedMeshRenderer.SetBlendShapeWeight (blendShapeNumber, value);
   }
   
}