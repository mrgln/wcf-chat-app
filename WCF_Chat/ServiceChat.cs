using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Chat
{
    
    [ServiceBehavior(InstanceContextMode= InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                UserName = name,
                operationContext = OperationContext.Current,
            };
            nextId++;

            SendMessage(": " + user.UserName + " подключился!",0);
            users.Add(user);

            return user.ID;
        }

        public void Disconnect(int identificator)
        {
            var user = users.FirstOrDefault(i => i.ID == identificator); //поиск usera
            if(user != null)
            {
                users.Remove(user);
                SendMessage(": "+user.UserName + " покинул чат",0);

            }
        }


        public void SendMessage(string message, int identificator)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(i => i.ID == identificator); 
                if (user != null)
                {
                    answer += ": " + user.UserName + " ";
                }
                answer += message;
                item.operationContext.GetCallbackChannel<IServerChatCallback>().MessageCallBack(answer);
            }
        }
    }
}
