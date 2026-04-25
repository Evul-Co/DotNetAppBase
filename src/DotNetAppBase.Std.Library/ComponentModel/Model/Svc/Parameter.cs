namespace DotNetAppBase.Std.Library.ComponentModel.Model.Svc;

public class Parameter<TData, TParam1>
{
    public TData Data { get; set; }

    public TParam1 Param1 { get; set; }
}

public class Parameter<TData, TParam1, TParam2>
{
    public TData Data { get; set; }

    public TParam1 Param1 { get; set; }

    public TParam2 Param2 { get; set; }
}

public class Parameter<TData, TParam1, TParam2, TParam3>
{
    public TData Data { get; set; }

    public TParam1 Param1 { get; set; }

    public TParam2 Param2 { get; set; }

    public TParam3 Param3 { get; set; }
}