using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class ContractCanvas : MonoBehaviour
{
    private MainMenuScene _mainMenuScene;
    private Button _goBack;
    private RectTransform _contractPanel;
    private List<ContractItem> _contractItems = new List<ContractItem>();
    private GameObject _contractPrefab;
    public uint contractCount = 5; // Number of contracts to generate
    public float borderShift = 10f; // Shift from the border to avoid overlap
    void Awake()
    {
        _mainMenuScene = GameObject.Find("MainMenuScene").GetComponent<MainMenuScene>();

        _goBack = transform.Find("Back").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_goBack, EventTriggerType.PointerClick, (data) => OnGoBackClicked(data));

        _contractPrefab = Resources.Load<GameObject>("Prefabs/MainMenu/ContractItem");
        _contractPanel = transform.Find("ContractPanel").GetComponent<RectTransform>();
        GenerateContracts(contractCount);
    }

    private void OnGoBackClicked(BaseEventData data)
    {
        _mainMenuScene.GoToCareer();
    }

    public void GenerateContracts(uint count)
    {
        var contracts = ContractManager.GenerateContracts(count);
        foreach (var contract in contracts)
        {
            var contractItem = Instantiate(_contractPrefab, _contractPanel);
            var contractItemScript = contractItem.GetComponent<ContractItem>();
            var rect = _contractPanel.rect;
            var x_min = rect.xMin + borderShift;
            var x_max = rect.xMax - borderShift;
            var y_min = rect.yMin + borderShift;
            var y_max = rect.yMax - borderShift;
            var x = Random.Range(x_min, x_max);
            var y = Random.Range(y_min, y_max);
            contractItemScript.SetContract(contract);
            contractItemScript.SetPosition(new Vector2(x, y));
            _contractItems.Add(contractItemScript);
        }
    }
}
