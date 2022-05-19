using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameBehaviour : MonoBehaviour
{
    playerBehaviour playerBeh;
    private int itemsCollected;
    public float smashCustomerForce = 450f;

    public Material skybox1;
    public Material skybox2;
    public Material skybox3;
    public Text bagScoreText;
    public GameObject endScreen;
    public GameObject torba1;
    public GameObject torba2;

    private void Start()
    {
        //RenderSettings.skybox = skybox1;
        playerBeh = FindObjectOfType<playerBehaviour>().GetComponent<playerBehaviour>();
    }

    public void collectItem()
    {
        itemsCollected++;
        changeBagScore();
        if (itemsCollected == 1)
        {
            torba1.SetActive(true);
        }
        if (itemsCollected == 2)
        {
            torba2.SetActive(true);
        }
        //RenderSettings.skybox = skybox2;
    }

    public void wallHit()
    {
        playerBeh.wallHit();
    }
    public void endOfLevel()
    {
        playerBeh.endOfLevel();
    }
    public void strikeCustomer(Rigidbody customer)
    {
        {
            customer.AddForce(customer.transform.right * smashCustomerForce);
            //customer.AddForce(customer.transform.up * 150f);
            smashCustomerForce *= -1;
            itemsCollected--;
            changeBagScore();
        }
    }

    public int getItemsCollected()
    {
        return itemsCollected;
    }

    public void changeSkyBox(int i)
    {
        if (i == 1)
        {
            RenderSettings.skybox = skybox1;
        }
        else if (i == 2)
        {
            RenderSettings.skybox = skybox2;
        }
        else if(i == 3)
        {
            RenderSettings.skybox = skybox3;
        }
        
    }
    public void changeBagScore()
    {
        bagScoreText.text = "x" + itemsCollected;
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
    }

    public void continueGame()
    {
        Time.timeScale = 1f;
    }

    public void changeScene(int i)
    {
        SceneManager.LoadScene(i);
        Time.timeScale = 1f;
    }
    public void retryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void showEndScreen()
    {
        endScreen.SetActive(true);
        pauseGame();
    }
}
