using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
    float previousTime;
    float currentTime;
    float TimeOfDay;
    float DayDuration = 600; //day is longer than night
    float NightDuration = 420;
    float Dusk = 90;
    public GameObject moon;
    Vector3 midpoint = new Vector3(1000, 0, 1000);
    float radius = 1000;
       // Use this for initialization
    void Start () {
        GameTime gameTime = GameObject.FindGameObjectWithTag("Time").GetComponentInChildren<GameTime>();
        
    }
	
	// Update is called once per frame
	void Update () {
        GameTime gameTime = GameObject.FindGameObjectWithTag("Time").GetComponentInChildren<GameTime>();
        
        float angle = CalculateAngle(gameTime.GetHours(), gameTime.GetMin());
        transform.position = midpoint + Quaternion.Euler(0, 0, angle) * (radius * Vector3.right);
        transform.LookAt(midpoint);
        moon.transform.position = midpoint + Quaternion.Euler(0, 0, angle + 180) * (radius * Vector3.right);
        moon.transform.LookAt(midpoint);
     }

    float CalculateAngle(float hours, float min)
    {

        float TimeOfDay = hours * 60 + min;
        float angle = TimeOfDay - 360;
        if ((TimeOfDay < 1260) && (TimeOfDay > 420))
        {
            angle = angle / 4.5f;
        }
        else {
            angle = angle / 4f;
        }
        return angle;
    }

    }
