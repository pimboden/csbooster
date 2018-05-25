// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar
using System;
using System.Transactions;

namespace AJAXASMXHandler
{
    public class TransactionalMethodAttribute : Attribute
    {
        private TransactionScopeOption _TransactionOption = TransactionScopeOption.Required;

        public TransactionScopeOption TransactionOption
        {
            get { return _TransactionOption; }
            set { _TransactionOption = value; }
        }

        private IsolationLevel _IsolationLevel = IsolationLevel.ReadCommitted;

        public IsolationLevel IsolationLevel
        {
            get { return _IsolationLevel; }
            set { _IsolationLevel = value; }
        }

        private int _Timeout = 30;

        public int Timeout
        {
            get { return _Timeout; }
            set { _Timeout = value; }
        }

        public TransactionalMethodAttribute(TransactionScopeOption option, IsolationLevel isolationLevel, int timeout)
        {
            Timeout = timeout;
            IsolationLevel = isolationLevel;
            TransactionOption = option;
        }

        public TransactionalMethodAttribute(int timeout)
        {
            Timeout = timeout;
        }

        public TransactionalMethodAttribute(TransactionScopeOption option)
        {
            TransactionOption = option;
        }

        public TransactionalMethodAttribute(TransactionScopeOption option, IsolationLevel isolationLevel)
        {
            TransactionOption = option;
            IsolationLevel = isolationLevel;
        }

        public TransactionalMethodAttribute(IsolationLevel isolationLevel)
        {
            IsolationLevel = isolationLevel;
        }

        public TransactionalMethodAttribute()
        {
        }
    }
}