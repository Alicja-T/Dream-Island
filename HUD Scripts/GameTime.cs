using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTime : MonoBehaviour {
    private float previousTime;
    private float currentTime;
    private float delta;
    private float gameTime = 0;
    private float minutes;
    private float min = 0;
    private float hours = 12;
    private float days = 1;
    private float dayHour = 12;
    private string amPM = "am";
    public float timeSpeed = 10;
    
    Text timeLabel;
	// Use this for initialization
	void Start () {
        previousTime = Time.time;
        timeLabel = this.GetComponent<Text>();
        timeLabel.text = string.Format("Day {0:00} {1:00} {2:00}" + amPM, days, hours, min);
        }
	
	// Update is called once per frame
	void Update () {
        gameTime = gameTime + Time.deltaTime;
        delta = gameTime - previousTime;
        if ((delta * timeSpeed) > 60) {
            min = UpdateMin();
            previousTime = gameTime;
           }
        if (hours >= 12)
        {
            amPM = "pm";
            if (hours > 12) {
                dayHour = hours - 12;
            }
        }
        else {
            amPM = "am";
            dayHour = hours;
        }
        timeLabel = this.GetComponent<Text>();
        timeLabel.text = string.Format("Day {0:00} {1:00}:{2:00}" + amPM, days, dayHour, min);
        
    }

    public float GetTime() { //returns gameTime in minutes relative to timeSpeed from the beginning of the game
        currentTime = (gameTime / 60) * timeSpeed;
        return currentTime;
    }

    float UpdateMin() {
        min = min + (delta * timeSpeed) / 60;
        if (min > 60) {
            hours = UpdateHours();
            min = min % 60;
        }
        return min;
    }

    float UpdateDays() {
        days = days + (int)(hours / 24);
        return days;
    }

    float UpdateHours() {
        hours = hours + (int)( min / 60);
        if (hours >= 24)
        {
            days = UpdateDays();
            hours = hours % 24;
        }
        return hours;
    }

    public float GetHours() {
        return hours;
    }

    public float GetMin() {
        return min;
    }

    public float GetDays() {
        return days;
    }
}
