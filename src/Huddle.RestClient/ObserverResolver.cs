using System;
using System.Collections.Generic;

namespace Huddle.Clients
{
    public class ObserverResolver<TObserver> : List<TObserver>
    {
        private static IEnumerable<TObserver> _observers;

        public ObserverResolver()
        {
            AddRange(GetObservers());
        }

        private IEnumerable<TObserver> GetObservers()
        {
            if (_observers != null)
            {
                return _observers;
            }

            var stuff = new List<TObserver>();
            foreach (var assType in GetType().Assembly.GetTypes())
            {
                if (typeof(TObserver).IsAssignableFrom(assType) && assType.IsClass)
                {
                    stuff.Add((TObserver)Activator.CreateInstance(assType));
                }
            }

            _observers = stuff;

            return _observers;
        }
    }
}