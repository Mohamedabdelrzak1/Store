using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DeliveryMethodNotFoundExceptions(int id) : NotFoundExceptions($"Delivery Method With Id {id} Not Found !!")
    {
    }
}
