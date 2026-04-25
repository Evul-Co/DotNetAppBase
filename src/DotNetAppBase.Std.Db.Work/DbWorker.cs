using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
#if NETFRAMEWORK
using System.Data.SqlClient;

#else
using Microsoft.Data.SqlClient;
#endif

// ReSharper disable UnusedMember.Global

namespace DotNetAppBase.Std.Db.Work;

[Localizable(false)]
public class DbWorker
{
    private static string _connectionString;

    private readonly string _myConnectionString;

    public DbWorker(int? usuID = null)
    {
        _myConnectionString = _connectionString.Replace("{usuID}", usuID == null ? string.Empty : $"_USUID_{usuID.Value}");
    }

    public Task<DbCollection<TModel>> AsyncCollectionSp<TModel>(string storedProc, params SqlParameter[] parameters) where TModel : DbEntity, new()
    {
        return Task.Run(() => CollectionSp<TModel>(storedProc, parameters));
    }

    public Task<DataSet> AsyncDataSetSp(string storedProc, params SqlParameter[] parameters)
    {
        return Task.Run(() => DataSetSp(storedProc, parameters));
    }

    public Task<TModel> AsyncEntitySp<TModel>(string storedProc, params SqlParameter[] parameters) where TModel : DbEntity, new()
    {
        return Task.Run(() => EntitySp<TModel>(storedProc, parameters));
    }

    public DbCollection<TModel> Collection<TModel>(string commandText, params SqlParameter[] parameters) where TModel : DbEntity, new()
    {
        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = commandText;
        comm.Parameters.AddRange(parameters);

        var table = new DataTable();

        using (var adapter = new SqlDataAdapter(comm))
        {
            adapter.Fill(table);
        }

        return new DbCollection<TModel>(table);
    }

    public DbCollection<TModel> CollectionSp<TModel>(string storedProc, params SqlParameter[] parameters) where TModel : DbEntity, new()
    {
        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = storedProc;
        comm.CommandType = CommandType.StoredProcedure;

        comm.Parameters.AddRange(parameters);

        var table = new DataTable();

        using (var adapter = new SqlDataAdapter(comm))
        {
            adapter.Fill(table);
        }

        return new DbCollection<TModel>(table);
    }

    public static void Configure(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void DataReader(string commandText, Action<DbDataReader> processAction, params SqlParameter[] parameters)
    {
        if (processAction == null)
        {
            throw new ArgumentNullException(nameof(processAction));
        }

        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        comm.CommandText = commandText;
        comm.Parameters.AddRange(parameters);

        var reader = comm.ExecuteReader();
        while (reader.NextResult())
        {
            processAction(reader);
        }
    }

    public IEnumerable<T> DataReader<T>(string commandText, Func<SqlDataReader, T> processAction, params SqlParameter[] parameters)
    {
        if (processAction == null)
        {
            throw new ArgumentNullException(nameof(processAction));
        }

        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = commandText;
        comm.Parameters.AddRange(parameters);

        var reader = comm.ExecuteReader();
        while (reader.Read())
        {
            yield return processAction(reader);
        }
    }

    public void DataReaderSp(string storedProc, Action<DbDataReader> processAction, params SqlParameter[] parameters)
    {
        if (processAction == null)
        {
            throw new ArgumentNullException(nameof(processAction));
        }

        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        comm.CommandText = storedProc;
        comm.CommandType = CommandType.StoredProcedure;

        comm.Parameters.AddRange(parameters);

        var reader = comm.ExecuteReader();
        while (reader.NextResult())
        {
            processAction(reader);
        }
    }

    public IEnumerable<T> DataReaderSp<T>(string storedProc, Func<SqlDataReader, T> processAction, params SqlParameter[] parameters)
    {
        if (processAction == null)
        {
            throw new ArgumentNullException(nameof(processAction));
        }

        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = storedProc;
        comm.CommandType = CommandType.StoredProcedure;

        comm.Parameters.AddRange(parameters);

        var reader = comm.ExecuteReader();
        while (reader.Read())
        {
            yield return processAction(reader);
        }
    }

    public DataSet DataSet(string commandText, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = commandText;
        comm.CommandType = CommandType.Text;

        comm.Parameters.AddRange(parameters);

        var set = new DataSet();

        using var adapter = new SqlDataAdapter(comm);
        adapter.Fill(set);

        return set;
    }

    public DataSet DataSetSp(string storedProc, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = storedProc;
        comm.CommandType = CommandType.StoredProcedure;

        comm.Parameters.AddRange(parameters);

        var set = new DataSet();

        using var adapter = new SqlDataAdapter(comm);
        adapter.Fill(set);

        return set;
    }

    public DataTable DataTable(string commandText, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = commandText;
        comm.CommandType = CommandType.Text;

        comm.Parameters.AddRange(parameters);

        var table = new DataTable();

        using var adapter = new SqlDataAdapter(comm);
        adapter.Fill(table);

        return table;
    }

    public DataTable DataTableSp(string storedProc, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = storedProc;
        comm.CommandType = CommandType.StoredProcedure;

        comm.Parameters.AddRange(parameters);

        var table = new DataTable();

        using var adapter = new SqlDataAdapter(comm);
        adapter.Fill(table);

        return table;
    }

    public TEntity Entity<TEntity>(string storedProc, params SqlParameter[] parameters) where TEntity : DbEntity, new() => Collection<TEntity>(storedProc, parameters).FirstOrDefault();

    public TEntity EntitySp<TEntity>(string storedProc, params SqlParameter[] parameters) where TEntity : DbEntity, new() => CollectionSp<TEntity>(storedProc, parameters).FirstOrDefault();

    public int NonQuery(string commandText, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = commandText;
        comm.Parameters.AddRange(parameters);

        return comm.ExecuteNonQuery();
    }

    public int NonQuerySp(string storedProc, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = storedProc;
        comm.CommandType = CommandType.StoredProcedure;

        comm.Parameters.AddRange(parameters);

        return comm.ExecuteNonQuery();
    }

    public TScalar Scalar<TScalar>(string commandText, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = commandText;
        comm.Parameters.AddRange(parameters);

        return (TScalar) comm.ExecuteScalar();
    }

    public TScalar ScalarSp<TScalar>(string storedProc, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(_myConnectionString);
        using var comm = conn.CreateCommand();
        conn.Open();

        comm.CommandText = storedProc;
        comm.CommandType = CommandType.StoredProcedure;

        comm.Parameters.AddRange(parameters);

        return (TScalar) comm.ExecuteScalar();
    }
}