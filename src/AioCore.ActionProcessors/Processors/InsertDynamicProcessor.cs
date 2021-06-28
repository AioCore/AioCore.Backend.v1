using AioCore.Application.ActionProcessors;
using AioCore.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.ActionProcessors.Processors
{
    public class XXXXXDynamicProcessor : IActionProcessor
    {
        public StepType StepType => StepType.Update;

        public async Task<Dictionary<string, object>> ExecuteAsync(ActionParamModel actionParam, CancellationToken cancellationToken)
        {
            if(actionParam.InitParamType == InitParamType.FormValue)
            {
            }


            var typeId = actionParam.TargetTypeId;
            var attribute = actionParam.TargetAttribute;
           
            
            var data = actionParam.Data;
            
            return null;
        }
    }
}
