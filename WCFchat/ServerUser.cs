using System.ServiceModel;

namespace WCFchat
{
    public class ServerUser
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public OperationContext OperationContext { get; set; }
    }
}
