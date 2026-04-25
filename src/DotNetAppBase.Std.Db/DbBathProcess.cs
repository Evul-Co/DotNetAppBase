using System;
using System.Text;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Db;

public class DbBathProcess
{
    private readonly StringBuilder _buffer;
    private readonly int _maxLengthParameter;
    private readonly Action<string> _methodExecute;

    private readonly object _objSync = new object();
    private readonly char _parameterSeparator;

    public DbBathProcess(int maxLengthParameter, char parameterSeparator, Action<string> methodExecute)
    {
        _maxLengthParameter = maxLengthParameter;
        _parameterSeparator = parameterSeparator;
        _methodExecute = methodExecute;

        _buffer = new StringBuilder();
    }

    public void AddParameterPart(string parameterPart)
    {
        if (string.IsNullOrEmpty(parameterPart))
        {
            throw new XException("Não é possível adicionar um parâmetro parcial nulo ou vazio.");
        }

        lock (_objSync)
        {
            if (_buffer.Length + parameterPart.Length > _maxLengthParameter)
            {
                _methodExecute(_buffer.ToString());
                _buffer.Length = 0;
            }

            _buffer.Append(parameterPart + _parameterSeparator);
        }
    }

    public void Flush()
    {
        lock (_objSync)
        {
            if (_buffer.Length <= 0)
            {
                return;
            }

            _methodExecute(_buffer.ToString());
            _buffer.Length = 0;
        }
    }
}