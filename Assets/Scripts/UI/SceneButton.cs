using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _scene;
    [SerializeField] private LoadSceneMode mode;

    private void Awake()
    {
        _button.onClick.AddListener(SetState);
    }

    private void SetState()
    {
        GameManager.instance.SceneManager.LoadScene(_scene, mode, true);
    }
}