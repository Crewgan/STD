/**********************************************************************************************************************************************************************************************************************
 * Name        : Joey Martig
 * Project     : exIkea
 * Date        : 26.10.2020
 * Description : Simulate a store with clients and checkouts.
 **********************************************************************************************************************************************************************************************************************/
using System.Collections.Generic;
using System.Drawing;

namespace ExIkea
{
    class Checkout
    {
        static Size size = new Size(50, 50);
        List<Client> clients;
        private PointF _location;
        private bool _isOpen;
        private int _maxNumberClients;
        Brush color;
        PointF _lastLocation; // Beginning of the line for new clients.

        public PointF Location { get => new PointF(_location.X, _location.Y); }
        public static Size Size { get => size; }
        public PointF LastLocation { get => _lastLocation; }
        public int MaxNumberClients { get => _maxNumberClients; }

        public Checkout(bool isOpen, int maxNumberClients, Point location)
        {
            this._isOpen = isOpen;
            this._maxNumberClients = maxNumberClients;
            this._location = location;
            _lastLocation = location;

            clients = new List<Client>();
        }

        /// <summary>
        /// Display the checkout.
        /// Blue if opened and red if closed.
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            if (_isOpen)
                color = Brushes.Blue;
            else
                color = Brushes.Red;

            g.FillRectangle(color, new RectangleF(_location, size));
        }

        /// <summary>
        /// Add new clients to this checkout and move the position in the line.
        /// </summary>
        /// <param name="client">Client to add to this checkout</param>
        /// <returns>True if the client can be in line(Someone took the place), false if not</returns>
        public bool NewClient(Client client)
        {
            if (clients.Count < MaxNumberClients)
            {
                clients.Add(client);
                _lastLocation = new PointF(Location.X, _lastLocation.Y - 50);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tell the client if he is checking out or still in line.
        /// </summary>
        public bool CheckingOut(Client client)
        {
            return client.Equals(clients[0]);
        }

        /// <summary>
        /// Removes client from this checkout and changes the position in the line.
        /// </summary>
        /// <param name="client">Client to remove</param>
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
            _isOpen = true;
        }

        public void CloseCheckout()
        {
            _isOpen = false;
        }

        public int GetNumberClients()
        {
            return clients.Count;
        }

        public bool IsOpen()
        {
            return _isOpen;
        }

        public bool IsFull()
        {
            return !(clients.Count < MaxNumberClients);
        }
    }
}
