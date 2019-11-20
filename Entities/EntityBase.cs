using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MongoDAL.Entities
{
    [DataContract]
    public abstract class EntityBase
    {
        [DataMember]
        public string Id { get; set; }
    }
}
