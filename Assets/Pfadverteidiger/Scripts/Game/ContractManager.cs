using System.Collections.Generic;

public class Contract
{
    public string Name { get; set; }
    public List<ThreatGroup> ThreatGroups { get; set; }
}

public static class ContractManager
{
    private static List<Contract> _contracts = new List<Contract>();

    public static List<Contract> GenerateContracts(uint count = 5)
    {
        _contracts.Clear();
        for (int i = 0; i < count; i++)
        {
            Contract contract = new Contract();
            contract.Name = $"Contract {i + 1}";
            contract.ThreatGroups = new List<ThreatGroup>{ ThreatGroup.CosmicHazard };
            _contracts.Add(contract);
        }
        return _contracts;
    }

    public static List<Contract> GetContracts()
    {
        return _contracts;
    }
}
