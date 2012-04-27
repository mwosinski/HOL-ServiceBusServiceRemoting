' ----------------------------------------------------------------------------------
' Microsoft Developer & Platform Evangelism
' 
' Copyright (c) Microsoft Corporation. All rights reserved.
' 
' THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
' EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
' OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
' ----------------------------------------------------------------------------------
' The example companies, organizations, products, domain names,
' e-mail addresses, logos, people, places, and events depicted
' herein are fictitious.  No association with any real company,
' organization, product, domain name, email address, logo, person,
' places, or events is intended or should be inferred.
' ----------------------------------------------------------------------------------

Imports Microsoft.VisualBasic
Imports System.ServiceModel
Imports Common.Contracts

Namespace Clients

    Public Class PublicServiceClient
        Implements ICrmPublicService
        Public Function ListCustomers() As Customer() Implements ICrmPublicService.ListCustomers
            Dim client = New PublicServiceChannel()
            Dim customers = New Customer() {}

            Try
                customers = client.ListCustomers()
                client.Close()
            Catch e1 As CommunicationException
                client.Abort()
                Throw
            Catch e2 As TimeoutException
                client.Abort()
                Throw
            Catch e3 As Exception
                client.Abort()
                Throw
            End Try

            Return customers
        End Function

        Public Sub MoveCustomersToBankingEntity(ByVal customerIds() As Guid, ByVal bankingEntity As BankingEntity) Implements ICrmPublicService.MoveCustomersToBankingEntity
            Dim client = New PublicServiceChannel()

            Try
                client.MoveCustomersToBankingEntity(customerIds, bankingEntity)
                client.Close()
            Catch e1 As CommunicationException
                client.Abort()
                Throw
            Catch e2 As TimeoutException
                client.Abort()
                Throw
            Catch e3 As Exception
                client.Abort()
                Throw
            End Try
        End Sub
    End Class
End Namespace
