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
    public int coins = 0;
    public TextMeshProUGUI coinsText;


    public void setAlive(bool status)
    {
        isAlive = status;

    }
    public bool getAlive()
    {
        return isAlive;
    }
    void Update()
    {

        coinsText.text = coins.ToString("0");
        var zVal = playerContainer.transform.position.z;
        Transform platform = GameObject.FindGameObjectWithTag("Platform").transform;
        var scaleZ = platform.localScale.z;
        var progress = zVal * 100 / scaleZ;
        if (progress * 1.11f <= 100)
        {

            pb.BarValue = progress * 1.11f;
        }
        else
        {
            pb.BarValue = 100;
        }
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
        PlayerController pc = GameObject.FindGameObjectWithTag("PlayerContainer").GetComponent<PlayerController>();
        
        pc.KillPlayer(type);
    }

    public void CollectCoin()
    {
        coins++;
    }
}
