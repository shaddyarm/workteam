using UnityEngine;
using System.Collections;
 
public class BlendShapeExample : MonoBehaviour
{
    int blendShapeCount;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
	
	public float value=0;

    void Awake ()
    {
        //skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
        skinnedMesh = skinnedMeshRenderer.sharedMesh;
		
    }

    void Start ()
    {
        blendShapeCount = skinnedMesh.blendShapeCount; 
		Debug.Log ("blendShapeCount=" + blendShapeCount);
    }

    void Update ()
    {
        if (blendShapeCount > 0) 
		{
			skinnedMeshRenderer.SetBlendShapeWeight (68, value);
        }
    }
}