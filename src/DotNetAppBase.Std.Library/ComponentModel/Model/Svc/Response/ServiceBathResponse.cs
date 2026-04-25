using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Svc;

[DataContract, Serializable]
public class ServiceBathResponse<TEntityID> : ServiceResponse
{
    private readonly Result[] _results;

    public ServiceBathResponse(IEnumerable<Result> results)
    {
        _results = results.ToArrayEfficient();
    }

    public int CountFailed => _results.Count(r => r.Status == EServiceResponse.Failed);

    public int CountSuccess => _results.Count(r => r.Status == EServiceResponse.Succeeded);

    [DataMember]
    public IEnumerable<Result> Results => _results;

    public class Result
    {
        public Result(TEntityID entityID, string detail, EServiceResponse status)
        {
            EntityID = entityID;
            Detail = detail;
            Status = status;
        }

        public string Detail { get; }

        public TEntityID EntityID { get; }

        public EServiceResponse Status { get; }
    }
}