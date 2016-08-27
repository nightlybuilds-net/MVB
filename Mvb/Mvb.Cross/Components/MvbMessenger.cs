﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Mvb.Core.Components
{
    public static class MvbMessenger
    {
        private static readonly
            Dictionary<Tuple<string, Type, Type>, List<Tuple<WeakReference, Action<object, object>>>> Calls =
                new Dictionary<Tuple<string, Type, Type>, List<Tuple<WeakReference, Action<object, object>>>>();

        /// <summary>
        /// Send Message with args
        /// </summary>
        /// <typeparam name="TSender">TSender</typeparam>
        /// <typeparam name="TArgs">TMessageArgs</typeparam>
        /// <param name="sender">Sender</param>
        /// <param name="message">Message</param>
        /// <param name="args">Args</param>
        public static void Send<TSender, TArgs>(TSender sender, string message, TArgs args) where TSender : class
        {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));
            InnerSend(message, typeof(TSender), typeof(TArgs), sender, args);
        }

        /// <summary>
        /// Send Message without args
        /// </summary>
        /// <typeparam name="TSender">TSender</typeparam>
        /// <param name="sender">Sender</param>
        /// <param name="message">Message</param>
        public static void Send<TSender>(TSender sender, string message) where TSender : class
        {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));
            InnerSend(message, typeof(TSender), null, sender, null);
        }

        /// <summary>
        /// Subscribe Message with args
        /// </summary>
        /// <typeparam name="TSender">TSender</typeparam>
        /// <typeparam name="TArgs">TArgs</typeparam>
        /// <param name="subscriber">Subscriber</param>
        /// <param name="message">Message</param>
        /// <param name="callback">Action</param>
        /// <param name="source">source</param>
        public static void Subscribe<TSender, TArgs>(object subscriber, string message, Action<TSender, TArgs> callback,
            TSender source = null) where TSender : class
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            Action<object, object> wrap = (sender, args) =>
            {
                var send = (TSender) sender;
                if (source == null || send == source)
                    callback((TSender) sender, (TArgs) args);
            };

            InnerSubscribe(subscriber, message, typeof(TSender), typeof(TArgs), wrap);
        }

        /// <summary>
        /// Subscribe Message without args
        /// </summary>
        /// <typeparam name="TSender">TSender</typeparam>
        /// <param name="subscriber">Subscriber</param>
        /// <param name="message">Message</param>
        /// <param name="callback">Action</param>
        /// <param name="source">source</param>
        public static void Subscribe<TSender>(object subscriber, string message, Action<TSender> callback,
            TSender source = null) where TSender : class
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            Action<object, object> wrap = (sender, args) =>
            {
                var send = (TSender) sender;
                if (source == null || send == source)
                    callback((TSender) sender);
            };

            InnerSubscribe(subscriber, message, typeof(TSender), null, wrap);
        }

        /// <summary>
        /// Unsubscribe action with args
        /// </summary>
        /// <typeparam name="TSender">TSender</typeparam>
        /// <typeparam name="TArgs">TArgs</typeparam>
        /// <param name="subscriber">Subscriber</param>
        /// <param name="message">Message</param>
        public static void Unsubscribe<TSender, TArgs>(object subscriber, string message) where TSender : class
        {
            InnerUnsubscribe(message, typeof(TSender), typeof(TArgs), subscriber);
        }

        /// <summary>
        /// Unsubscribe action without args
        /// </summary>
        /// <typeparam name="TSender">TSender</typeparam>
        /// <param name="subscriber">Subscriber</param>
        /// <param name="message">Message</param>
        public static void Unsubscribe<TSender>(object subscriber, string message) where TSender : class
        {
            InnerUnsubscribe(message, typeof(TSender), null, subscriber);
        }

        /// <summary>
        /// Remove all callbacks
        /// </summary>
        public static void Reset()
        {
            Calls.Clear();
        }

        #region PRIVATE
        private static void InnerSend(string message, Type senderType, Type argType, object sender, object args)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            var key = new Tuple<string, Type, Type>(message, senderType, argType);
            if (!Calls.ContainsKey(key))
                return;
            var actions = Calls[key];
            if (actions == null || !actions.Any())
                return;

            var actionsCopy = actions.ToList();
            foreach (var action in actionsCopy)
            {
                if (action.Item1.IsAlive && actions.Contains(action))
                    action.Item2(sender, args);
            }
        }

        private static void InnerSubscribe(object subscriber, string message, Type senderType, Type argType,
            Action<object, object> callback)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            var key = new Tuple<string, Type, Type>(message, senderType, argType);
            var value = new Tuple<WeakReference, Action<object, object>>(new WeakReference(subscriber), callback);
            if (Calls.ContainsKey(key))
            {
                Calls[key].Add(value);
            }
            else
            {
                var list = new List<Tuple<WeakReference, Action<object, object>>> { value };
                Calls[key] = list;
            }
        }

        private static void InnerUnsubscribe(string message, Type senderType, Type argType, object subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var key = new Tuple<string, Type, Type>(message, senderType, argType);
            if (!Calls.ContainsKey(key))
                return;
            Calls[key].RemoveAll(tuple => !tuple.Item1.IsAlive || tuple.Item1.Target == subscriber);
            if (!Calls[key].Any())
                Calls.Remove(key);
        } 
        #endregion
    }
}