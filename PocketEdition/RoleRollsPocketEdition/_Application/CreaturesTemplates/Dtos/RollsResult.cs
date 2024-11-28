namespace RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos
{
    public class RollsResult
    {
        public List<int> Rolls { get; set; }
        public int Successes { get; set; }
        public int CriticalSuccesses { get; set; }
        public int Misses { get; set; }
        public int CriticalMisses { get; set; }
        public int? Dificulty { get; set; }
        public int? Complexity { get; set; }
        public bool Success { get; set; }
        public int SuccessTimes { get; set; }
    }
    public class RollCheck 
    {
        public int? Dificulty { get; set; }
        public int? Complexity { get; set; }
        public int Bonus { get; set; }
    }
}
