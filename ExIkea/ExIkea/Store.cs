/**********************************************************************************************************************************************************************************************************************
 * Name        : Joey Martig
 * Project     : exIkea
 * Date        : 26.10.2020
 * Description : Simulate a store with clients and checkouts.
 **********************************************************************************************************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace ExIkea
{
    class Store
    {
        readonly Size size; // Size of the store
        readonly int maxClients; // Number max of clients in the store
        private int _checkoutAvailableAtOpening; // Number of open checkout when the store opens

        private Stopwatch _spCheckout; // Timer to open checkouts
        readonly int timeBeforeOpeningCheckout; // the amount of time _spCheckout must reach to open a new checkout

        private Stopwatch _spClientArrival; // Timer to add new clients to the store
        private int _timeBeforeClientArrival; // The amount of time _spClientArrival must reach before adding new clients
        private int _numberOfClientByArrival; // Amount of clients at each arrivals

        private List<Checkout> _checkouts;
        private List<Client> _clients;

        Random rdm;
        public Store(Size size, int maxClients, int checkoutAvailableAtOpening, int timeBeforeOpeningCheckout, int timeBeforeClientArrival, int numberOfClientByArrival, int numberOfCheckouts)
        {

            this.size = size;
            this.maxClients = maxClients;
            this._checkoutAvailableAtOpening = checkoutAvailableAtOpening;
            this.timeBeforeOpeningCheckout = timeBeforeOpeningCheckout;
            this._timeBeforeClientArrival = timeBeforeClientArrival * 1000; // Convert seconds in milliseconds
            this._numberOfClientByArrival = numberOfClientByArrival;

            _spCheckout = new Stopwatch();

            _spClientArrival = new Stopwatch();
            _spClientArrival.Start();

            _checkouts = new List<Checkout>();
            _clients = new List<Client>();

            rdm = new Random();

            CreateCheckouts(numberOfCheckouts);
            CreateClients(numberOfClientByArrival);
        }
        
        /// <summary>
        /// All the basics actions of the store.
        /// Opening/Closing checkouts.
        /// Adding/Removing clients.
        /// Displaying checkouts and clients.
        /// </summary>
        public void Actions(Graphics g)
        {
            if (_spClientArrival.ElapsedMilliseconds >= _timeBeforeClientArrival && _clients.Count < maxClients)
            {
                CreateClients(_numberOfClientByArrival);
                _spClientArrival.Restart();
            }

            if (CountAngryClients() > 0)
                _spCheckout.Start();
            else
                _spCheckout.Stop();

            if (_spCheckout.ElapsedMilliseconds >= timeBeforeOpeningCheckout)
            {
                _checkouts.Where(k => !k.IsOpen()).First().OpenCheckout();
                _spCheckout.Reset();
            }

            RemoveClientsWhenDone();

            if (_checkouts.Where(k => k.IsOpen()).Sum(k => k.MaxNumberClients) > _clients.Count)
            {
                CloseCheckout();
            }

            Paint(g);
        }

        /// <summary>
        /// Display the checkouts and the clients.
        /// Let the clients do what they must do.
        /// </summary>
        public void Paint(Graphics g)
        {
            foreach (var checkout in _checkouts)
            {
                checkout.Paint(g);
            }

            foreach (var client in _clients)
            {
                client.Actions();
                client.Paint(g);
            }
        }

        /// <summary>
        /// Create numberOfNewClient clients.
        /// </summary>
        private void CreateClients(int numberOfNewClient)
        {
            while (numberOfNewClient != 0)
            {
                _clients.Add(new Client(size, _checkouts, rdm));
                numberOfNewClient--;
            }
        }

        /// <summary>
        /// Create numberOfNewCheckouts checkouts.
        /// Set if they are open or not.
        /// Set the number of max client for each checkouts.
        /// Set their locations.
        /// </summary>
        private void CreateCheckouts(int numberOfNewCheckouts)
        {
            for (int i = 0; i < numberOfNewCheckouts; i++)
            {
                bool isOpen = i < _checkoutAvailableAtOpening ? true : false;
                int numberMaxClient = 5;
                _checkouts.Add(new Checkout(isOpen, numberMaxClient, new Point(i * (Checkout.Size.Width + 10), size.Height - Checkout.Size.Height)));
            }
        }

        /// <summary>
        /// Close one checkout.
        /// </summary>
        private void CloseCheckout()
        {
            int index = _checkouts.Count - 1;
            while (index > 0)
            {
                if (_checkouts[index].IsOpen())
                {
                    _checkouts[index].CloseCheckout();
                    break;
                }
                else
                    index--;
            }
        }

        /// <summary>
        /// Count the number of available checkouts.
        /// </summary>
        /// <returns>number of available checkouts</returns>
        private int CountAvailableCheckouts()
        {
            int result = 0;
            foreach (var checkout in _checkouts)
            {
                result += checkout.IsOpen() ? 1 : 0;
            }
            return result;
        }

        /// <summary>
        /// Count the number of clients without checkout.
        /// </summary>
        /// <returns>number of angry clients</returns>
        private int CountAngryClients()
        {
            int result = 0;
            foreach (var client in _clients)
            {
                result += client.IsAngry ? 1 : 0;
            }
            return result;
        }

        /// <summary>
        /// Remove a client from the list when he is done.
        /// </summary>
        private void RemoveClientsWhenDone()
        {
            int index = 0;
            while (index < _clients.Count-1)
            {
                if (_clients[index].state == Client.clientState.Done)
                {
                    _clients.RemoveAt(index);
                }
                else
                    index++;
            }
        }
    }
}
