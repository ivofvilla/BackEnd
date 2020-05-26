﻿using ByteBank.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ByteBank.View.Utils
{
    public class ByteBankProgress<T> : IProgress<T>
    {
        private readonly Action<T> _handler;
        private readonly TaskScheduler taskScheduler;

        public ByteBankProgress(Action<T> handler)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            _handler = handler;
        }

        public void Report(T value)
        {
            Task.Factory.StartNew(
                    () => _handler(value),
                          CancellationToken.None,
                          TaskCreationOptions.None,
                          taskScheduler
                );
        }
    }
}
