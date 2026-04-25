namespace DotNetAppBase.Std.Library.ComponentModel.Model.Business;

public interface IEntityWorkWithConcurrency : IEntity
{
    byte[] RowVersion { get; set; }
}