using System;

namespace DotNetAppBase.Std.Library.ComponentModel.Types;

public class XTypeDescriptor<TModel> : XTypeDescriptor, IXTypeDescriptor<TModel>
{
    protected internal XTypeDescriptor() : this(typeof(TModel)) { }
    protected XTypeDescriptor(Type type) : base(type) { }
}