using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class weather : MonoBehaviour
{
    public TextMeshProUGUI countDownText; 
    public TextMeshProUGUI directionText; 
    public Image wind;
    private float countDown = 3;
    private bool isWeatherForecastRunning = false;
    public cameraShake camera;

    void Start()
    {
        wind.enabled = false;
    }

    void TriggerShake()
    {
        StartCoroutine(camera.Shake(5f, 5f));
    }

    void Update()
    {
        if (!isWeatherForecastRunning) {
            StartCoroutine(waitWeatherForecast());
        }
    }

    IEnumerator waitWeatherForecast()
    {
        isWeatherForecastRunning = true;
        string directionString = "";
        int direction = Random.Range(0, 4);
        if (direction == 0){
            directionString = "DOWN";
        }
        if (direction == 1){
            directionString = "UP";
        }
        if (direction == 2){
            directionString = "RIGHT";
        }
        if (direction == 3){
            directionString = "LEFT";
        }

        float randomWait = Random.Range(5,20);
        yield return new WaitForSeconds(randomWait);

        // Waiting additional 3 seconds for countdown
        float countDownNow = countDown;
        while (countDownNow > 0){
            countDownText.text = $"{countDownNow.ToString("0")} seconds to storm coming! Wind going:";
            directionText.color = Color.red;
            directionText.text = directionString;
            yield return new WaitForSeconds(1);
            countDownNow--;
        }  
        wind.enabled = true;

        TriggerShake();
        playerManager.Ins.EnableAllPlayersMovement(direction);

        yield return new WaitForSeconds(1);
        directionText.text = "";
        countDownText.text = "";

        yield return new WaitForSeconds(2);

        wind.enabled = false;

        isWeatherForecastRunning = false;
    }
}
