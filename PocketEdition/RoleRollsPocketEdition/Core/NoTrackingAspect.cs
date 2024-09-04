using Microsoft.EntityFrameworkCore;
using PostSharp.Aspects;

namespace RoleRollsPocketEdition.Core;

[Serializable]
public class NoTrackingAspect : OnMethodBoundaryAspect
{
    public override void OnEntry(MethodExecutionArgs args)
    {
        // Adicione lógica para manipular o DbContext
        var dbContext = args.Arguments.OfType<DbContext>().FirstOrDefault();
        if (dbContext != null)
        {
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        base.OnEntry(args);
    }        
}