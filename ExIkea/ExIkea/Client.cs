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
        public PointF location;
        PointF departure;
        PointF arrival;
        Stopwatch spMovement;
        Stopwatch spCheckout;

        float timeToReachArrival;
        float speedX;
        float speedY;
        int milliseconds = 1000;

        int nbArticles;
        int timeBeforeCheckout;
        bool isAngry;
        List<Checkout> checkouts;
        private Checkout _checkout;

        int positionInLine;

        enum clientState
        {
            Shopping,
            SearchCheckout,
            InQueue,
            CheckingOut,
            Done
        }

        clientState state;

        Brush color;

        public bool IsAngry { get => isAngry; }

        public Client(Size storeSize, List<Checkout> checkouts, Random rdm)
        {
            this.storeSize = storeSize;
            this.rdm = rdm;
            departure = new PointF(rdm.Next(0, this.storeSize.Width), rdm.Next(0, this.storeSize.Height));
            location = departure;
            arrival = new PointF(rdm.Next(0, this.storeSize.Width), rdm.Next(0, this.storeSize.Height));
            spMovement = new Stopwatch();
            spMovement.Start();

            spCheckout = new Stopwatch();
            spCheckout.Start();

            timeToReachArrival = rdm.Next(2500, 4000);
            nbArticles = rdm.Next(5, 25);
            timeBeforeCheckout = nbArticles * 1000;

            isAngry = false;
            this.checkouts = checkouts;

            state = clientState.Shopping;

            CalculateSpeed();
        }

        public void Paint(Graphics g)
        {
            if (state == clientState.Shopping)
                color = Brushes.Gray;
            else if (!isAngry)
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
                    if (spCheckout.ElapsedMilliseconds >= timeBeforeCheckout)
                    {
                        state = clientState.SearchCheckout;
                        spCheckout.Reset();
                        spCheckout.Stop();
                    }
                    Move();
                    break;
                case clientState.SearchCheckout:
                    Checkout newCheckout = FindCheckout();
                    // Aller vers la caisse aillant le moins de clients
                    if (newCheckout != null && _checkout != newCheckout)
                    {
                        _checkout = newCheckout;
                        NewDestination(_checkout.LastLocation);
                    }

                    // Trouver l'emplacement dans la queue
                    if (_checkout != null && !arrival.Equals(_checkout.LastLocation) && !isAngry)
                        NewDestination(_checkout.LastLocation);

                    Move();
                    if (_checkout != null && Point.Round(location).Equals(Point.Round(arrival)) && _checkout.NewClient(this))
                    {
                        state = clientState.InQueue;
                        positionInLine = _checkout.GetPositionInLine(this);
                    }

                    break;
                case clientState.InQueue:
                    Move();
                    int newPositionInLine = _checkout.GetPositionInLine(this);

                    if (positionInLine != newPositionInLine)
                    {
                        positionInLine = newPositionInLine;
                        NewDestination(new PointF(_checkout.Location.X, _checkout.Location.Y - positionInLine * 50));
                    }

                    if (Point.Round(location).Equals(Point.Round(_checkout.Location)))
                        Console.WriteLine(Point.Round(location) + " " + Point.Round(_checkout.Location));

                    if (Point.Round(location).Equals(Point.Round(_checkout.Location)) && _checkout.CheckingOut(this))
                        state = clientState.CheckingOut;

                    break;
                case clientState.CheckingOut:
                    if (!spCheckout.IsRunning)
                        spCheckout.Start();

                    if (spCheckout.ElapsedMilliseconds >= timeBeforeCheckout)
                    {
                        _checkout.ClientDone(this);
                        state = clientState.Done;
                    }
                    break;
                default:
                    break;
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

        #region Movement
        // Changer ce merdier
        private void CalculateSpeed()
        {
            float minX, maxX, minY, maxY, distanceX, distanceY;

            minX = departure.X > arrival.X ? arrival.X : departure.X;
            maxX = departure.X < arrival.X ? arrival.X : departure.X;

            minY = departure.Y > arrival.Y ? arrival.Y : departure.Y;
            maxY = departure.Y < arrival.Y ? arrival.Y : departure.Y;

            distanceX = (maxX - minX) * (arrival.X < departure.X ? -1 : 1);
            distanceY = (maxY - minY) * (arrival.Y < departure.Y ? -1 : 1);

            speedX = distanceX / (timeToReachArrival / milliseconds);
            speedY = distanceY / (timeToReachArrival / milliseconds);
        }

        private void Move()
        {
            if (spMovement.IsRunning)
            {
                location.X = speedX * ((float)spMovement.ElapsedMilliseconds / milliseconds) + departure.X;
                location.Y = speedY * ((float)spMovement.ElapsedMilliseconds / milliseconds) + departure.Y;
            }

            if (spMovement.ElapsedMilliseconds >= timeToReachArrival)
            {
                spMovement.Reset();
                if (state == clientState.Shopping || isAngry)
                    NewDestination(new PointF(rdm.Next(0, this.storeSize.Width), rdm.Next(0, this.storeSize.Height)));
            }
        }

        private void NewDestination(PointF newArrival)
        {
            departure = new PointF(location.X, location.Y);
            arrival = newArrival;

            CalculateSpeed();
            spMovement.Restart();
        }
        #endregion
    }
}
