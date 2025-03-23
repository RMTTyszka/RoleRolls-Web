namespace RoleRollsPocketEdition.Core.Dtos
{
    public class ValidationResult<TResult>
    {
        public TResult Result { get; set; }
        public string Message { get; set; }
    }
}