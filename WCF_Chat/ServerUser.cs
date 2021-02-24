using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCF_Chat
{
    public class ServerUser
    {
        public int ID { get; set; }

        public string UserName{ get; set; }

        public OperationContext operationContext { get; set; }
    }
}
