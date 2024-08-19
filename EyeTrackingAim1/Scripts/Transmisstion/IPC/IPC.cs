using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace EyeTrackingAim1.Scripts.Transmisstion.IPC
{
    public class IPC
    {
        public class IPC_Data : MarshalByRefObject
        {
            public System.Windows.Vector vector;

            
        }

        IpcServerChannel Server_Channel;
        IPC_Data TestObj;
        public void Register_Server_Chanel()
        {
            TestObj = new IPC_Data();
            Server_Channel = new IpcServerChannel("IpcTest");
            ChannelServices.RegisterChannel(Server_Channel, true);
        }

        public void send_Data(System.Windows.Vector vector_)
        {
            TestObj.vector = vector_;
            RemotingServices.Marshal(TestObj, "test", typeof(IPC_Data));
        }
    }
}
