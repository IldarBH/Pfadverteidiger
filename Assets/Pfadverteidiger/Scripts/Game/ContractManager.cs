using System.Collections.Generic;

public class ContractData
{
    public string Name { get; set; }
    public List<ThreatGroup> ThreatGroups { get; set; }
}

public static class ContractManager
{
    private static List<ContractData> _contracts = new List<ContractData>();

    private static List<ContractData> GenerateContracts(uint count = 5)
    {
        _contracts.Clear();
        for (int i = _contracts.Count; i < count; i++)
        {
            ContractData contract = new ContractData();
            contract.Name = $"Contract {i + 1}";
            contract.ThreatGroups = new List<ThreatGroup>{ ThreatGroup.CosmicHazard };
            _contracts.Add(contract);
        }
        return _contracts;
    }

    public static List<ContractData> GetContracts(uint count = 5)
    {
        if (_contracts.Count < count)
        {
            return GenerateContracts(count);
        }
        return _contracts;
    }
}
