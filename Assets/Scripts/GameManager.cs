using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ProgressBar pb;
    public GameObject playerContainer;

    private int Multiplier = 0;

    private bool FinishLineStatus = false;
    public SoundAudioClip[] soundAudioClipArray;
    private bool isAlive = false;
    private bool isOver = false;
    public int coins = 0;
    public TextMeshProUGUI coinsText;
    public GameObject LevelCompletePanel;
    public GameObject LevelIncompletePanel;
    public void EnableLevelComplete()
    {
        LevelCompletePanel.SetActive(true);
    }
    public void EnableLevelIncomplete()
    {
        LevelIncompletePanel.SetActive(true);
    }


    public void setAlive(bool status)
    {
        isAlive = status;

    }
    public bool getAlive()
    {
        return isAlive;
    }
    public void setOver(bool status)
    {
        isOver = status;
    }
    public bool getOver()
    {
        return isOver;
    }
    void Update()
    {

        coinsText.text = coins.ToString("0");

    }
    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;

    }

    public void setMultiplier()
    {
        Multiplier++;
    }
    public void FinishLine()
    {
        Multiplier++;
        FinishLineStatus = true;
    }
    public bool GetFinishLine()
    {
        return FinishLineStatus;
    }
    public int getMultiplier()
    {
        return Multiplier;
    }
    public void EndGame(string type)
    {
        setAlive(false);
        GameObject playerContainer = GameObject.FindGameObjectWithTag("PlayerContainer");
        PlayerController pc = playerContainer.GetComponent<PlayerController>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Animator PlayerAnimator = player.GetComponent<Animator>();

        setOver(true);
        if (type == "Multiplier" || type == "FinishLine")
        {
            Debug.Log("You Win");
            Debug.Log(getMultiplier());
            PlayerAnimator.SetBool("isVictorious", true);
            Invoke("EnableLevelComplete", 2f);
            return;
        }



        player.transform.SetParent(null);
        player.GetComponent<Collider>().enabled = true;
        player.AddComponent<Rigidbody>();
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -100));
        PlayerAnimator.SetBool("isDead", true);

        playerContainer.transform.SetParent(null);
        playerContainer.GetComponent<Collider>().enabled = true;
        playerContainer.GetComponent<Collider>().isTrigger = false;
        if (type == "Lava")
        {

            GameObject mainCube = GameObject.FindGameObjectWithTag("MainCube");
            Destroy(mainCube);
            Destroy(player, 2f);
        }
        Invoke("EnableLevelIncomplete", 2f);

    }

    public void CollectCoin()
    {
        coins++;
    }
}
