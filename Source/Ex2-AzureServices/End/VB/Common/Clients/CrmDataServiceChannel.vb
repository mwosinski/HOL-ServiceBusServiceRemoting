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

    Friend Class CrmDataServiceChannel
        Inherits ClientBase(Of ICrmDataService)
        Implements ICrmDataService, IDisposable
        Public Sub New()
        End Sub

        Protected Overrides Sub Finalize()
            Me.Dispose(False)
        End Sub

        Public Function GetCustomer(ByVal id As Guid) As Customer Implements ICrmDataService.GetCustomer
            Return Me.Channel.GetCustomer(id)
        End Function

        Public Function GetCustomers() As Customer() Implements ICrmDataService.GetCustomers
            Return Me.Channel.GetCustomers()
        End Function

        Public Sub UpdateCustomer(ByVal customer As Customer) Implements ICrmDataService.UpdateCustomer
            Me.Channel.UpdateCustomer(customer)
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

