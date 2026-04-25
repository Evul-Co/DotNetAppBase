using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using DotNetAppBase.Std.Library.Data.Udt;
#if NETFRAMEWORK
using System.Data.SqlClient;

#else
using Microsoft.Data.SqlClient;
#endif

// ReSharper disable CheckNamespace
public static class XSqlParameterExtensions
// ReSharper restore CheckNamespace
{
    public static SqlCommand AddParameters(this SqlCommand cmd, params SqlParameter[] args)
    {
        cmd.Parameters.AddRange(args);

        return cmd;
    }

    public static SqlParameterCollection AddParameters(this SqlParameterCollection parameters, params SqlParameter[] args)
    {
        parameters.AddRange(args);

        return parameters;
    }

    public static SqlParameter AsOutput(this SqlParameter parameter)
    {
        parameter.Direction = ParameterDirection.Output;

        return parameter;
    }

    public static SqlParameter AsReturnValue(this SqlParameter parameter)
    {
        parameter.Direction = ParameterDirection.ReturnValue;

        return parameter;
    }

    public static SqlXml AsSqlXml(this string content) => new(XmlReader.Create(new StringReader(content)));

    [Localizable(false)]
    public static SqlParameter Set(this SqlParameter parameter, string name, SqlDbType type, object value, bool setDbNullOnNullValue = true)
    {
        parameter.ParameterName = name;
        parameter.SqlDbType = type;
        parameter.Value = setDbNullOnNullValue && Equals(value, null) ? DBNull.Value : value;

        return parameter;
    }

    public static SqlParameter SetAsUdt(this SqlParameter parameter, string name, object value)
    {
        switch (value)
        {
            case DataTable table:
                parameter.Set(name, SqlDbType.Structured, table);
                parameter.TypeName = table.TableName;
                break;

            case UdtBase udtBase:
                parameter.Set(name, SqlDbType.Structured, udtBase.Table);
                parameter.TypeName = udtBase.Table.TableName;
                break;

            default:
                parameter.Set(name, SqlDbType.Udt, value);
                parameter.TypeName = $"dbo.{value.GetType().Name}";
                break;
        }

        return parameter;
    }

    public static SqlParameter SetAsXml(this SqlParameter parameter, string name, string value)
    {
        parameter.Set(name, SqlDbType.Xml, value.AsSqlXml());

        return parameter;
    }
}