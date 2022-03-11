using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    ButtonType buttonType;

    private Button buttonComponent;

    void Start()
    {
        buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.AddListener( delegate{
            switch(buttonType)
            {
                case ButtonType.Reload:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                case ButtonType.Back:
                    SceneManager.LoadScene("Menu");
                    break;

                case ButtonType.Play:
                    SceneManager.LoadScene("Penalty");
                    break;

                case ButtonType.Profile:
                    SceneManager.LoadScene("Profile");
                    break;
            }
        });
    }
}

[System.Serializable]
public enum ButtonType
{
    Reload,
    Back,
    Play,
    Profile
}
