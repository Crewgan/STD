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
        Dictionary<Point, Client> queue;
        private Point _location;
        bool isOpen;
        int maxNumberClients;
        Brush color;

        public Point Location { get => new Point(_location.X, _location.Y); }
        public static Size Size { get => size;}

        public Checkout(bool isOpen, int maxNumberClients, Point location)
        {
            this.isOpen = isOpen;
            this.maxNumberClients = maxNumberClients;
            this._location = location;

            clients = new List<Client>();
            queue = new Dictionary<Point, Client>();
            for (int i = 0; i < this.maxNumberClients; i++)
            {
                queue.Add(new Point(this.Location.X + size.Width/2, this.Location.Y - (size.Height + 10) * i), null);
            }
        }
        public void Paint(Graphics g)
        {
            if (isOpen)
                color = Brushes.Blue;
            else
                color = Brushes.Red;

            g.FillRectangle(color, new RectangleF(_location, size));

            // Temporaire
            foreach (var item in queue)
            {
                g.FillRectangle(Brushes.Orange, new RectangleF(item.Key, new Size(15, 15)));
            }
        }

        public Point GetQueueLocation()
        {
            return queue.Select(k => k).Where(k => k.Value == null).FirstOrDefault().Key;
        }

        public void NewClient(Client client)
        {
            clients.Add(client);
        }

        public bool IsInQueue(Client client)
        {
            Point position;
            bool result;
            int errorMargin = 10;
            position = queue.Select(k => k).Where(k => k.Key.X <= client.location.X + errorMargin && 
                                                       k.Key.X >= client.location.X - errorMargin &&
                                                       k.Key.Y <= client.location.Y + errorMargin &&
                                                       k.Key.Y <= client.location.Y - errorMargin).FirstOrDefault().Key;

            result = !position.Equals(new Point(0, 0));

            if (result)
                queue[position] = client;

            Console.WriteLine(result + " position: " + position + " clientPosition: " + client.location);
            return result;
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
            return !(clients.Count < maxNumberClients);
        }
    }
}
