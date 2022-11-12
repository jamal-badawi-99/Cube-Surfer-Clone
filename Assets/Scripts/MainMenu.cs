using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject ChangeCubePanel;
    public GameObject MainMenuPanel;
    public Color CubeColor;
    public ColorManager colorManager;


    private void FixedUpdate()
    {
        if (colorManager.getCubeColor() == CubeColor)
        {

            gameObject.GetComponent<Button>().interactable = false;

        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;


        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
	Application.Quit();
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ChangeCube()
    {
        ChangeCubePanel.SetActive(true);
        MainMenuPanel.SetActive(false);


    }
    public void onCubeChange()
    {

        colorManager.setCubeColor(CubeColor);

    }
    public void GoToMainMenu()
    {

        ChangeCubePanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
}
