using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace DotNetAppBase.Std.Db;

public partial class DbAccess
{
    [Localizable(false)]
    public static class Actions
    {
        public static T ExecuteProc<T>(string procName, params DbParameter[] parameters)
        {
            using var access = new DbAccess();

            return access.ExecProc<T, DbParameter>(procName, parameters);
        }

        public static int ExecuteProc(string procName, params DbParameter[] parameters)
        {
            using var access = new DbAccess();

            return access.ExecProc(procName, parameters);
        }

        public static T ExecuteProc<T>(DbSession session, string procName, params DbParameter[] parameters)
        {
            using var access = new DbAccess();
            access.Session = session;

            return access.ExecProc<T, DbParameter>(procName, parameters);
        }

        public static void ExecuteProcAndFill(DataTable dataTable, string procName, params DbParameter[] parameters)
        {
            using var access = new DbAccess();

            access.ExecProcAndFill(dataTable, procName, parameters);
        }

        public static void ExecuteProcAndFill(DbSession session, DataTable dataTable, string procName, params DbParameter[] parameters)
        {
            using var access = new DbAccess();
            access.Session = session;

            access.ExecProcAndFill(dataTable, procName, parameters);
        }

        public static int ExecuteText(string sql, params DbParameter[] parameters)
        {
            using var access = new DbAccess();

            return access.ExecText(sql, parameters);
        }

        public static T ExecuteText<T>(string sql, params DbParameter[] parameters)
        {
            return ExecuteText<T>(sql, CommandBehavior.Default, parameters);
        }

        public static T ExecuteText<T>(string sql, CommandBehavior behavior, params DbParameter[] parameters)
        {
            using var access = new DbAccess();

            return access.ExecText<T, DbParameter>(sql, behavior, parameters);
        }

        public static T ExecuteText<T>(DbSession session, string sql, params DbParameter[] parameters)
        {
            return ExecuteText<T>(session, sql, CommandBehavior.Default, parameters);
        }

        public static T ExecuteText<T>(DbSession session, string sql, CommandBehavior behavior, params DbParameter[] parameters)
        {
            using var access = new DbAccess();
            access.Session = session;

            return access.ExecText<T, DbParameter>(sql, behavior, parameters);
        }
    }
}