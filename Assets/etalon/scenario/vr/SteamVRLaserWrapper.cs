 using UnityEngine;
 using UnityEngine.EventSystems;
 using Valve.VR.Extras;
 using UnityEngine.UI;
 
 
 //в файле LaserPointer.cs нужно брать
 //argsIn.target = hit.collider.gameObject.transform; // А не   hit.transform;
 
 
 public class SteamVRLaserWrapper : MonoBehaviour
 {
     private SteamVR_LaserPointer steamVrLaserPointer;
 
     private void Awake()
     {
		 //Debug.Log ("Awake=");
		 
         steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
         steamVrLaserPointer.PointerIn += OnPointerIn;
         steamVrLaserPointer.PointerOut += OnPointerOut;
         steamVrLaserPointer.PointerClick += OnPointerClick;
     }
 
     private void OnPointerClick(object sender, PointerEventArgs e)
     {
		
		Button referenceToTheButton = e.target.gameObject.GetComponent<Button>();
		if (referenceToTheButton != null)
		{
			Debug.Log ("Button ОК");
			referenceToTheButton.Select ();
		    referenceToTheButton.onClick.Invoke();
			return;
		}
		 
         IPointerClickHandler clickHandler = e.target.GetComponent<IPointerClickHandler>();
         if (clickHandler != null)
         {
             clickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
			 return;
         }
 
         //Debug.Log ("OnPointerClick=" + e.target.name);
		 e.target.gameObject.SendMessage("OnMouseDown", UnityEngine.SendMessageOptions.DontRequireReceiver);
     }
 
     private void OnPointerOut(object sender, PointerEventArgs e)
     {
		 //Debug.Log ("OnPointerOut=" + e.target.gameObject.name);
		 IPointerExitHandler pointerExitHandler = e.target.GetComponent<IPointerExitHandler>();
         if (pointerExitHandler != null)
         {
             pointerExitHandler.OnPointerExit(new PointerEventData(EventSystem.current));
			 return;
         }
 
         e.target.gameObject.SendMessage("OnMouseExit", UnityEngine.SendMessageOptions.DontRequireReceiver);
     }
 
     private void OnPointerIn(object sender, PointerEventArgs e)
     {
		 //Debug.Log ("OnPointerIn=" + e.target.name);
		 
		 IPointerEnterHandler pointerEnterHandler = e.target.GetComponent<IPointerEnterHandler>();
         if (pointerEnterHandler != null)
         {
             pointerEnterHandler.OnPointerEnter(new PointerEventData(EventSystem.current));
			 return;
         }
		 //
		 
		 e.target.gameObject.SendMessage("OnMouseEnter", UnityEngine.SendMessageOptions.DontRequireReceiver);

     }
 }
 
 
 /*
взято для памяти, что подправил
SteamVR_LaserPointer.cs
using UnityEngine;
using System.Collections;

namespace Valve.VR.Extras
{
    public class SteamVR_LaserPointer : MonoBehaviour
    {
        public SteamVR_Behaviour_Pose pose;

        //public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.__actions_default_in_InteractUI;
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");

        public bool active = true;
        public Color color;
        public float thickness = 0.002f;
        public Color clickColor = Color.green;
        public GameObject holder;
        public GameObject pointer;
        bool isActive = false;
        public bool addRigidBody = false;
        public Transform reference;
        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;
        public event PointerEventHandler PointerClick;

        Transform previousContact = null;


        private void Start()
        {
            if (pose == null)
                pose = this.GetComponent<SteamVR_Behaviour_Pose>();
            if (pose == null)
                Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

            if (interactWithUI == null)
                Debug.LogError("No ui interaction action has been set on this component.", this);


            holder = new GameObject();
            holder.transform.parent = this.transform;
            holder.transform.localPosition = Vector3.zero;
            holder.transform.localRotation = Quaternion.identity;

            pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pointer.transform.parent = holder.transform;
            pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
            pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
            pointer.transform.localRotation = Quaternion.identity;
            BoxCollider collider = pointer.GetComponent<BoxCollider>();
            if (addRigidBody)
            {
                if (collider)
                {
                    collider.isTrigger = true;
                }
                Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
                rigidBody.isKinematic = true;
            }
            else
            {
                if (collider)
                {
                    Object.Destroy(collider);
                }
            }
            Material newMaterial = new Material(Shader.Find("Unlit/Color"));
            newMaterial.SetColor("_Color", color);
            pointer.GetComponent<MeshRenderer>().material = newMaterial;
        }

        public virtual void OnPointerIn(PointerEventArgs e)
        {
            if (PointerIn != null)
                PointerIn(this, e);
        }

        public virtual void OnPointerClick(PointerEventArgs e)
        {
            if (PointerClick != null)
                PointerClick(this, e);
        }

        public virtual void OnPointerOut(PointerEventArgs e)
        {
            if (PointerOut != null)
                PointerOut(this, e);
        }


        private void Update()
        {
            if (!isActive)
            {
                isActive = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }

            float dist = 100f;

            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);
			
			//if (hit.collider !=null)
			//{
			//	Debug.Log ("*" + hit.collider.gameObject.name);
			//}

            if (previousContact && previousContact != hit.transform)
            {
                PointerEventArgs args = new PointerEventArgs();
                args.fromInputSource = pose.inputSource;
                args.distance = 0f;
                args.flags = 0;
                args.target = previousContact;
                OnPointerOut(args);
                previousContact = null;
            }
            if (bHit && previousContact != hit.transform)
            {
                PointerEventArgs argsIn = new PointerEventArgs();
                argsIn.fromInputSource = pose.inputSource;
                argsIn.distance = hit.distance;
                argsIn.flags = 0;
                argsIn.target = hit.collider.gameObject.transform; //hit.transform;
                OnPointerIn(argsIn);
                previousContact = hit.transform;
            }
            if (!bHit)
            {
                previousContact = null;
            }
            if (bHit && hit.distance < 100f)
            {
                dist = hit.distance;
            }

            if (bHit && interactWithUI.GetStateUp(pose.inputSource))
            {
                PointerEventArgs argsClick = new PointerEventArgs();
                argsClick.fromInputSource = pose.inputSource;
                argsClick.distance = hit.distance;
                argsClick.flags = 0;
                argsClick.target = hit.collider.gameObject.transform;//hit.transform;
                OnPointerClick(argsClick);
            }

            if (interactWithUI != null && interactWithUI.GetState(pose.inputSource))
            {
                pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
                pointer.GetComponent<MeshRenderer>().material.color = clickColor;
            }
            else
            {
                pointer.transform.localScale = new Vector3(thickness, thickness, dist);
                pointer.GetComponent<MeshRenderer>().material.color = color;
            }
            pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
        }
    }

    public struct PointerEventArgs
    {
        public SteamVR_Input_Sources fromInputSource;
        public uint flags;
        public float distance;
        public Transform target;
    }

    public delegate void PointerEventHandler(object sender, PointerEventArgs e);
}
 */