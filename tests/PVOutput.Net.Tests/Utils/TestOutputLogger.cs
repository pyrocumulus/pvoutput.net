using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PVOutput.Net.Tests.Utils
{
    internal class TestOutputLogger : ILogger<PVOutputClient>, IDisposable
    {
        private readonly Stack<string> _scopeStack = new Stack<string>();

        private void AppendLog(string line) => Console.WriteLine(line);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            AppendLog(formatter(state, exception));
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable BeginScope<TState>(TState state)
        {
            string stackText = StateToString(state);
            _scopeStack.Push(stackText);
            AppendLog("BeginScope: " + stackText);
            return this;
        }

        private static string StateToString<TState>(TState state)
        {
            if (state is Dictionary<string, object> dictionary)
            {
                var sb = new StringBuilder();
                foreach (KeyValuePair<string, object> kvp in dictionary)
                {
                    sb.Append('[');
                    sb.Append(kvp.Key);
                    sb.Append(';');
                    sb.Append(kvp.Value == null ? "<NULL>" : kvp.Value.ToString());
                    sb.Append("],");
                }
                return sb.ToString().TrimEnd(',');
            }
            return state.ToString();
        }

        public void Dispose()
        {
            var stackText = _scopeStack.Pop();
            AppendLog("EndScope: " + stackText);
        }
    }
}
