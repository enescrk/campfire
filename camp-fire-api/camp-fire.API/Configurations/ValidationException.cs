// using FluentValidation.Results;

// namespace HilfeOhneGrenzen.API.Configurations;

// public class ValidationException : Exception
// {
//     public List<string> Failures { get; }

//     public ValidationException()
//         : base("One or more validation failures have occurred.")
//     {
//         Failures = new List<string>();
//     }

//     public ValidationException(List<ValidationFailure> failures) : this()
//     {
//         var propertyNames = failures
//             .Select(e => e.PropertyName)
//             .Distinct();

//         foreach (var propertyName in propertyNames)
//         {
//             var propertyFailures = failures
//                 .Where(e => e.PropertyName == propertyName)
//                 .Select(e => e.ErrorMessage)
//                 .ToArray();

//             // if (propertyFailures.Count() > 1)
//             //     Failures.Add($"{propertyName}: {string.Join(", ", propertyFailures)}");
//             // else
//                 Failures.Add($"{string.Join(", ", propertyFailures)}");
//         }
//     }
// }
