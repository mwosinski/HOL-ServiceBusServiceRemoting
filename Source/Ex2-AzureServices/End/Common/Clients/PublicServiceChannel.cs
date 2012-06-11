﻿// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

namespace Common.Clients
{
    using System;
    using System.ServiceModel;
    using Common.Contracts;

    internal class PublicServiceChannel : ClientBase<ICrmPublicService>, ICrmPublicService, IDisposable
    {
        public PublicServiceChannel() 
        {
        }

        ~PublicServiceChannel()
        {
            this.Dispose(false);
        }

        public Customer[] ListCustomers()
        {
            return this.Channel.ListCustomers();
        }

        public void MoveCustomersToBankingEntity(Guid[] customerIds, BankingEntity bankingEntity)
        {
            this.Channel.MoveCustomersToBankingEntity(customerIds, bankingEntity);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.State == CommunicationState.Faulted)
                {
                    this.Abort();
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}
