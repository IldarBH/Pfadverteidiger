using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    private Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void GoToCareer()
    {
        _animator.Play("GoToCareer");
    }

    public void GoToStartup()
    {
        _animator.Play("GoToStartup");
    }
}
