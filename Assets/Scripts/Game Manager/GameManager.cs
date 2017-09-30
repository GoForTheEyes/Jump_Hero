using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject player;
    public GameObject platform;

    private float minX = -2.5f, maxX = 2.5f, minY = -4.7f, maxY = -3.7f;

    private bool lerpCamera;
    private float lerpTime = 1.5f;
    private float lerpX;

    public Slider jumpPowerBar;

    void Awake()
    {
        Singleton();
        CreateInitialPlatforms();
    }

    void Singleton()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void CreateInitialPlatforms()
    {
        Vector3 temp = new Vector3(Random.Range(minX, minX + 1.2f), Random.Range(minY, maxY), 0);
        Instantiate(platform, temp, Quaternion.identity);
        temp.y += 2f;
        Instantiate(player, temp, Quaternion.identity);
        temp = new Vector3(Random.Range(maxX, maxX - 1.2f), Random.Range(minY, maxY), 0);
        Instantiate(platform, temp, Quaternion.identity);

    }

    public void CreateNewPlatformAndLerp(float lerpPosition)
    {
        CreateNewPlatform();
        lerpX = lerpPosition + maxX;
        lerpCamera = true;
    }

    void LerpTheCamera()
    {
        float x = Camera.main.transform.position.x;
        x = Mathf.Lerp(x, lerpX, lerpTime * Time.deltaTime);
        Camera.main.transform.position = new Vector3(x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        if (Camera.main.transform.position.x >= (lerpX - 0.07f))
        {
            lerpCamera = false;
        }


    }

    void CreateNewPlatform()
    {
        float cameraX = Camera.main.transform.position.x;
        float newMaxX = (maxX * 2) + cameraX;
        Instantiate(platform, new Vector3(Random.Range(newMaxX, newMaxX - 1.2f), Random.Range(minY, maxY - 1.2f), 0f), Quaternion.identity);
    }

    public void GameOverActions()
    {
        ScoreManager.instance.scoreText.gameObject.SetActive(false);
        ScoreManager.instance.CheckIfHighScore();
        GameOverManager.instance.GameOver(ScoreManager.instance.GetScore(), ScoreManager.instance.GetHighScore());
    }

	
	// Update is called once per frame
	void Update () {
		if (lerpCamera)
        {
            LerpTheCamera();
        }
        float jumpPower = PlayerJumpScript.instance.GetJumpPower();
        jumpPowerBar.value = jumpPower;
	}

}
