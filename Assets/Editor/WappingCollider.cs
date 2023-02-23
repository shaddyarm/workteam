#if UNITY_EDITOR
 using UnityEditor;
 #endif
 using UnityEngine;
 
 [RequireComponent(typeof(BoxCollider))]
 public class WappingCollider : MonoBehaviour {
 
 #if UNITY_EDITOR
 
     [MenuItem("Custom/Wrap BoxCollider to children %&f")]
     static void WrapBoxColliderToChildren() {
         foreach (var parentObject in Selection.gameObjects) {
             var bounds = new Bounds(Vector3.zero, Vector3.zero);
             var hasBounds = false;
             for (int i = 0; i < parentObject.transform.childCount; ++i) {
                 var childRenderer = parentObject.transform.GetChild(i).GetComponent<Renderer>();
                 if (childRenderer) {
                     //print($"{childRenderer.name} bounds {childRenderer.bounds}");
                     if (hasBounds) {
                         bounds.Encapsulate(childRenderer.bounds);
                     } else {
                         bounds = childRenderer.bounds;
                         hasBounds = true;
                     }
                 }
             }
             if (hasBounds) {
                 var boxCollider = parentObject.GetComponent<BoxCollider>();
                 if (!boxCollider) boxCollider = parentObject.AddComponent<BoxCollider>();
                 var scale = parentObject.transform.localScale;
                 scale = new Vector3(1 / scale.x, 1 / scale.y, 1 / scale.z);
                 boxCollider.center = Vector3.Scale((bounds.center - parentObject.transform.position), scale);
                 boxCollider.size = Vector3.Scale(bounds.size, scale);
                 //print($"{parentObject.name} wrapped");
             }
             //else {
             //    print($"Skipping {parentObject.name}");
             //}
         }
     }
 
 #endif
 }