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
Imports System.Diagnostics
Imports Common.Clients
Imports Common.Contracts

Public Class CrmPublicService
    Implements ICrmPublicService
    Private client As CrmDataServiceClient

    Public Sub New()
        Me.client = New CrmDataServiceClient()
    End Sub

    Public Sub MoveCustomersToBankingEntity(ByVal customerIds() As Guid, ByVal bankingEntity As BankingEntity) Implements ICrmPublicService.MoveCustomersToBankingEntity
        For Each id As Guid In customerIds
            Dim customer = Me.client.GetCustomer(id)
            customer.BankingEntity = bankingEntity

            Me.client.UpdateCustomer(customer)

            Console.ForegroundColor = ConsoleColor.Green
            Trace.TraceInformation("[{0}] Customer {1} moved to {2}", DateTime.Now, customer.Name, bankingEntity)
            Console.ResetColor()
        Next id
    End Sub

    Public Function ListCustomers() As Customer() Implements ICrmPublicService.ListCustomers
        Return Me.client.GetCustomers()
    End Function
End Class