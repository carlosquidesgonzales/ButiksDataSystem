using System;
using System.Collections.Generic;
using System.Text;

namespace ButiksDataSystem.Exeptions
{
    public class EntityException: Exception
    {
        public EntityException(string message, Exception innerException): base(message, innerException)
        {          
        }
    }
}
