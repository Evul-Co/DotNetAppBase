using System;

namespace DotNetAppBase.Std.Db.Contract;

public interface IDbDateTimeProvider
{
    DateTime GetDateTime();
}