/*
*  Copyright (c) 2017 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Mediator class that decouples event publishers from event subscribers.
    /// Each subscriber should register itself on a specific message (template) with an action.
    /// E.g. 
    ///     mediator.Register<AnyChangeEvent>(subscriber, OnChanged);
    ///     ...
    ///     private void OnChanged(AnyChangeEvent ev) {..}
    /// 
    /// A publisher can notify all the listeners using Raise (immediately).
    ///     mediator.Raise(new AnyChangeEvent());
    /// or a specific listener:
    ///     mediator.Raise(subscriber, new AnyChangeEvent());
    /// 
    /// Use mediator.Post(...) to notify listeners in idle time.
    /// 
    /// A subscriber can unregister a single action or itself completely.
    ///     mediator.Unregister(subscriber, OnChanged);
    ///     mediator.Unregister(subscriber);
    /// 
    /// Exceptions can be caught inside the Mediator when an handler is attached to the OnException event.
    /// </summary>
    public class Mediator
    {
        private class Subscription
        {
            public readonly Type Topic;
            public readonly object Subscriber;          // Subscribers have no common base class except for 'object'.
            public readonly object Action;              // Action<T> has no common base class, so store as 'object' and cast there where needed.

            public Subscription(Type type, object sub, object act)
            {
                Topic = type;
                Subscriber = sub;
                Action = act;
            }
        };
        private readonly List<Subscription> Subscriptions;


        /// <summary>
        /// Base class when events are to be posted instead of raised.
        /// </summary>
        private abstract class PostableObject
        {
            internal abstract void Raise();
        }

        /// <summary>
        /// Templated class for postable events.
        /// </summary>
        private class PostedObject<TEvent> : PostableObject
        {
            private readonly Mediator _mediator;
            private readonly TEvent _event;
            private readonly object _subscriber;


            internal PostedObject(Mediator med, TEvent ev, object subscriber)
            {
                _mediator = med;
                _event = ev;
                _subscriber = subscriber;
            }


            internal override void Raise()
            {
                if (_subscriber == null)
                {
                    _mediator.Raise(_event);
                }
                else
                {
                    _mediator.Raise(_subscriber, _event);
                }
            }
        }


        /// <summary>
        /// Queue of postable events at Application OnIdle.
        /// </summary>
        private static readonly Queue<PostableObject> _messageQueue = new Queue<PostableObject>();        


        /// <summary>
        /// Handler when the mediator catches an exception when processing an event.
        /// </summary>
        public event EventHandler<Exception> OnException;


        /// <summary>
        /// Get the default (first defined) mediator.
        /// If none defined, create one.
        /// </summary>
        private static Mediator _defaultMediator;
        public static Mediator Default
        {
            get { return _defaultMediator ?? new Mediator(); }
        }


        /// <summary>
        /// Constructor of the mediator.
        /// </summary>
        public Mediator()
        {
            Subscriptions = new List<Subscription>();
            if (_defaultMediator == null)
            {
                _defaultMediator = this;
            }
        }

        /// <summary>
        /// Register actions to the mediator.
        /// </summary>
        /// <typeparam name="TEvent">The argument/class/object that is raised</typeparam>
        /// <param name="subscriber">The subscriber</param>
        /// <param name="eventAction">The action that has to be executed when the event is raised</param>
        public void Register<TEvent>(object subscriber, Action<TEvent> eventAction)
        {
            // If a subscriber subscribes twice, it will receive the event twice.
            Subscriptions.Add(new Subscription(typeof(TEvent), subscriber, eventAction));
        }

        public bool IsRegistered<TEvent>(object subscriber, Action<TEvent> eventAction)
        {
            return Subscriptions.Any(x => x.Subscriber == subscriber && x.Action is Action && (Action<TEvent>)x.Action == eventAction);
        }


        /// <summary>
        /// Unregister all actions for this subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        public void Unregister(object subscriber)
        {
            Subscriptions.RemoveAll(obj => obj.Subscriber == subscriber);
        }

        /// <summary>
        /// Unregister a single action from the mediator.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="subscriber"></param>
        /// <param name="eventAction"></param>
        public void Unregister<TEvent>(object subscriber, Action<TEvent> eventAction)
        {
            Subscriptions.RemoveAll(obj => (obj.Topic == typeof(TEvent) && (obj.Subscriber == subscriber) && ((obj.Action as Action<TEvent>) == eventAction)));
        }

        /// <summary>
        /// Clear all subscribers.
        /// </summary>
        public void Clear()
        {
            Subscriptions.Clear();
        }

        /// <summary>
        /// Notify all subscribers to that specific event about a change.
        /// Return the subscribers that processed the event.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="ev"></param>
        public List<object> Raise<TEvent>(TEvent ev)
        {
            List<object> subscribers = new List<object>();                  // List of subscribers that already have processed the event so they do not process it again.
            for (Type type = typeof (TEvent); type != null; type = type.BaseType)
            {                               // Create a seperate list here, because events can be unregistered while handling this event.
                List<Subscription> list = Subscriptions.Where(obj => (obj.Topic == type) && (subscribers.Contains(obj.Subscriber) == false)).ToList();
                subscribers.AddRange(RaiseEvent(ev, list));
            }
            return subscribers;
        }

        /// <summary>
        /// Notify a single subscriber to that specific event about a change.
        /// Return true when that subscriber has processed the event.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="subscriber"></param>
        /// <param name="ev"></param>
        public bool Raise<TEvent>(object subscriber, TEvent ev)
        {
            for (Type type = typeof(TEvent); type != null; type = type.BaseType)
            {                               // Create a seperate list here, because events can be unregistered while handling this event.
                if (RaiseEvent(ev, Subscriptions.Where(obj => (obj.Subscriber == subscriber) && (obj.Topic == type)).ToList()).Count == 1)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Notify all subscribers in idle time about a specific event.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        public void Post<TEvent>(TEvent ev)
        {
            _messageQueue.Enqueue(new PostedObject<TEvent>(this, ev, null));
        }


        /// <summary>
        /// Notify a specific subscriber in idle time about a specific event.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        public void Post<TEvent>(object subscriber, TEvent ev)
        {
            _messageQueue.Enqueue(new PostedObject<TEvent>(this, ev, subscriber));
        }


        /// <summary>
        /// Process events that have been posted in the message queue.
        /// </summary>
        internal static void HandlePostedEvents()
        {
            int cnt = _messageQueue.Count;
            while (cnt-- > 0)
            {
                _messageQueue.Dequeue().Raise();
            }
        }


        /// <summary>
        /// Check if the subscribers are subscribed to that event.
        /// Process the event and return the list of subscribers that are.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="ev"></param>
        /// <param name="list"></param>
        private List<object> RaiseEvent<TEvent>(TEvent ev, List<Subscription> list)
        {
            List<object> retval = new List<object>();
            foreach (Subscription sub in list)
            {
                Action<TEvent> action = sub.Action as Action<TEvent>;
                if (action == null)
                    continue;
                RaiseEvent(action, ev);
                retval.Add(sub.Subscriber);
            }
            return retval;
        }


        private void RaiseEvent<TEvent>(Action<TEvent> action, TEvent arg)
        {
            try
            {
                action(arg);

            }
            catch (Exception ex)
            {
                if (OnException == null)
                {
                    throw;
                }
                OnException(this, ex);
            }
        }
    }

}
