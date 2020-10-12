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
        static Size size = new Size(30, 30);
        Random rdm;
        
        Size storeSize;
        public Point location;
        Point departure;
        Point arrival;
        Stopwatch spMovement;
        Stopwatch spCheckout;

        float timeToReachArrival;
        float speedX;
        float speedY;
        int milliseconds = 1000;

        int nbArticles;
        int timeBeforeCheckout;
        bool isCheckingOut;
        bool isAngry;
        List<Checkout> checkouts;
        private Checkout _checkout;

        enum clientState
        {
            Shopping,
            SearchCheckout,
            InQueue,
            CheckingOut
        }

        clientState state;

        Brush color;

        public bool IsAngry { get => isAngry; }

        public Client(Size storeSize, List<Checkout> checkouts, Random rdm)
        {
            this.storeSize = storeSize;
            this.rdm = rdm;
            departure = new Point(rdm.Next(0, this.storeSize.Width), rdm.Next(0, this.storeSize.Height));
            location = departure;
            arrival = new Point(rdm.Next(0, this.storeSize.Width), rdm.Next(0, this.storeSize.Height));
            spMovement = new Stopwatch();
            spMovement.Start();

            spCheckout = new Stopwatch();
            spCheckout.Start();

            timeToReachArrival = rdm.Next(5000, 8000);
            nbArticles = rdm.Next(5, 25);
            timeBeforeCheckout = nbArticles * 1000;

            isCheckingOut = false;
            isAngry = false;
            this.checkouts = checkouts;

            state = clientState.Shopping;

            CalculateSpeed();
        }

        public void Paint(Graphics g)
        {
            if (state == clientState.Shopping)
                color = Brushes.Gray;
            else if (!IsAngry)
                color = Brushes.Green;
            else
                color = Brushes.Red;

            g.FillEllipse(color, new RectangleF(location, size));
        }

        public void Actions()
        {
            switch (state)
            {
                case clientState.Shopping:
                    Move();
                    break;
                case clientState.SearchCheckout:
                    Checkout newCheckout = FindCheckout();
                    if (newCheckout != null && _checkout != newCheckout)
                    {
                        _checkout = newCheckout;
                        NewDestination(_checkout.GetQueueLocation());
                    }

                    Move();

                    if (_checkout != null && _checkout.IsInQueue(this))
                    {
                        state = clientState.InQueue;
                        _checkout.NewClient(this);
                    }

                    break;
                case clientState.InQueue:
                    Console.WriteLine("Youhou");
                    
                    break;
                case clientState.CheckingOut:
                    break;
                default:
                    Console.WriteLine("Help");
                    break;
            }

            if (spCheckout.ElapsedMilliseconds >= timeBeforeCheckout)
            {
                state = clientState.SearchCheckout;
                spCheckout.Reset();
                spCheckout.Stop();
            }
        }

        private Checkout FindCheckout()
        {
            Checkout result = null;
            int minCheckoutNbClients = Int32.MaxValue;
            foreach (var checkout in checkouts)
            {
                if (checkout.IsOpen() && !checkout.IsFull() && minCheckoutNbClients > checkout.GetNumberClients())
                {
                    minCheckoutNbClients = checkout.GetNumberClients();
                    result = checkout;
                }
            }
            isAngry = result == null;

            return result;
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
            if (spMovement.IsRunning)
            {
                location.X = Convert.ToInt32(speedX * (Convert.ToDouble(spMovement.ElapsedMilliseconds) / milliseconds) + departure.X);
                location.Y = Convert.ToInt32(speedY * (Convert.ToDouble(spMovement.ElapsedMilliseconds) / milliseconds) + departure.Y);
            }

            if (Convert.ToInt32(spMovement.ElapsedMilliseconds) >= Convert.ToInt32(timeToReachArrival) && state == clientState.Shopping)
            {
                NewDestination();
            }
        }

        private void NewDestination()
        {
            departure = new Point(location.X, location.Y);
            arrival = new Point(rdm.Next(0, this.storeSize.Width), rdm.Next(0, this.storeSize.Height));

            CalculateSpeed();
            spMovement.Restart();
        }

        private void NewDestination(Point newArrival)
        {
            departure = new Point(location.X, location.Y);
            arrival = newArrival;

            CalculateSpeed();
            spMovement.Restart();
        }
    }
}
