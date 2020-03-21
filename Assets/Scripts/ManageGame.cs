using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    [SerializeField] public bool isReloadable = false;
    public float meteorHitPoint;
    [SerializeField] Text hitPointText;
    [SerializeField] public Button playButton;
    [SerializeField] CannonMovement cannonMovement;

    private void Start()
    {
        isReloadable = false;
        hitPointText = GameObject.Find("HitPointText").GetComponent<Text>();
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        cannonMovement = FindObjectOfType<CannonMovement>();
    }

    private void Update()
    {
        hitPointText.text = meteorHitPoint.ToString();
        
    }

    public void StartGame()
    {
        cannonMovement.startGame = true;
        playButton.gameObject.SetActive(false);
        if (isReloadable) 
        { 
            ReloadGame();
            isReloadable = false;
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreasePoints()
    {
        meteorHitPoint += 2f;
        hitPointText.text = meteorHitPoint.ToString();
    }
}
