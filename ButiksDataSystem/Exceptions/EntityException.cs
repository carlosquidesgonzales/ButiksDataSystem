using System;

namespace ButiksDataSystem.Exeptions
{
    public class EntityException: Exception
    {
        public EntityException(string message, Exception innerException): base(message, innerException)
        {          
        }
    }
}
