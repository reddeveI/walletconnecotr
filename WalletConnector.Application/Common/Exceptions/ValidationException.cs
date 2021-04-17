using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WalletConnector.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
        
        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var (key, errors) in Errors)
            {
                builder.AppendLine(key);
                foreach (var s in errors)
                {
                    builder.Append("    ").AppendLine(s);
                }
            }

            return builder.ToString();
        }
    }
}
