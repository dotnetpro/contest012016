using System;
using contest.submission.contract;

namespace contest.submission
{
 
  [Serializable]
  public class Solution : IDnp1601Solution
  {
    string text;
    public void Start(string text)
    {
      this.text = text;
    }

    public void Process(string firstrelative, string secondrelative)
    {
      Result("Schwippschwapp");
    }

    public event Action<string> Result;
  }
}
