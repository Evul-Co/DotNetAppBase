using System;
using System.Linq.Expressions;
using DotNetAppBase.Std.Library;

// ReSharper disable CheckNamespace
public static class XExpressionExtensions
// ReSharper restore CheckNamespace
{
    public static string GetMemberName<T>(this T obj, Expression<Func<T, object>> expression) => XHelper.Expressions.GetMemberName(expression);

    public static string GetMemberName<T>(this Expression<Func<T, object>> expression) => XHelper.Expressions.GetMemberName(expression);

    public static string GetMemberName(this LambdaExpression expression) => XHelper.Expressions.GetMemberName(expression);
}