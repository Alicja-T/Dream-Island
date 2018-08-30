using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private InputManager inputManagerDatabase;
    public float animSpeed = 1.5f;
    private Animator anim;
    private CapsuleCollider col;
    public Inventory inventory;
    PlayerInventory playerInv;
    bool isSitting = false;

    // Use this for initialization
    void Awake()
    {
        inputManagerDatabase = (InputManager)Resources.Load("InputManager");
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        playerInv = GetComponent<PlayerInventory>();
        
    }



    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float s = playerInv.GetStamina();
        if (Input.GetKeyDown(inputManagerDatabase.SitKeyCode)) {
			isSitting = !isSitting;
        }
        anim.SetFloat("Speed", v);
        anim.SetFloat("Direction", h);
        anim.speed = animSpeed;
		anim.SetBool ("Sit", isSitting);
        CollectItem();
    }

    void CollectItem() {

        if (Input.GetMouseButtonDown(0))
        { //left mouse button
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Pickable Item") {

                    ItemButton itb = hit.collider.gameObject.GetComponent<ItemButton>();
                    string name = itb.GetItemName();
                    if (inventory == null)
                    {
                        print("no inventory");
                    }
                    else print("there is an inventory");
                    inventory.AddItem(itb.GetItemID());
                    Destroy(hit.collider.gameObject);
                    print("Added " + name + " to inventory");

                }

            }


        }//if mousebutton down end



    }



}
