using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExIkea
{
    class Store
    {
        readonly Size size;
        readonly int maxClients;
        private int _checkoutAvailableAtOpening;

        private Stopwatch _spCheckout;
        readonly int timeBeforeOpeningCheckout;

        private Stopwatch _spClientArrival;
        private System.Windows.Forms.Timer _timer;
        private int _timeBeforeClientArrival;
        private int _numberOfClientByArrival;

        private List<Checkout> _checkouts;
        private List<Client> _clients;

        Random rdm;
        public Store(Size size, int maxClients, int checkoutAvailableAtOpening, int timeBeforeOpeningCheckout, int timeBeforeClientArrival, int numberOfClientByArrival, int numberOfCheckouts)
        {

            this.size = size;
            this.maxClients = maxClients;
            this._checkoutAvailableAtOpening = checkoutAvailableAtOpening;
            this.timeBeforeOpeningCheckout = timeBeforeOpeningCheckout;
            this._timeBeforeClientArrival = timeBeforeClientArrival * 1000;
            this._numberOfClientByArrival = numberOfClientByArrival;
            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += new EventHandler(TimerOnTick);
            _timer.Enabled = true;
            _timer.Interval = 60;

            _spCheckout = new Stopwatch();

            _spClientArrival = new Stopwatch();
            _spClientArrival.Start();

            _checkouts = new List<Checkout>();
            _clients = new List<Client>();


            rdm = new Random();

            CreateCheckouts(numberOfCheckouts);
            CreateClients(numberOfClientByArrival);
        }

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
        
        private void TimerOnTick(object sender, EventArgs e)
        {
            if (_spClientArrival.ElapsedMilliseconds >= _timeBeforeClientArrival && _clients.Count < maxClients)
            {
                CreateClients(_numberOfClientByArrival);
                _spClientArrival.Restart();
            }

            CountAvailableCheckouts(); // Afficher
            if (CountAngryClients() > 0)
            {
                _spCheckout.Start();
            }
            else
            {
                _spCheckout.Stop();
            }

            if (_spCheckout.ElapsedMilliseconds >= timeBeforeOpeningCheckout)
            {
                foreach (var checkout in _checkouts)
                {
                    if (!checkout.IsOpen())
                    {
                        //checkout.OpenCheckout();
                    }
                }
                _spCheckout.Reset();
            }
        }
        private int CountAvailableCheckouts()
        {
            int result = 0;
            foreach (var checkout in _checkouts)
            {
                result += checkout.IsOpen() ? 1 : 0;
            }
            return result;
        }

        private int CountAngryClients()
        {
            int result = 0;
            foreach (var client in _clients)
            {
                result += client.IsAngry ? 1 : 0;
            }
            return result;
        }

        private void CreateClients(int numberOfNewClient)
        {
            while (numberOfNewClient != 0)
            {
                _clients.Add(new Client(size, _checkouts, rdm));
                numberOfNewClient--;
            }
        }

        private void CreateCheckouts(int numberOfNewCheckouts)
        {
            for (int i = 0; i < numberOfNewCheckouts; i++)
            {
                bool isOpen = i < _checkoutAvailableAtOpening ? true : false;
                int numberMaxClient = 5;
                _checkouts.Add(new Checkout(isOpen, numberMaxClient, new Point(i * (Checkout.Size.Width + 10), size.Height - Checkout.Size.Height)));
            }
        }
    }
}
