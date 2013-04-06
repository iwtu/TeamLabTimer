using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamLab
{
    interface IAPI
    {
        T execute<T>(string resource, REST.METHOD method, Dictionary<string, string> body);
    }
}
