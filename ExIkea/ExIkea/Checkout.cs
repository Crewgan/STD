using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExIkea
{
    class Checkout
    {
        static Size size = new Size(50, 50);
        List<Client> clients;
        private PointF _location;
        bool isOpen;
        int maxNumberClients;
        Brush color;
        PointF _lastLocation;

        public PointF Location { get => new PointF(_location.X, _location.Y); }
        public static Size Size { get => size;}
        public PointF LastLocation { get => _lastLocation; }

        public Checkout(bool isOpen, int maxNumberClients, Point location)
        {
            this.isOpen = isOpen;
            this.maxNumberClients = maxNumberClients;
            this._location = location;
            _lastLocation = location;

            clients = new List<Client>();
        }
        public void Paint(Graphics g)
        {
            if (isOpen)
                color = Brushes.Blue;
            else
                color = Brushes.Red;

            g.FillRectangle(color, new RectangleF(_location, size));
        }

        public bool NewClient(Client client)
        {
            if (clients.Count < maxNumberClients)
            {
                
                clients.Add(client);
                _lastLocation = new PointF(Location.X, _lastLocation.Y - 50);
                return true;
            }
            return false;
        }

        public bool CheckingOut(Client client)
        {
            return client.Equals(clients[0]);
        }

        public void ClientDone(Client client)
        {
            clients.Remove(client);
            _lastLocation = new PointF(Location.X, _lastLocation.Y + 50);
        }

        public int GetPositionInLine(Client client)
        {
            return clients.IndexOf(client);
        }

        public void OpenCheckout()
        {
            isOpen = true;
        }

        public void CloseCheckout()
        {
            isOpen = false;
        }

        public int GetNumberClients()
        {
            return clients.Count;
        }

        public bool IsOpen()
        {
            return isOpen;
        }

        public bool IsFull()
        {
            return !(clients.Count < maxNumberClients);
        }
    }
}
