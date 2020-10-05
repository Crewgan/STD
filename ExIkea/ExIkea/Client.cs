using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExIkea
{
    class Client
    {
        static Size size = new Size(10, 10);

        Random rdm;
        Size storeSize;
        Point location;
        Point departure;
        Point arrival;
        Stopwatch sp;

        float timeToReachArrival;
        float speedX;
        float speedY;
        int milliseconds = 1000;

        int nbArticles;
        int timeBeforeCheckout;
        bool isCheckingOut;
        bool isShopping;
        bool isAngry;
        List<Checkout> checkouts;
        private Checkout _checkout;

        public bool IsAngry { get => isAngry; }

        public Client(Size storeSize, List<Checkout> checkouts)
        {
            rdm = new Random();
            this.storeSize = storeSize;
            departure = new Point(rdm.Next(0, this.storeSize.Width), rdm.Next(0, this.storeSize.Height));
            location = departure;
            arrival = new Point(rdm.Next(0, this.storeSize.Width), rdm.Next(0, this.storeSize.Height));
            sp = new Stopwatch();
            sp.Start();

            timeToReachArrival = rdm.Next(5000, 8000);
            nbArticles = rdm.Next(5, 25);
            timeBeforeCheckout = nbArticles * 1000;

            isCheckingOut = false;
            isShopping = true;
            isAngry = false;
            this.checkouts = checkouts;

            CalculateSpeed();
        }

        public void Actions()
        {
            Move();
            if (sp.ElapsedMilliseconds >= timeBeforeCheckout)
            {
                isShopping = false;
            }
            FindCheckout();
        }
        private void FindCheckout()
        {
            if (!isShopping)
            {
                if (_checkout == null)
                {
                    int minCheckoutNbClients = Int32.MaxValue;
                    foreach (var checkout in checkouts)
                    {
                        if (minCheckoutNbClients > checkout.GetNumberClients() && checkout.IsOpen())
                        {
                            minCheckoutNbClients = checkout.GetNumberClients();
                            _checkout = checkout.IsFull() ? null : checkout;
                        }
                    }
                    isAngry = _checkout == null ? true : false;
                }

                departure = location;
                arrival = _checkout.Location;
                if (location == _checkout.Location)
                {
                    isCheckingOut = true;
                }

                if (isCheckingOut)
                {
                    _checkout.NewClient(this);
                }
            }
        }
        private void CalculateSpeed()
        {
            int minX, maxX, minY, maxY, distanceX, distanceY;

            minX = departure.X > arrival.X ? arrival.X : departure.X;
            maxX = departure.X < arrival.X ? arrival.X : departure.X;

            minY = departure.Y > arrival.Y ? arrival.Y : departure.Y;
            maxY = departure.Y < arrival.Y ? arrival.Y : departure.Y;

            distanceX = (maxX - minX) * (arrival.X < departure.X ? -1 : 1);
            distanceY = (maxY - minY) * (arrival.Y < departure.Y ? -1 : 1);

            speedX = (distanceX) / Convert.ToInt32(timeToReachArrival / milliseconds);
            speedY = (distanceY) / Convert.ToInt32(timeToReachArrival / milliseconds);
        }

        private void Move()
        {
            if (sp.IsRunning)
            {
                location.X = Convert.ToInt32(speedX * (Convert.ToDouble(sp.ElapsedMilliseconds) / milliseconds) + departure.X);
                location.Y = Convert.ToInt32(speedY * (Convert.ToDouble(sp.ElapsedMilliseconds) / milliseconds) + departure.Y);
            }

            if (Convert.ToInt32(sp.ElapsedMilliseconds) >= Convert.ToInt32(timeToReachArrival))
            {
                sp.Stop();
            }
        }
    }
}
