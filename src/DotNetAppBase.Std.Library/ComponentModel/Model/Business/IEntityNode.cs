using System.Collections.Generic;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Business;

public interface IEntityNode
{
    IEnumerable<IEntityNode> Children { get; }

    bool IsParent { get; }
    string Name { get; }

    IEntityNode Parent { get; }
}