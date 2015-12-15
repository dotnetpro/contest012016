using System;

namespace contest.submission.contract
{
    public interface IDnp1601Solution
    {
      void Start(string text);
      void Process(string firstrelative, string secondrelative);

      event Action<string> Result;
    }
}
