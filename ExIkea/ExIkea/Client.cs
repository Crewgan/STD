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
using System.Runtime.CompilerServices;

namespace ExIkea
{
    class Client
    {
        static Size size = new Size(30, 30);
        private Random _rdm;

        public PointF location;
        private Size _storeSize;
        private PointF _departure; // Point of departure
        private PointF _arrival; // Point of arrival
        private Stopwatch _spMovement; // Movement timer
        private Stopwatch _spCheckout; // Shopping timer

        private float _timeToReachArrival;
        private float _speedX;
        private float _speedY;
        private int _milliseconds = 1000;

        private int _nbArticles; // Number of articles the client has
        private int _timeBeforeCheckout; // The amount of time _spCheckout must reach before searching for a checkout
        private bool _isAngry;
        private List<Checkout> _checkouts;
        private Checkout _checkout; // Selected checkout

        private int _positionInLine;
        public clientState state;
        private Brush _color;

        private int lineDistance; // distance between each points of the line

        public enum clientState
        {
            Shopping,
            SearchCheckout,
            InQueue,
            CheckingOut,
            Done
        }

        public bool IsAngry { get => _isAngry; }

        public Client(Size storeSize, List<Checkout> checkouts, Random rdm)
        {
            this._storeSize = storeSize;
            this._rdm = rdm;
            _departure = new PointF(rdm.Next(0, this._storeSize.Width), rdm.Next(0, this._storeSize.Height));
            location = _departure;
            _arrival = new PointF(rdm.Next(0, this._storeSize.Width), rdm.Next(0, this._storeSize.Height));
            _spMovement = new Stopwatch();
            _spMovement.Start();

            _spCheckout = new Stopwatch();
            _spCheckout.Start();

            _timeToReachArrival = rdm.Next(2500, 4000); // Random speed
            _nbArticles = rdm.Next(5, 25);
            _timeBeforeCheckout = _nbArticles * 1000; // Converte the number of articles in time (milliseconds)

            _isAngry = false;
            this._checkouts = checkouts;

            state = clientState.Shopping;

            lineDistance = 50;

            CalculateSpeed();
        }

        /// <summary>
        /// Display the client at his position with different colors depending on his state.
        /// </summary>
        public void Paint(Graphics g)
        {
            if (state == clientState.Shopping)
                _color = Brushes.Gray; // Shopping
            else if (!_isAngry)
                _color = Brushes.Green; // Search for checkout
            else
                _color = Brushes.Red; // Angry

            g.FillEllipse(_color, new RectangleF(location, size));
        }


        public void Actions()
        {
            switch (state)
            {
                case clientState.Shopping: // Move at a random speed to random location in the store. Until it finished shopping.______________________________________________________________________________________
                    if (_spCheckout.ElapsedMilliseconds >= _timeBeforeCheckout)
                    {
                        state = clientState.SearchCheckout;
                        _spCheckout.Reset();
                        _spCheckout.Stop();
                    }
                    Move();
                    break;
                case clientState.SearchCheckout: // Search for a checkout and move towards it._________________________________________________________________________________________________________________________
                    Move();

                    Checkout newCheckout = FindCheckout();
                    // Find the checkout with the lesser clients.
                    if (!_isAngry && _checkout != newCheckout)
                    {
                        _checkout = newCheckout;
                        NewDestination(_checkout.LastLocation);
                    }

                    // Find the position in the line.
                    if (_checkout != null && !Point.Round(_arrival).Equals(Point.Round(_checkout.LastLocation)) && !_isAngry)
                        NewDestination(_checkout.LastLocation);

                    // Found and obtains a position in line.
                    if (_checkout != null && Point.Round(location).Equals(Point.Round(_checkout.LastLocation)) && _checkout.NewClient(this))
                    {
                        state = clientState.InQueue;
                        _positionInLine = _checkout.GetPositionInLine(this);
                    }

                    // Easiest way to deal with a bug where the angry client happend to stop in the line and never move again.
                    if (IsAngry && !_spMovement.IsRunning)
                        NewDestination(new PointF(_rdm.Next(0, this._storeSize.Width), _rdm.Next(0, this._storeSize.Height)));

                    break;
                case clientState.InQueue: // Move in line until it reaches the checkout._______________________________________________________________________________________________________________________________
                    Move();
                    int newPositionInLine = _checkout.GetPositionInLine(this);

                    if (_positionInLine != newPositionInLine)
                    {
                        _positionInLine = newPositionInLine;
                        NewDestination(new PointF(_checkout.Location.X, _checkout.Location.Y - _positionInLine * lineDistance));
                    }

                    if (InteresctWith(_checkout) && _checkout.CheckingOut(this))
                        state = clientState.CheckingOut;

                    break;
                case clientState.CheckingOut: // Buy his articles. Takes more time if he has more articles.____________________________________________________________________________________________________________
                    if (!_spCheckout.IsRunning)
                        _spCheckout.Start();

                    if (_spCheckout.ElapsedMilliseconds >= _timeBeforeCheckout)
                    {
                        _checkout.ClientDone(this);
                        state = clientState.Done;
                    }
                    break;
                case clientState.Done: // Finished and await the store to be deleted.
                default:
                    break;
            }
        }

        /// <summary>
        /// Find an opened checkout with the lesser clients and go towards it or towards te position in line.
        /// If non found, becomes angry.
        /// </summary>
        /// <returns>Selected checkout</returns>
        private Checkout FindCheckout()
        {
            Checkout result = null;
            result = _checkouts.OrderBy(k => k.GetNumberClients()).Where(k => !k.IsFull() && k.IsOpen()).SingleOrDefault();
            _isAngry = result == null;
            return result;
        }

        /// <summary>
        /// Check if the client collided with the checkout.
        /// </summary>
        /// <param name="checkout">Checkout to collide with</param>
        /// <returns>True if collided, false if not</returns>
        private bool InteresctWith(Checkout checkout)
        {
            bool result = false;

            if (location.X < checkout.Location.X + Checkout.Size.Width &&
                location.X + size.Width > checkout.Location.X &&
                location.Y < checkout.Location.Y + Checkout.Size.Height * 1.5 &&
                location.Y + size.Height > checkout.Location.Y + Checkout.Size.Height / 2)
            {
                result = true;
            }
            return result;
        }

        #region Movement

        /// <summary>
        /// Calculate the speed and the time the client has to reach his target.
        /// </summary>
        private void CalculateSpeed()
        {
            float speed = 100;
            float xDiff = _arrival.X - _departure.X;
            float yDiff = _arrival.Y - _departure.Y;
            double angle = Math.Atan2(yDiff, xDiff);

            _speedX = (float)Math.Cos(angle) * speed;
            _speedY = (float)Math.Sin(angle) * speed;

            _timeToReachArrival = xDiff / _speedX * _milliseconds;
        }

        /// <summary>
        /// Move the client in direction of his target. If the client is shopping, select a random target.
        /// </summary>
        private void Move()
        {
            if (_spMovement.IsRunning)
            {
                location.X = _speedX * ((float)_spMovement.ElapsedMilliseconds / _milliseconds) + _departure.X;
                location.Y = _speedY * ((float)_spMovement.ElapsedMilliseconds / _milliseconds) + _departure.Y;
            }

            if (_spMovement.ElapsedMilliseconds >= _timeToReachArrival)
            {
                _spMovement.Reset();
                if (state == clientState.Shopping || (state == clientState.SearchCheckout && _isAngry))
                    NewDestination(new PointF(_rdm.Next(0, this._storeSize.Width), _rdm.Next(0, this._storeSize.Height)));
            }
        }

        /// <summary>
        /// Set the new destination.
        /// </summary>
        private void NewDestination(PointF newArrival)
        {
            _departure = new PointF(location.X, location.Y);
            _arrival = newArrival;

            CalculateSpeed();
            _spMovement.Restart();
        }
        #endregion
    }
}
