using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ticket4S.Utils
{
    public static class ValidatorHelper
    {
        public static void ThrowesIfNotValid(string paramName, object objectToValidate)
        {
            if(!TryValidate(objectToValidate))
                throw new ArgumentException("Validacao dos dados do Paramentro Invalida.", paramName); //TODO: Validar melhor, Informar detalhes do erro na mensagem //TODO: Fix Typo
        }

        public static bool TryValidate(object @object)
        {
            ICollection<ValidationResult> results;
            TryValidate(@object, out results);
            return !results.Any();
        }

        public static bool TryValidate(object @object, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(@object, serviceProvider: null, items: null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(@object, context, results, validateAllProperties: true
            );
        }
    }
}
