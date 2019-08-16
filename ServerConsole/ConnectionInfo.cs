using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

namespace ServerTestApp
{
    public class ConnectionInfo
    {
        public Connection connection;
        public String username;

        public ConnectionInfo(Connection conn)
        {
            connection = conn;
            username = "";
        }

        public ConnectionInfo(Connection conn, String user)
        {
            connection = conn;
            username = user;
        }
    }
}


