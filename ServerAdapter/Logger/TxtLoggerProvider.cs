using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAdapter.Logger
{
    public class TxtLoggerProvider : ILoggerProvider
    {
        private string path;
        public TxtLoggerProvider(string _path)
        {
            path = _path;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TxtLogger(path);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
