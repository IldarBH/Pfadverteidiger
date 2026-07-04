using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    private Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartupToCareer() { _animator.Play("StartupToCareer"); }
    public void CareerToStartup() { _animator.Play("CareerToStartup"); }
    public void CareerToContract() { _animator.Play("CareerToContract"); }
    public void ContractToCareer() { _animator.Play("ContractToCareer"); }
}
