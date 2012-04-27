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

    Friend Class PublicServiceChannel
        Inherits ClientBase(Of ICrmPublicService)
        Implements ICrmPublicService, IDisposable
        Public Sub New()
        End Sub

        Protected Overrides Sub Finalize()
            Me.Dispose(False)
        End Sub

        Public Function ListCustomers() As Customer() Implements ICrmPublicService.ListCustomers
            Return Me.Channel.ListCustomers()
        End Function

        Public Sub MoveCustomersToBankingEntity(ByVal customerIds() As Guid, ByVal bankingEntity As BankingEntity) Implements ICrmPublicService.MoveCustomersToBankingEntity
            Me.Channel.MoveCustomersToBankingEntity(customerIds, bankingEntity)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Me.State = CommunicationState.Faulted Then
                    Me.Abort()
                Else
                    Me.Close()
                End If
            End If
        End Sub
    End Class
End Namespace
