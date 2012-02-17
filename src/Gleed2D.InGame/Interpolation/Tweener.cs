using System ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame.Interpolation
{
    public delegate float TweeningFunction(float timeElapsed, float start, float change, float duration);

    public delegate void TweenEndHandler();

    public sealed class Tweener
    {
        bool _hasEnded ;
        
        public event TweenEndHandler Ended;

        public static TweeningFunction CreateTweeningFunction<T>( Easing easing )
        {
            return CreateTweeningFunction( typeof( T ), easing ) ;
        }

        public static TweeningFunction CreateTweeningFunction(Type type, Easing easing )
        {
            return (TweeningFunction)(Delegate.CreateDelegate(typeof(TweeningFunction), type, easing.ToString( )) ) ;
        }

        public Tweener(float from, float to, float duration, TweeningFunction tweeningFunction)
        {
            this.tweeningFunction = tweeningFunction;

            Initialise( from, to, duration ) ;
        }

        public void Initialise( float from, float to,  float duration )
        {
            Running = true ;
            _elapsed = 0.0f ;
            _from = from;
            Position = from;
            _change = to - from;
            _duration = duration;
        }

        public Tweener(float from, float to, TimeSpan duration, TweeningFunction tweeningFunction)
            : this(from, to, (float)duration.TotalSeconds, tweeningFunction)
        {
        }

        public float Elapsed
        {
            get
            {
                return _elapsed ;
            }
        }

        public float Position
        {
            get ;
            protected set ;
        }

        protected float _from
        {
            get ;
            set ;
        }

        protected float _change
        {
            get ;
            set ;
        }

        protected float _duration
        {
            get ;
            private set ;
        }

        protected float _elapsed
        {
            get ;
            set ;
        }

        public bool Running
        {
            get ;
            protected set ;
        }

        protected TweeningFunction tweeningFunction
        {
            get ;
            private set ;
        }

        public void Update(GameTime gameTime)
        {
            if (!Running || (_elapsed == _duration))
            {
                return;
            }
            Position = tweeningFunction(_elapsed, _from, _change, _duration);
            _elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_elapsed >= _duration)
            {
                _elapsed = _duration;
                Position = _from + _change;
                OnEnd();
            }
        }

        protected void OnEnd()
        {
            _hasEnded = true ;
            
            if (Ended != null)
            {
                Ended();
            }
        }

        public void Start()
        {
            Running = true;
        }

        public void Stop()
        {
            Running = false;
        }

        public bool HasEnded
        {
            get
            {
                return _hasEnded ;
            }
        }

        public void Reset()
        {
            _hasEnded = false ;
            _elapsed = 0.0f;
            _from = Position;
        }

        public void Reset(float to)
        {
            _change = to - Position;
            Reset();
        }

        public void Reverse()
        {
            _elapsed = 0.0f;
            _change = -_change + (_from + _change - Position);
            _from = Position;
        }

        public override string ToString()
        {
            return
                @"{0}.{1}. Tween {2} -> {3} in {4}s. Elapsed {5:##0.##}s".FormatWith(
                    tweeningFunction.Method.DeclaringType.Name, tweeningFunction.Method.Name, _from, _from + _change,
                    _duration, _elapsed);
        }
    }
}
