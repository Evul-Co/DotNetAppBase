using System.Threading.Tasks;

// ReSharper disable CheckNamespace
public static class XTaskExtensions
// ReSharper restore CheckNamespace
{
    public static T ReadValue<T>(this Task<T> task) => task.Result;
}