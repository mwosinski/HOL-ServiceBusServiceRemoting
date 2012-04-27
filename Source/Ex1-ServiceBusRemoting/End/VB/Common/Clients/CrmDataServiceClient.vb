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

    Public Class CrmDataServiceClient
        Implements ICrmDataService
        Public Function GetCustomer(ByVal id As Guid) As Customer Implements ICrmDataService.GetCustomer
            Dim client = New CrmDataServiceChannel()
            Dim customer As Customer

            Try
                customer = client.GetCustomer(id)
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

            Return customer
        End Function

        Public Function GetCustomers() As Customer() Implements ICrmDataService.GetCustomers
            Dim client = New CrmDataServiceChannel()
            Dim customers = New Customer() {}

            Try
                customers = client.GetCustomers()
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

        Public Sub UpdateCustomer(ByVal customer As Customer) Implements ICrmDataService.UpdateCustomer
            Dim client = New CrmDataServiceChannel()

            Try
                client.UpdateCustomer(customer)
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