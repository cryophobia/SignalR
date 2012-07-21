﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR
{
    public interface INewMessageBus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="eventKey"></param>
        /// <param name="value"></param>
        Task Publish(string source, string eventKey, string value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="cursor"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        IDisposable Subscribe(ISubscriber subscriber, string cursor, Func<MessageResult, Task<bool>> callback, int messageBufferSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventKey"></param>
        /// <returns></returns>
        string GetCursor(string eventKey);
    }
}
