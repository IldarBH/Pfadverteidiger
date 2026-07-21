using UnityEngine;

public class CareerScene : MonoBehaviour
{
    private CareerCanvas _careerCanvas;
    private ContractCanvas _contractCanvas;
    private CareerCamera _careerCamera;

    private void Awake()
    {
        _careerCamera = GameObject.Find("CareerCamera").GetComponent<CareerCamera>();

        _careerCanvas = GameObject.Find("CareerCanvas").GetComponent<CareerCanvas>();
        _careerCanvas.Initialize(this);
        _contractCanvas = GameObject.Find("ContractCanvas").GetComponent<ContractCanvas>();
        _contractCanvas.Initialize(this);
    }

    void OnEnable()
    {
        _careerCanvas.gameObject.SetActive(true);
        _contractCanvas.gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        _careerCamera.GoToMainMenu();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void GoToHangar()
    {
        _careerCamera.GoToHangar();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Hangar", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void GoToContract()
    {
        _careerCamera.GoToContract();
        _careerCanvas.gameObject.SetActive(false);
        _contractCanvas.gameObject.SetActive(true);
    }

    public void GoToCareer()
    {
        _careerCamera.GoToCareer();
        _careerCanvas.gameObject.SetActive(true);
        _contractCanvas.gameObject.SetActive(false);
    }
}
