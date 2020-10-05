using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExIkea
{
    class Checkout
    {
        static Size size = new Size(15, 10);
        List<Client> clients;
        List<Point> queue;
        private Point _location;
        bool isOpen;
        int maxNumberClients;

        public Point Location { get => new Point(_location.X, _location.Y); }
        public static Size Size { get => size;}

        public Checkout(bool isOpen, int maxNumberClients, Point location)
        {
            this.isOpen = isOpen;
            this.maxNumberClients = maxNumberClients;
            this._location = location;

            queue = new List<Point>();
            for (int i = 0; i < this.maxNumberClients; i++)
            {
                queue.Add(new Point(this.Location.X, this.Location.Y - 10 * i));
            }
        }

        public void NewClient(Client client)
        {
            clients.Add(client);
        }

        public void ClientDone(Client client)
        {
            clients.Remove(client);
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
            return clients.Count < maxNumberClients ? false : true;
        }
    }
}
