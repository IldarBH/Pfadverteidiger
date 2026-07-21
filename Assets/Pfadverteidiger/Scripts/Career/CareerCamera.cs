using UnityEngine;

public class CareerCamera : MonoBehaviour
{
    private Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void GoToMainMenu()
    {
        // TODO: Implement camera transition to main menu
    }

    public void GoToHangar()
    {
        // TODO: Implement camera transition to hangar
    }

    public void GoToContract()
    {
        // TODO: Implement camera transition to contract
    }

    public void GoToCareer()
    {
        // TODO: Implement camera transition to career
    }
}
