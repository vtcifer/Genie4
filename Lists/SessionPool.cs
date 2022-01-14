using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenieClient
{
    public class SessionPool : Genie.Collections.ThreadedDictionary<Guid, Session>
    {
        private static SessionPool _instance;
        private int _lastSessionID;

        public static SessionPool Instance
        {
            get
            {
                if(_instance is null)
                {
                    _instance = new SessionPool();
                }
                return _instance;
            }
        }
        private SessionPool() { }

        public Session CreateSession()
        {
            Session newSession = new Session();
            Add(newSession.SessionID, newSession);
            return newSession;
        }

    }
}
