using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class GameManager : MonoBehaviour {

    public static GameManager instance;

    void Awake()  {    
            MakeSingleton();
    }


    //singleton pattern
    void MakeSingleton() {
        if (instance != null) {
                Destroy(gameObject);
        }
        else {
                instance = this;
                DontDestroyOnLoad(gameObject);
        }

    }

       


    
}//end of GameManager class


