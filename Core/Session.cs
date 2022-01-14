using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenieClient.Genie;



namespace GenieClient
{
    public class Session
    {
        private Guid _sessionID;
        private Globals _globals;
        private Game _game;
        private Command _command;
        private Mapper.AutoMapper _mapper;
        
        
        public Guid SessionID { get { return _sessionID; } }
        public Globals Globals { get { return _globals; } set { _globals = value; } }
        public Game Game { get { return _game; } set { _game = value;  } }
        public Command Command { get { return _command; } set { _command = value; } }
        public Mapper.AutoMapper Mapper { get { return _mapper; } set { _mapper = value; } }
        public Session()
        {
            _sessionID = Guid.NewGuid();
            _globals = new Genie.Globals();
            _game = new Genie.Game(ref _globals) ;
            _command = new Genie.Command(ref _globals);
            _mapper = new Mapper.AutoMapper(ref _globals);
        }
    }
}
