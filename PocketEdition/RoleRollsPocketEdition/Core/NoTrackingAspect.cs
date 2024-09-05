using Microsoft.EntityFrameworkCore;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace RoleRollsPocketEdition.Core;

[PSerializable]
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