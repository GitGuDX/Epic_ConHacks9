using Fusion;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Canvas canvas;
    public NetworkRunner runner;

    public void PlayGame()
    {
        Debug.Log("fuck");
        canvas.gameObject.SetActive(false);
        runner.StartGame(new StartGameArgs {
            EnableClientSessionCreation = true,
            PlayerCount = 2,
            IsOpen = true,
            IsVisible = true,
        });
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
