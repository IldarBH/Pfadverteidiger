using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    private Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartupToCareer() { _animator.SetTrigger("StartupToCareer"); }
    public void CareerToStartup() { _animator.SetTrigger("CareerToStartup"); }
    public void CareerToContract() { _animator.SetTrigger("CareerToContract"); }
    public void ContractToCareer() { _animator.SetTrigger("ContractToCareer"); }
}
