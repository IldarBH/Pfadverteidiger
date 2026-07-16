using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildPlatformPanel : MonoBehaviour
{
    public enum State
    {
        UpgradeAvailable,
        UpgradeUnavailable,
        UpgradeSelected,
    }
    
    public Sprite upgradeAvailable;
    public Sprite upgradeUnavailable;
    public Sprite upgradeSelected;

    private Image _spriteImage;
    private GameObject _tower;
    private HangarScene _hangarScene;
    private State _state;

    void Awake()
    {
        _spriteImage = GetComponent<Image>();
        UtilityHelpers.RegisterEvent(this, EventTriggerType.PointerClick, (data) => OnPointerClick(data));
    }

    public void Initialize(HangarScene hangarScene, GameObject tower)
    {
        _hangarScene = hangarScene;
        _tower = tower;
    }

    void OnPointerClick(BaseEventData data)
    {
        if (_state == State.UpgradeAvailable)
        {
            _state = State.UpgradeSelected;
            UpdateSprite();
            _hangarScene.InstallPlatformOnTower(_tower);
        }
    }

    private void UpdateSprite()
    {
        switch (_state)
        {
            case State.UpgradeAvailable:
                _spriteImage.sprite = upgradeAvailable;
                break;
            case State.UpgradeUnavailable:
                _spriteImage.sprite = upgradeUnavailable;
                break;
            case State.UpgradeSelected:
                _spriteImage.sprite = upgradeSelected;
                break;
        }
    }

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (_tower != null)
        {
            var towerBounds = _tower.GetComponent<Renderer>().bounds;
            var towerPosition = towerBounds.center;
            var screenPosition = Camera.main.WorldToScreenPoint(towerPosition);
            transform.position = screenPosition;
        }
    }
}
