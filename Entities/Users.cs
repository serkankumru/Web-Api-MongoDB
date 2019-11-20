using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MongoDAL.Entities
{
    public class Users:EntityBase
    {
        public string Name { get; set; }

        public string Password { get; set; }
    }
}
