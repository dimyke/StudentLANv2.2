using System;
using System.Data.Entity.Validation;
using System.Runtime.Serialization;

namespace DAL
{
    internal class FormattedDbEntityValidationException : Exception
    {
        private DbEntityValidationException e;

        public FormattedDbEntityValidationException()
        {
        }

        public FormattedDbEntityValidationException(string message) : base(message)
        {
        }

        public FormattedDbEntityValidationException(DbEntityValidationException e)
        {
            this.e = e;
        }

        public FormattedDbEntityValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FormattedDbEntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}