using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchestrA.GRAccess;


namespace GalaxyBackup
{
    class GalaxyConn
    {
        private string _GalaxyName { get; set; }
        private string _UserName { get; set; }
        private string _Password { get; set; }
        public IGalaxy Galaxy { get; set; }
        public GalaxyConn(string galaxyName, string userName, string password)
        {
            _GalaxyName = galaxyName;
            _UserName = userName;
            _Password = password;
        }

        public void Connect(GRAccessApp grAccess)
        {

            string nodeName = Environment.MachineName;
            Console.WriteLine($"Machine: {nodeName}");
            Console.WriteLine($"Connecting to galaxy: {_GalaxyName}");

            IGalaxies gals = grAccess.QueryGalaxies(nodeName);
            if (gals == null || grAccess.CommandResult.Successful == false)
            {
                Console.WriteLine(grAccess.CommandResult.CustomMessage + grAccess.CommandResult.Text);
            }

            IGalaxy galaxy = gals[_GalaxyName];

            ICommandResult cmd;

            galaxy.Login(_UserName, _Password);
            cmd = galaxy.CommandResult;

            if (!cmd.Successful)
            {
                Console.WriteLine($"Login to galaxy {_GalaxyName} Failed :" +
                                cmd.Text + " : " +
                                cmd.CustomMessage);
            }
            else
            {
                Console.WriteLine($"Login {_GalaxyName} successful");
            }

            Galaxy = galaxy;
        }
    }
}
