using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.DomainBase
{
    [Serializable]
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string message) : base(message)
        {            
        }
    }
}
