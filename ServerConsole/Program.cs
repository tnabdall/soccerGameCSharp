using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

namespace ServerTestApp
{
    class Program
    {
        private static ConnectionInfo shooter;
        private static ConnectionInfo keeper;
        private static List<ConnectionInfo> spectatorConnections = new List<ConnectionInfo>();

        static void Main(string[] args)
        {
            //Trigger the method PrintIncomingMessage when a packet of type 'Message' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("RequestConnectionInfo", HandleConnectionInfoRequest);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("ConnectionRequest", HandleConnectionRequest);

            // Close connection and open connection array for new connection
            NetworkComms.AppendGlobalConnectionCloseHandler(closeConnection);
            //Start listening for incoming connections
            Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0));

            //Print out the IPs and ports we are now listening on
            Console.WriteLine("Server listening for TCP connection on:");
            foreach (System.Net.IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
                Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);

            //Let the user close the server
            Console.WriteLine("\nPress any key to close server.");
            Console.ReadKey(true);

            //We have used NetworkComms so we should ensure that we correctly call shutdown
            NetworkComms.Shutdown();
        }

        private static void HandleConnectionInfoRequest(PacketHeader header, Connection connection, string message)
        {
            String sendingMessage = "";
            if (keeper == null)
            {
                sendingMessage += "k1";
            }
            else
            {
                sendingMessage += "k0";
            }
            if (shooter == null)
            {
                sendingMessage += "s1";
            }
            else
            {
                sendingMessage += "s0";
            }
            connection.SendObject("connectionInfo",sendingMessage);
        }

        private static void HandleConnectionRequest(PacketHeader header, Connection connection, string message)
        {
            if(message=="keeper" && keeper == null)
            {
                keeper = new ConnectionInfo(connection);
                connection.SendObject("success", "keeper");
            }
            else if (message == "keeper")
            {
                connection.SendObject("error", "Keeper slot is taken.");
            }
            else if (message == "shooter" && shooter == null)
            {
                shooter = new ConnectionInfo(connection);
                connection.SendObject("success", "shooter");
            }
            else if (message == "shooter")
            {
                connection.SendObject("error", "Shooter slot is taken.");
            }
        }

        /// <summary>
        /// Writes the provided message to the console window
        /// </summary>
        /// <param name="header">The packet header associated with the incoming message</param>
        /// <param name="connection">The connection used by the incoming message</param>
        /// <param name="message">The message to be printed to the console</param>
        private static void PrintIncomingMessage(PacketHeader header, Connection connection, string message)
        {
            int connectionIndex = -1;
            if (spectatorConnections.Count == 0)
            {
                spectatorConnections.Add(new ConnectionInfo(connection, message));
                Console.WriteLine("\nA message was received from " + connection.ToString() + " which said '" + message + "'. Set as connection 0");
                connectionIndex = 0;
            }
            else
            {
                Boolean newUser = true;
                for (int i = 0; i < spectatorConnections.Count; i++)
                {
                    if (spectatorConnections[i].connection.ToString().Equals(connection.ToString()))
                    {
                        newUser = false;
                        connectionIndex = i;
                    }
                }
                if (newUser)
                {
                    spectatorConnections.Add(new ConnectionInfo(connection, message));
                    Console.WriteLine("\nA message was received from " + connection.ToString() + " which said '" + message + "'. Set as connection 0");
                    connectionIndex = spectatorConnections.Count - 1;
                }
            }

            if (connectionIndex != -1 && spectatorConnections.Count > 1)
            {
                for (int i = 0; i < spectatorConnections.Count; i++)
                {
                    if (i != connectionIndex)
                    {
                        spectatorConnections[i].connection.SendObject("Message", spectatorConnections[connectionIndex].username + ": " + message);
                    }
                }
                Console.WriteLine("\nSent message from " + spectatorConnections[connectionIndex].ToString() + " which said '" + message);
            }

        }

        private static void closeConnection(Connection connection)
        {
            for (int i = 0; i < spectatorConnections.Count; i++)
            {
                if (spectatorConnections[i].connection.ToString().Equals(connection.ToString()))
                {
                    spectatorConnections.RemoveAt(i);
                }
            }

        }
    }

}
