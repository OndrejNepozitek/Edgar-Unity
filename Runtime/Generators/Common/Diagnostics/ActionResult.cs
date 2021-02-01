using System.Collections.Generic;

namespace Edgar.Unity.Diagnostics
{
    public class ActionResult
    {
        public bool IsSuccessful => !HasErrors;

        public bool HasErrors => Errors.Count > 0;

        public List<string> Errors { get; } = new List<string>();

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            Errors.AddRange(errors);
        }
    }
}