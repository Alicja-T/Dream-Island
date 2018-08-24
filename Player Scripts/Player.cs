﻿using UnityEngine;
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
    Inventory inventory;
    PlayerInventory playerInv;
    bool isSitting = false;

    // Use this for initialization
    void Start()
    {
        inputManagerDatabase = (InputManager)Resources.Load("InputManager");
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
       if (GameObject.Find("Player") != null)
        {
            playerInv = GameObject.Find("Player").GetComponent<PlayerInventory>();

            if (playerInv.inventory != null)
            {
                inventory = playerInv.GetComponent<Inventory>();
            }
        }
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

            if (Physics.Raycast(ray, out hit))
            {
                print("Collider tag " + hit.collider.tag);
                if (hit.collider.tag == "Pickable Item")
                {
                    print("You clicked: " + hit.collider.name);

                }

            }


        }//if mousebutton down end



    }



}
