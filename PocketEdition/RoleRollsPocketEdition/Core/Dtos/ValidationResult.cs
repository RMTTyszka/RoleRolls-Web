namespace RoleRollsPocketEdition.Core.Dtos
{
    public class ValidationResult<TResult> where TResult : enum
    {
        public TResult Result { get; set; }
        public string Message { get; set; }
    }
}
