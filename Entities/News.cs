using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MongoDAL.Entities
{
    [DataContract]
    public class News: EntityBase
    {
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        public string Text { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }
    }
}
