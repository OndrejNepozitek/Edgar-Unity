using System.Collections.Generic;
using Edgar.Unity.Diagnostics;

namespace Edgar.Unity
{
    public class TimeoutException : GeneratorException
    {
        public List<IDiagnosticResult> DiagnosticResults { get; set; }

        public TimeoutException() : base($"The generator was not able to produce a level within a given time limit. Please see the console above for additional diagnostic information.")
        {
            /* empty */
        }
    }
}