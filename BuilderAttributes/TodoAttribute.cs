using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuilderAttributes
{
    [AttributeUsage(AttributeTargets.Method| AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class TodoAttribute : Attribute
    {
        public TodoAttribute([CallerFilePath]string path = "", [CallerLineNumber] int line = 0)
        {
            if (Debugger.IsAttached)
            {
                Debug.Fail("文件:" + path + ",在第" + line.ToString() + "未完成");
                Debugger.Break();
            }
        }

    }
}
