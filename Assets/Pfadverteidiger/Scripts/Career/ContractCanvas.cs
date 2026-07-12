using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class ContractCanvas : MonoBehaviour
{
    private CareerScene _careerScene;
    private RectTransform _contractPanel;
    private List<ContractItem> _contractItems = new List<ContractItem>();
    private Button _goBack;
    private GameObject _contractItemPrefab;
    public uint contractCount = 5; // Number of contracts to generate
    public float borderShift = 10f; // Shift from the border to avoid overlap
    
    void Awake()
    {
        _goBack = transform.Find("Back").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_goBack, EventTriggerType.PointerClick, (data) => OnGoBackClicked(data));
    }

    public void Initialize(CareerScene careerScene)
    {
        _careerScene = careerScene;
        _contractPanel = transform.Find("ContractPanel").GetComponent<RectTransform>();
        _contractItemPrefab = Resources.Load<GameObject>("Prefabs/Career/ContractItem");
        GenerateContracts(contractCount);
    }

    private void OnGoBackClicked(BaseEventData data)
    {
        _careerScene.GoToCareer();
    }

    private void GenerateContracts(uint count = 5)
    {
        var contracts = ContractManager.GetContracts(count);
        foreach (var contract in contracts)
        {
            var rect = _contractPanel.rect;
            var x_min = rect.xMin + borderShift;
            var x_max = rect.xMax - borderShift;
            var y_min = rect.yMin + borderShift;
            var y_max = rect.yMax - borderShift;
            var x = Random.Range(x_min, x_max);
            var y = Random.Range(y_min, y_max);

            var contractItem = Instantiate(_contractItemPrefab, _contractPanel);
            var contractItemScript = contractItem.GetComponent<ContractItem>();
            contractItemScript.Initialize(contract, new Vector2(x, y));
            _contractItems.Add(contractItemScript);
        }
    }
}
